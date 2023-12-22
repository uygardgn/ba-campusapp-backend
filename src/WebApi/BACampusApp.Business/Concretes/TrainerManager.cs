using BACampusApp.Dtos.Admin;
using BACampusApp.Dtos.Students;
using BACampusApp.Dtos.Trainers;

namespace BACampusApp.Business.Concretes
{
    public class TrainerManager : ITrainerService
    {
        private readonly ITrainerRepository _trainerRepo;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IPhoneNumberService _phoneNumberManager;
        private readonly IClassroomTrainersRepository _classroomTrainersRepository;
        private readonly IClassroomRepository _classroomRepository;
        private readonly IActivityStateLogSevices _activityStateLogService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IStringLocalizer<Resource> _stringLocalizer;

        public TrainerManager(ITrainerRepository trainerRepo, IMapper mapper, IEmailService emailService, IPhoneNumberService phoneNumberService, IClassroomTrainersRepository classroomTrainersRepository, IClassroomRepository classroomRepository, IActivityStateLogSevices activityStateLogService, UserManager<IdentityUser> userManager, IStringLocalizer<Resource> stringLocalizer)
        {
            _trainerRepo = trainerRepo;
            _mapper = mapper;
            _emailService = emailService;
            _phoneNumberManager = phoneNumberService;
            _classroomTrainersRepository = classroomTrainersRepository;
            _classroomRepository = classroomRepository;
            _activityStateLogService = activityStateLogService;
            _userManager = userManager;
            _stringLocalizer = stringLocalizer;
        }

        /// <summary>
        ///  Bu metot Trainer nesnesi oluşturma işlemini yapmaktadır
        /// </summary>
        /// <param name="trainerCreateDto"></param>
        ///  <returns>SuccessDataResult<TrainerDto>, ErrorDataResult<TrainerDto></returns>
        public async Task<IResult> AddAsync(TrainerCreateDto trainerCreateDto)
        {
            bool isExistingEmail = await _trainerRepo.AnyAsync(x => x.Email == trainerCreateDto.Email);
            if (isExistingEmail)
                return new ErrorDataResult<TrainerDto>(_stringLocalizer[Messages.TrainerAlreadyExists]);

            try
            {
                var phoneNumber = await _phoneNumberManager.PhoneNumberFormaterAsync(trainerCreateDto.PhoneNumber, trainerCreateDto.Country);
                var countryCode = await _phoneNumberManager.GetCountryCodeAsync(trainerCreateDto.Country);
                trainerCreateDto.PhoneNumber = phoneNumber;
                trainerCreateDto.CountryCode = countryCode;
            }
            catch (Exception)
            {
                return new ErrorDataResult<TrainerDto>(_stringLocalizer[Messages.PhoneNumberError]);
            }

            var existingIdentityUser = await _userManager.FindByEmailAsync(trainerCreateDto.Email);
            var createdTrainerResult = existingIdentityUser == null
                ? await CreateTrainerForNewUserAsync(trainerCreateDto)
                : await CreateTrainerForExistingUserAsync(trainerCreateDto, existingIdentityUser);

            return createdTrainerResult;
        }

