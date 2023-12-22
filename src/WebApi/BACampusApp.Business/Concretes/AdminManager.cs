using BACampusApp.Dtos.Admin;
using BACampusApp.Dtos.Trainers;
using BACampusApp.Entities.DbSets;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BACampusApp.Business.Concretes
{
    public class AdminManager : IAdminService
    {
        private readonly IAdminRepository _adminRepo;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IPhoneNumberService _phoneNumberManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IStringLocalizer<Resource> _stringLocalizer;

        public AdminManager(IAdminRepository adminRepo, IMapper mapper, IEmailService emailService, IPhoneNumberService phoneNumberService, UserManager<IdentityUser> userManager, IStringLocalizer<Resource> stringLocalizer)
        {
            _adminRepo = adminRepo;
            _mapper = mapper;
            _emailService = emailService;
            _phoneNumberManager = phoneNumberService;
            _userManager = userManager;
            _stringLocalizer = stringLocalizer;
        }

        /// <summary>
        /// Bu metot Admin rolünde yeni bir kullanıcı eklemek için kullanılmaktadır.
        /// </summary>
        /// <param name="adminCreateDto">Kullanıcı tarafından gönderilen AdminCreateDto nesnesi.</param>
        /// <returns>ErrorDataResult<AdminDto>, SuccessDataResult<AdminDto></returns>
        public async Task<IResult> CreateAsync(AdminCreateDto adminCreateDto)
        {
            bool isExistingEmail = await _adminRepo.AnyAsync(x => x.Email == adminCreateDto.Email);
            if (isExistingEmail)
                return new ErrorDataResult<AdminDto>(_stringLocalizer[Messages.AdminAlreadyExists]);

            try
            {
                var phoneNumber = await _phoneNumberManager.PhoneNumberFormaterAsync(adminCreateDto.PhoneNumber, adminCreateDto.Country);
                var countryCode = await _phoneNumberManager.GetCountryCodeAsync(adminCreateDto.Country);
                adminCreateDto.PhoneNumber = phoneNumber;
                adminCreateDto.CountryCode = countryCode;
            }
            catch (Exception)
            {
                return new ErrorDataResult<AdminDto>(_stringLocalizer[Messages.PhoneNumberError]);
            }

            var existingIdentityUser = await _userManager.FindByEmailAsync(adminCreateDto.Email);
            var createdAdminResult = existingIdentityUser == null
                ? await CreateAdminForNewUserAsync(adminCreateDto)
                : await CreateAdminForExistingUserAsync(adminCreateDto, existingIdentityUser);

            return createdAdminResult;
        }

        /// <summary>
        /// Mevcut bir kullanıcıyı admin rolüne eklemek için kullanılan private method.
        /// </summary>
        /// <param name="adminCreateDto">Kullanıcı tarafından gönderilen AdminCreateDto nesnesi.</param>
        /// <param name="existingIdentityUser">AspNetUser tablosunda mevcut olan kullanıcının nesnesi.</param>
        /// <returns>ErrorDataResult<AdminDto>, SuccessDataResult<AdminDto></returns>
        private async Task<DataResult<AdminDto>> CreateAdminForExistingUserAsync(AdminCreateDto adminCreateDto, IdentityUser existingIdentityUser)
        {
            DataResult<AdminDto> result = new ErrorDataResult<AdminDto>(_stringLocalizer[Messages.AddFail]);
            var strategy = await _adminRepo.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var transactionScope = await _adminRepo.BeginTransactionAsync();
                try
                {
                    var addToRoleResult = await _userManager.AddToRoleAsync(existingIdentityUser, "Admin");
                    if (!addToRoleResult.Succeeded)
                        return;

                    var toBeCreatedAdmin = _mapper.Map<Admin>(adminCreateDto);
                    toBeCreatedAdmin.IdentityId = existingIdentityUser.Id;
                    var createdAdmin = await _adminRepo.AddAsync(toBeCreatedAdmin);
                    await _adminRepo.SaveChangesAsync();
                    await transactionScope.CommitAsync();
                    result = new SuccessDataResult<AdminDto>(_mapper.Map<AdminDto>(createdAdmin), _stringLocalizer[Messages.AddSuccess]);
                }
                catch (Exception)
                {
                    await transactionScope.RollbackAsync();
                }
            });
            return result;
        }

        /// <summary>
        /// Sistemde olmayan bir kullanıcıyı admin olarak sisteme eklemek için kullanılan private method.
        /// </summary>
        /// <param name="adminCreateDto">Kullanıcı tarafından gönderilen AdminCreateDto nesnesi.</param>
        /// <returns>ErrorDataResult<AdminDto>, SuccessDataResult<AdminDto></returns>
        private async Task<DataResult<AdminDto>> CreateAdminForNewUserAsync(AdminCreateDto adminCreateDto)
        {
            DataResult<AdminDto> result = new ErrorDataResult<AdminDto>(_stringLocalizer[Messages.AddFail]);
            var strategy = await _adminRepo.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var transactionScope = await _adminRepo.BeginTransactionAsync();
                try
                {
                    IdentityUser identityAdmin = _mapper.Map<IdentityUser>(adminCreateDto);
                    var randomPassword = AuthenticationHelper.GenerateRandomPassword();
                    var createIdentityUserResult = await _userManager.CreateAsync(identityAdmin, randomPassword);
                    if (!createIdentityUserResult.Succeeded)
                    {
                        return;
                    }
                    var createdIdentityUser = await _userManager.FindByEmailAsync(identityAdmin.Email);
                    var addToRoleResult = await _userManager.AddToRoleAsync(createdIdentityUser, "Admin");
                    if (!addToRoleResult.Succeeded)
                    {
                        await transactionScope.RollbackAsync();
                        return;
                    }
                    var toBeCreatedAdmin = _mapper.Map<Admin>(adminCreateDto);
                    toBeCreatedAdmin.IdentityId = createdIdentityUser.Id;
                    var createdAdmin = await _adminRepo.AddAsync(toBeCreatedAdmin);
                    await _adminRepo.SaveChangesAsync();
                    await _emailService.SendEmailAsync(createdAdmin.Email, randomPassword);
                    await transactionScope.CommitAsync();
                    result = new SuccessDataResult<AdminDto>(_mapper.Map<AdminDto>(createdAdmin), _stringLocalizer[Messages.AddSuccess]);
                }
                catch (Exception)
                {
                    await transactionScope.RollbackAsync();
                }
            });
            return result;
        }

        /// <summary>
        /// Bu metot ile admin silme işlemi yapılmaktadır. Kullanıcının Admin dışında bir rolü varsa Admin rolünü kaldırır, sadece admin rolüne sahipse kullanıcıyı tamamen siler. Her iki durumda da Admin tablosundan kayıt silinir.
        /// </summary>
        /// <param name="id">Silinecek Admin nesnesi için verilen id parametresi</param>
        /// <returns> <see cref="ErrorResult"/> ,<see cref="SuccessResult"/></returns>
        public async Task<Result> DeleteAsync(Guid id)
        {
            var adminUser = await _adminRepo.GetByIdAsync(id);
            if (adminUser == null)
                return new ErrorResult(_stringLocalizer[Messages.AdminNotFound]);

            var identityUser = await _userManager.FindByIdAsync(adminUser.IdentityId);
            var userRoles = await _userManager.GetRolesAsync(identityUser);

            var hasDifferentRoleFromAdmin = userRoles.Any(role => role != "Admin");
            var result = hasDifferentRoleFromAdmin == true ? await DeleteAdminRoleFromUserAsync(identityUser, adminUser) : await DeleteUserAsync(identityUser, adminUser);

            return result;
        }

        /// <summary>
        /// Birden fazla rolü olan kullanıcıların Admin rolünü ve Admin tablosundan verisini silmek için kullanılan method.
        /// </summary>
        /// <param name="identityUser"></param>
        /// <param name="adminUser"></param>
        /// <returns><see cref="ErrorResult"/>,<see cref="SuccessResult"/></returns>
        private async Task<Result> DeleteAdminRoleFromUserAsync(IdentityUser identityUser, Admin adminUser)
        {
            Result result = new ErrorResult(_stringLocalizer[Messages.DeleteFail]);

            using var transactionScope = await _adminRepo.BeginTransactionAsync();
            try
            {
                var deleteAdminTask = _adminRepo.DeleteAsync(adminUser);
                var deleteAdminRoleTask = _userManager.RemoveFromRoleAsync(identityUser, "Admin");
                var tasks = new List<Task> { deleteAdminTask, deleteAdminRoleTask };
                await Task.WhenAll(tasks);
                await _adminRepo.SaveChangesAsync();
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
        /// Sadece Admin rolü olan kullanıcıyı AspNetUser ve Admin tablosundan silen method.
        /// </summary>
        /// <param name="identityUser"></param>
        /// <param name="adminUser"></param>
        /// <returns><see cref="ErrorResult"/>,<see cref="SuccessResult"/></returns>
        private async Task<Result> DeleteUserAsync(IdentityUser identityUser, Admin adminUser)
        {
            Result result = new ErrorResult(_stringLocalizer[Messages.DeleteFail]);

            using var transactionScope = await _adminRepo.BeginTransactionAsync();
            try
            {
                var deleteAdminTask = _adminRepo.DeleteAsync(adminUser);
                var deleteUserTask = _userManager.DeleteAsync(identityUser);
                var tasks = new List<Task> { deleteAdminTask, deleteUserTask };
                await Task.WhenAll(tasks);
                await _adminRepo.SaveChangesAsync();
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
        /// Bu metot ile verilen AdminUpdateDto ile id'si eşleşen Admin nesnesinin güncelleme işlemi yapılmaktadır.
        /// </summary>
        /// <param name="adminUpdateDto">Güncellemesi yapılacak olan verileri içeren AdminUpdateDto nesnesi</param>
        /// <returns>ErrorResult,SuccessResult</returns>
        public async Task<IResult> UpdateAsync(AdminUpdateDto adminUpdateDto)
        {
            using (var transactionScope = await _adminRepo.BeginTransactionAsync())
            {
                try
                {
                    var hasAdmin = await _adminRepo.GetByIdAsync(adminUpdateDto.Id);
                    if (hasAdmin == null)
                        return new ErrorResult(_stringLocalizer[Messages.AdminNotFound]);

                    try
                    {
                        var phoneNumber = await _phoneNumberManager.PhoneNumberFormaterAsync(adminUpdateDto.PhoneNumber, adminUpdateDto.Country);
                        var countryCode = await _phoneNumberManager.GetCountryCodeAsync(adminUpdateDto.Country);
                        adminUpdateDto.PhoneNumber = phoneNumber;
                        adminUpdateDto.CountryCode = countryCode;
                    }
                    catch (Exception)
                    {
                        return new ErrorResult(_stringLocalizer[Messages.PhoneNumberError]);
                    }
                    var updatedAdmin = _mapper.Map(adminUpdateDto, hasAdmin);

                    await _adminRepo.UpdateAsync(updatedAdmin);
                    await _adminRepo.SaveChangesAsync();

                    var identityUser = await _userManager.FindByIdAsync(updatedAdmin.IdentityId);
                    _mapper.Map(updatedAdmin, identityUser);
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

        /// <summary>
        /// Bu metot verilen id ile eşleşen Admin nesnesinin getirilmesi için kullanılmaktadır.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ErrorDataResult<AdminDto>, SuccessDataResult<AdminDto></returns>
        public async Task<IResult> GetByIdAsync(Guid id)
        {
            var hasAdmin = await _adminRepo.GetByIdAsync(id);
            if (hasAdmin == null)
                return new ErrorDataResult<AdminDto>(_stringLocalizer[Messages.AdminNotFound]);
            var country = await _phoneNumberManager.GetCountryByCountryCodeAsync(hasAdmin.CountryCode);

            var adminDto = _mapper.Map<AdminDto>(hasAdmin);

            adminDto.Country = country;
            return new SuccessDataResult<AdminDto>(adminDto, _stringLocalizer[Messages.FoundSuccess]);
        }

        /// <summary>
        /// Bu metot veritabanına kayıtlı tüm Admin nesnelerinin liste şeklinde getirilmesi için kullanılmaktadır.
        /// </summary>
        /// <returns>ErrorDataResult<IEnumerable<AdminListDto>>,SuccessDataResult<IEnumerable<AdminListDto>></returns>
        public async Task<IResult> GetAllAsync()
        {
            var admins = await _adminRepo.GetAllAsync();
            if (admins.Count() <= 0)
                return new ErrorDataResult<IEnumerable<AdminListDto>>(_stringLocalizer[Messages.ListHasNoElements]);

            return new SuccessDataResult<IEnumerable<AdminListDto>>(_mapper.Map<IEnumerable<AdminListDto>>(admins), _stringLocalizer[Messages.ListedSuccess]);
        }

        public async Task<IResult> GetByIdentityId(string identityId)
        {
            var hasAdmin = await _adminRepo.GetByIdentityId(identityId);
            if (hasAdmin == null)
                return new ErrorDataResult<AdminDto>(_stringLocalizer[Messages.AdminNotFound]);
            return new SuccessDataResult<AdminDto>(_mapper.Map<AdminDto>(hasAdmin), _stringLocalizer[Messages.FoundSuccess]);
        }


        /// <summary>
        /// Bu metot sisteme giriş yapan admin rolündeki kullanıcı nesnesini getirmek için kullanılır.
        /// </summary>
        /// <param name="currentUserId"></param>
        /// <returns>ErrorDataResult<AdminDetailsDto>,SuccessDataResult<AdminDetailsDto></returns>

        public async Task<IResult> GetCurrentAdminDetailsAsync(string currentUserId)
        {

            var admin = await _adminRepo.GetByIdentityId(currentUserId);
            if (admin == null)
                return new ErrorDataResult<AdminDetailsDto>(_stringLocalizer[Messages.AdminNotFound]);

            var adminDto = _mapper.Map<AdminDetailsDto>(admin);
            return new SuccessDataResult<AdminDetailsDto>(adminDto, _stringLocalizer[Messages.UserDetailsViewSuccess]);
        }


        /// <summary>
        /// Bu metot giriş yapan admin rolündeki kullanıcının kendi bilgilerini güncellemesi için kullanılır.
        /// </summary>
        /// <param name="adminUpdateDto">Güncellemesi yapılacak olan verileri içeren AdminCurrentUserUpdateDto nesnesi</param>
        /// <returns>ErrorResult,SuccessResult</returns>
        public async Task<IResult> UpdateCurrentAdminAsync(AdminCurrentUserUpdateDto adminUpdateDto)
        {

            var hasAdmin = await _adminRepo.GetByIdentityId(adminUpdateDto.IdentityId);
            if (hasAdmin == null)
                return new ErrorResult(_stringLocalizer[Messages.AdminNotFound]);

            try
            {
                var phoneNumber = await _phoneNumberManager.PhoneNumberFormaterAsync(adminUpdateDto.PhoneNumber, adminUpdateDto.CountryCode);
                var countryCode = await _phoneNumberManager.GetCountryCodeAsync(adminUpdateDto.CountryCode);
                adminUpdateDto.PhoneNumber = phoneNumber;
                adminUpdateDto.CountryCode = countryCode;
            }
            catch (Exception)
            {
                return new ErrorResult(_stringLocalizer[Messages.PhoneNumberError]);
            }
            if (string.IsNullOrEmpty(adminUpdateDto.Image))
                adminUpdateDto.Image = hasAdmin.Image;

            var updatedAdmin = _mapper.Map(adminUpdateDto, hasAdmin);

            await _adminRepo.UpdateAsync(updatedAdmin);
            await _adminRepo.SaveChangesAsync();

            return new SuccessResult(_stringLocalizer[Messages.UpdateSuccess]);
            
        }

        public async Task<IResult> UpdateLoggedInAdminAsync(AdminLoggedInUserUpdateDto adminLoggedInUserUpdateDto)
        {
            var hasAdmin = await _adminRepo.GetByIdentityId(adminLoggedInUserUpdateDto.IdentityId);
            if (hasAdmin == null)
                return new ErrorResult(_stringLocalizer[Messages.AdminNotFound]);

            try
            {
                var phoneNumber = await _phoneNumberManager.PhoneNumberFormaterAsync(adminLoggedInUserUpdateDto.PhoneNumber, adminLoggedInUserUpdateDto.CountryCode);
                var countryCode = await _phoneNumberManager.GetCountryCodeAsync(adminLoggedInUserUpdateDto.CountryCode);
                adminLoggedInUserUpdateDto.PhoneNumber = phoneNumber;
                adminLoggedInUserUpdateDto.CountryCode = countryCode;
            }
            catch (Exception)
            {
                return new ErrorResult(_stringLocalizer[Messages.PhoneNumberError]);
            }
            if (string.IsNullOrEmpty(adminLoggedInUserUpdateDto.Image))
                adminLoggedInUserUpdateDto.Image = hasAdmin.Image;

            var updatedAdmin = _mapper.Map(adminLoggedInUserUpdateDto, hasAdmin);

            await _adminRepo.UpdateAsync(updatedAdmin);
            await _adminRepo.SaveChangesAsync();

            return new SuccessResult(_stringLocalizer[Messages.UpdateSuccess]);
        }
    }


}