        /// <summary>
        /// Mevcut bir kullanıcıyı trainer rolüne eklemek için kullanılan private method.
        /// </summary>
        /// <param name="trainerCreateDto">Kullanıcı tarafından gönderilen trainerCreateDto nesnesi.</param>
        /// <param name="existingIdentityUser">AspNetUser tablosunda mevcut olan kullanıcının nesnesi.</param>
        /// <returns>ErrorDataResult<TrainerDto>, SuccessDataResult<Trainer></returns>
        private async Task<DataResult<TrainerDto>> CreateTrainerForExistingUserAsync(TrainerCreateDto trainerCreateDto, IdentityUser existingIdentityUser)
        {
            DataResult<TrainerDto> result = new ErrorDataResult<TrainerDto>(_stringLocalizer[Messages.AddFail]);
            var strategy = await _trainerRepo.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var transactionScope = await _trainerRepo.BeginTransactionAsync();
                try
                {
                    var addToRoleResult = await _userManager.AddToRoleAsync(existingIdentityUser, "Trainer");
                    if (!addToRoleResult.Succeeded)
                        return;

                    var toBeCreatedTrainer = _mapper.Map<Trainer>(trainerCreateDto);
                    toBeCreatedTrainer.IdentityId = existingIdentityUser.Id;
                    var createdTrainer = await _trainerRepo.AddAsync(toBeCreatedTrainer);
                    await _trainerRepo.SaveChangesAsync();
                    await transactionScope.CommitAsync();
                    result = new SuccessDataResult<TrainerDto>(_mapper.Map<TrainerDto>(createdTrainer), _stringLocalizer[Messages.AddSuccess]);
                }
                catch (Exception)
                {
                    await transactionScope.RollbackAsync();
                }
            });
            return result;
        }

        /// <summary>
        /// Sistemde olmayan bir kullanıcıyı trainer olarak sisteme eklemek için kullanılan private method.
        /// </summary>
        /// <param name="trainerCreateDto">Kullanıcı tarafından gönderilen trainerCreateDto nesnesi.</param>
        /// <returns>ErrorDataResult<TrainerDto>, SuccessDataResult<TrainerDto></returns>
        private async Task<DataResult<TrainerDto>> CreateTrainerForNewUserAsync(TrainerCreateDto trainerCreateDto)
        {
            DataResult<TrainerDto> result = new ErrorDataResult<TrainerDto>(_stringLocalizer[Messages.AddFail]);
            var strategy = await _trainerRepo.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var transactionScope = await _trainerRepo.BeginTransactionAsync();
                try
                {
                    IdentityUser identityTrainer = _mapper.Map<IdentityUser>(trainerCreateDto);
                    var randomPassword = AuthenticationHelper.GenerateRandomPassword();
                    var createIdentityUserResult = await _userManager.CreateAsync(identityTrainer, randomPassword);
                    if (!createIdentityUserResult.Succeeded)
                    {
                        return;
                    }
                    var createdIdentityUser = await _userManager.FindByEmailAsync(identityTrainer.Email);
                    var addToRoleResult = await _userManager.AddToRoleAsync(createdIdentityUser, "Trainer");
                    if (!addToRoleResult.Succeeded)
                    {
                        await transactionScope.RollbackAsync();
                        return;
                    }
                    var toBeCreatedTrainer = _mapper.Map<Trainer>(trainerCreateDto);
                    toBeCreatedTrainer.IdentityId = createdIdentityUser.Id;
                    var createdTrainer = await _trainerRepo.AddAsync(toBeCreatedTrainer);
                    await _trainerRepo.SaveChangesAsync();
                    await _emailService.SendEmailAsync(createdTrainer.Email, randomPassword);
                    await transactionScope.CommitAsync();
                    result = new SuccessDataResult<TrainerDto>(_mapper.Map<TrainerDto>(createdTrainer), _stringLocalizer[Messages.AddSuccess]);
                }
                catch (Exception)
                {
                    await transactionScope.RollbackAsync();
                }
            });
            return result;
        }

        /// <summary>
        /// Bu metot ile Trainer silme işlemi yapılmaktadır. Kullanıcının Trainer dışında bir rolü varsa Trainer rolünü kaldırır, sadece Trainer rolüne sahipse kullanıcıyı tamamen siler. Her iki durumda da Trainer tablosundan kayıt silinir.
        /// </summary>
        /// <param name="id">Silinecek Trainer nesnesi için verilen id parametresi</param>
        /// <returns> <see cref="ErrorResult"/> ,<see cref="SuccessResult"/></returns>
        public async Task<Result> DeleteAsync(Guid id)
        {
            var trainerUser = await _trainerRepo.GetByIdAsync(id);
            if (trainerUser == null)
                return new ErrorResult(_stringLocalizer[Messages.TrainerNotFound]);

            var identityUser = await _userManager.FindByIdAsync(trainerUser.IdentityId);
            var userRoles = await _userManager.GetRolesAsync(identityUser);

            var hasDifferentRoleFromTrainer = userRoles.Any(role => role != "Trainer");
            var result = hasDifferentRoleFromTrainer == true ? await DeleteTrainerRoleFromUserAsync(identityUser, trainerUser) : await DeleteUserAsync(identityUser, trainerUser);

            return result;
        }

        /// <summary>
        /// Birden fazla rolü olan kullanıcıların Trainer rolünü ve Trainer tablosundan verisini silmek için kullanılan method.
        /// </summary>
        /// <param name="identityUser"></param>
        /// <param name="adminUser"></param>
        /// <returns><see cref="ErrorResult"/>,<see cref="SuccessResult"/></returns>
        private async Task<Result> DeleteTrainerRoleFromUserAsync(IdentityUser identityUser, Trainer trainerUser)
        {
            Result result = new ErrorResult(_stringLocalizer[Messages.DeleteFail]);

            using var transactionScope = await _trainerRepo.BeginTransactionAsync();
            try
            {
                var deleteTrainerTask = _trainerRepo.DeleteAsync(trainerUser);
                var deleteTrainerRoleTask = _userManager.RemoveFromRoleAsync(identityUser, "Trainer");
                var tasks = new List<Task> { deleteTrainerTask, deleteTrainerRoleTask };
                await Task.WhenAll(tasks);
                await _trainerRepo.SaveChangesAsync();
                await transactionScope.CommitAsync();
                result = new SuccessResult(_stringLocalizer[Messages.DeleteSuccess]);
            }
            catch (Exception)
            {
                await transactionScope.RollbackAsync();
            }
            return result;
        }

        /// <summary>
        /// Sadece Trainer rolü olan kullanıcıyı AspNetUser ve Trainer tablosundan silen method.
        /// </summary>
        /// <param name="identityUser"></param>
        /// <param name="adminUser"></param>
        /// <returns><see cref="ErrorResult"/>,<see cref="SuccessResult"/></returns>
        private async Task<Result> DeleteUserAsync(IdentityUser identityUser, Trainer trainerUser)
        {
            Result result = new ErrorResult(_stringLocalizer[Messages.DeleteFail]);

            using var transactionScope = await _trainerRepo.BeginTransactionAsync();
            try
            {
                var deleteTrainerTask = _trainerRepo.DeleteAsync(trainerUser);
                var deleteUserTask = _userManager.DeleteAsync(identityUser);
                var tasks = new List<Task> { deleteTrainerTask, deleteUserTask };
                await Task.WhenAll(tasks);
                await _trainerRepo.SaveChangesAsync();
                await transactionScope.CommitAsync();
                result = new SuccessResult(_stringLocalizer[Messages.DeleteSuccess]);
            }
            catch (Exception)
            {
                await transactionScope.RollbackAsync();
            }
            return result;
        }

        /// <summary>
        /// GetByIdAsync() metodu database de kayıtlı id si verilen Trainer'i çeker ve TrainerDto'ya Map'leyerek TrainerDto nesnesine çevirir. En son olarak bu nesneyi ve işlemin durumuna göre verilmek istenen mesajı birlikte döner.
        /// </summary>
        /// /// <param name="id">detayları getirilmek istenen trainer nesnesinin Guid tipinde Id si </param>
        /// <returns>SuccessDataResult<TrainerDto>, ErrorDataResult<TrainerDto></returns> 
        public async Task<IResult> GetByIdAsync(Guid id)
        {
            var trainer = await _trainerRepo.GetByIdAsync(id);
            if (trainer == null)
                return new ErrorDataResult<TrainerDto>(_stringLocalizer[Messages.TrainerNotFound]);
            var country = await _phoneNumberManager.GetCountryByCountryCodeAsync(trainer.CountryCode);

            var trainerDto = _mapper.Map<TrainerDto>(trainer);

            trainerDto.Country = country;
            return new SuccessDataResult<TrainerDto>(trainerDto, _stringLocalizer[Messages.FoundSuccess]);
        }

        /// <summary>
        ///  Bu metot Adminin,tüm Trainer nesnelerini listelemesini sağlamaktadır.
        /// </summary>        
        /// <returns>SuccessDataResult<List<TrainerListDto>>, ErrorDataResult<List<TrainerListDto>></returns>   
        public async Task<IResult> ListAsync()
        {
            var trainers = await _trainerRepo.GetAllAsync();
            if (trainers.Count() <= 0)
                return new ErrorDataResult<List<TrainerListDto>>(_stringLocalizer[Messages.ListHasNoElements]);

            return new SuccessDataResult<List<TrainerListDto>>(_mapper.Map<List<TrainerListDto>>(trainers), _stringLocalizer[Messages.ListedSuccess]);
        }

        /// <summary>
        /// Bu metot Trainer nesnesini güncelleme işlemini yapmaktadır.
        /// </summary>
        /// <param name="trainerUpdateDto"></param>
        /// <returns>SuccessResult, ErrorResult</returns>
        public async Task<IResult> UpdateAsync(TrainerUpdateDto trainerUpdateDto)
        {
            using (var transactionScope = await _trainerRepo.BeginTransactionAsync())
            {
                try
                {
                    var trainer = await _trainerRepo.GetByIdAsync(trainerUpdateDto.Id);
                    if (trainer == null)
                        return new ErrorResult(_stringLocalizer[Messages.TrainerNotFound]);

                    try
                    {
                        var phoneNumber = await _phoneNumberManager.PhoneNumberFormaterAsync(trainerUpdateDto.PhoneNumber, trainerUpdateDto.Country);
                        var countryCode = await _phoneNumberManager.GetCountryCodeAsync(trainerUpdateDto.Country);
                        trainerUpdateDto.PhoneNumber = phoneNumber;
                        trainerUpdateDto.CountryCode = countryCode;
                    }
                    catch (Exception)
                    {
                        return new ErrorResult(_stringLocalizer[Messages.PhoneNumberError]);
                    }
                    var tobeUpdated = _mapper.Map(trainerUpdateDto, trainer);

                    await _trainerRepo.UpdateAsync(tobeUpdated);
                    await _trainerRepo.SaveChangesAsync();

                    var identityUser = await _userManager.FindByIdAsync(tobeUpdated.IdentityId);
                    _mapper.Map(tobeUpdated, identityUser);
                    await _userManager.UpdateAsync(identityUser);
                    await transactionScope.CommitAsync();
                    return new SuccessResult(_stringLocalizer[Messages.UpdateSuccess]);
                }
                catch (Exception ex)
                {
                    await transactionScope.RollbackAsync();
                    return new ErrorResult(_stringLocalizer[Messages.UpdateFail] + " " + ex.Message);
                }
                finally
                {
                    await transactionScope.DisposeAsync();
                }
            }
        }

        public async Task<IResult> DeletedListAsync()
        {
            var trainers = await _trainerRepo.GetAllDeletedAsync();
            if (trainers.Count() <= 0)
                return new ErrorDataResult<List<TrainerDeletedListDto>>(_stringLocalizer[Messages.ListHasNoElements]);

            return new SuccessDataResult<List<TrainerDeletedListDto>>(_mapper.Map<List<TrainerDeletedListDto>>(trainers), _stringLocalizer[Messages.ListedSuccess]);
        }

        public async Task<IResult> ActiveListAsync()
        {
            var trainers = await _trainerRepo.GetAllAsync(x => x.Status == Status.Active);
            if (trainers.Count() <= 0)
                return new ErrorDataResult<List<TrainerListDto>>(_stringLocalizer[Messages.ListHasNoElements]);

            return new SuccessDataResult<List<TrainerListDto>>(_mapper.Map<List<TrainerListDto>>(trainers), _stringLocalizer[Messages.ListedSuccess]);
        }

        public async Task<IResult> UpdateActiveAsync(TrainerActiveUpdateDto trainerActiveUpdateDto)
        {
            int classroomCount = 0;
            Trainer? trainer = await _trainerRepo.GetByIdAsync(trainerActiveUpdateDto.Id);

            if (trainer == null)
                return new ErrorResult(_stringLocalizer[Messages.TrainerNotFound]);
            else if (trainer.Status == trainerActiveUpdateDto.Status)
                return new ErrorResult(_stringLocalizer[Messages.TrainerActivityNotChanged]);

            var classroomTrainer = await _classroomTrainersRepository.GetAllAsync(x => x.TrainerId == trainer.Id);
            if (classroomTrainer.Count() > 0)
            {
                foreach (var item in classroomTrainer)
                {
                    Classroom? classroom = await _classroomRepository.GetByIdAsync(item.ClassroomId);

                    if (classroom == null || classroom.Status == Status.Active)
                    {
                        item.Status = Status.Active;
                        classroomCount++;
                    }
                    await _classroomTrainersRepository.UpdateAsync(item);
                }
                await _classroomTrainersRepository.SaveChangesAsync();
            }

            Trainer? newTrainer = _mapper.Map(trainerActiveUpdateDto, trainer);
            ActivityStateLogCreateDto? activityStateLogCreateDto = _mapper.Map<ActivityStateLogCreateDto>(trainerActiveUpdateDto);

            await _trainerRepo.UpdateAsync(newTrainer);
            await _activityStateLogService.CreateAsync(activityStateLogCreateDto);
            await _trainerRepo.SaveChangesAsync();

            return new SuccessResult(Messages.UpdateSuccess + $" etkilenen ClassroomTrainer = {classroomCount}");
        }
        public async Task<IResult> GetByIdentityId(string identityId)
        {
            var hasTrainer = await _trainerRepo.GetByIdentityId(identityId);
            if (hasTrainer == null)
                return new ErrorDataResult<TrainerDto>(_stringLocalizer[Messages.TrainerNotFound]);
            return new SuccessDataResult<TrainerDto>(_mapper.Map<TrainerDto>(hasTrainer), _stringLocalizer[Messages.FoundSuccess]);
        }


        /// <summary>
        /// Bu metot sisteme giriş yapan trainer rolündeki kullanıcı nesnesini getirmek için kullanılır.
        /// </summary>
        /// <param name="currentUserId"></param>
        /// <returns>ErrorDataResult<TrainerDetailsDto>,SuccessDataResult<TrainerDetailsDto></returns>

        public async Task<IResult> GetCurrentTrainerAsync(string currentUserId)
        {

            var trainer = await _trainerRepo.GetByIdentityId(currentUserId);
            if (trainer == null)
                return new ErrorDataResult<TrainerDetailsDto>(_stringLocalizer[Messages.TrainerNotFound]);

            var trainerDto = _mapper.Map<TrainerDetailsDto>(trainer);
            return new SuccessDataResult<TrainerDetailsDto>(trainerDto, _stringLocalizer[Messages.UserDetailsViewSuccess]);
        }

        /// <summary>
        /// Bu metot giriş yapan trainer rolündeki kullanıcının kendi bilgilerini güncellemesi için kullanılır.
        /// </summary>
        /// <param name="trainerUpdateDto">Güncellemesi yapılacak olan verileri içeren TrainerCurrentUserUpdateDto nesnesi</param>
        /// <returns>ErrorResult,SuccessResult</returns>
        public async Task<IResult> UpdateCurrentTrainerAsync(TrainerCurrentUserUpdateDto trainerUpdateDto)
        {


            var hasTrainer = await _trainerRepo.GetByIdentityId(trainerUpdateDto.IdentityId);
            if (hasTrainer == null)
                return new ErrorResult(_stringLocalizer[Messages.TrainerNotFound]);

            try
            {

                var phoneNumber = await _phoneNumberManager.PhoneNumberFormaterAsync(trainerUpdateDto.PhoneNumber, trainerUpdateDto.CountryCode);
                var countryCode = await _phoneNumberManager.GetCountryCodeAsync(trainerUpdateDto.CountryCode);
                trainerUpdateDto.PhoneNumber = phoneNumber;
                trainerUpdateDto.CountryCode = countryCode;
            }
            catch (Exception)
            {
                return new ErrorResult(_stringLocalizer[Messages.PhoneNumberError]);
            }

            if (string.IsNullOrEmpty(trainerUpdateDto.Image))
                trainerUpdateDto.Image = hasTrainer.Image;

            var updatedTrainer = _mapper.Map(trainerUpdateDto, hasTrainer);

            await _trainerRepo.UpdateAsync(updatedTrainer);
            await _trainerRepo.SaveChangesAsync();

            
            return new SuccessResult(_stringLocalizer[Messages.UpdateSuccess]);
        }



    }
}


