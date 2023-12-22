using BACampusApp.Dtos.Admin;
using BACampusApp.Dtos.Trainers;
using BACampusApp.Entities.DbSets;

namespace BACampusApp.Business.Concretes
{
    public class StudentManager : IStudentService
    {
        private readonly IStudentRepository _studentRepo;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IPhoneNumberService _phoneNumberManager;
        private readonly IActivityStateLogSevices _activityStateLogService;
        private readonly IClassroomStudentRepository _classroomStudentRepo;
        private readonly IClassroomRepository _classroomRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IStringLocalizer<Resource> _stringLocalizer;

        public StudentManager(IStudentRepository studentRepo, IMapper mapper, IEmailService emailService, IPhoneNumberService phoneNumberService, IActivityStateLogSevices activityStateLogService, IClassroomStudentRepository classroomStudentRepository, IClassroomRepository classroomRepository, UserManager<IdentityUser> userManager, IStringLocalizer<Resource> stringLocalizer)
        {
            _studentRepo = studentRepo;
            _mapper = mapper;
            _emailService = emailService;
            _phoneNumberManager = phoneNumberService;
            _activityStateLogService = activityStateLogService;
            _classroomStudentRepo = classroomStudentRepository;
            _classroomRepository = classroomRepository;
            _userManager = userManager;
            _stringLocalizer = stringLocalizer;
        }

        /// <summary>
        /// Bu metot Student nesnesini veritabanında oluşturur.
        /// </summary>
        /// <param name="studentCreateDto">StudentCreateDto tipinde yeniÖğrenci parametresini alır </param>
        /// <returns>SuccessDataResult<StudentDto>, ErrorDataResult<StudentDto></returns>
        public async Task<IResult> CreateAsync(StudentCreateDto studentCreateDto)
        {
            var isExistingEmail = await _studentRepo.AnyAsync(x => x.Email == studentCreateDto.Email);
            if (isExistingEmail)
                return new ErrorDataResult<StudentDto>(_stringLocalizer[Messages.SameEmailAddressAlreadyUsed]);

            try
            {
                var phoneNumber = await _phoneNumberManager.PhoneNumberFormaterAsync(studentCreateDto.PhoneNumber, studentCreateDto.Country);
                var countryCode = await _phoneNumberManager.GetCountryCodeAsync(studentCreateDto.Country);
                studentCreateDto.PhoneNumber = phoneNumber;
                studentCreateDto.CountryCode = countryCode;

            }
            catch (Exception)
            {
                return new ErrorDataResult<StudentDto>(_stringLocalizer[Messages.PhoneNumberError]);
            }

            //Guid guid = Guid.Empty;
            //guid = new Guid(studentCreateDto.CreatedBy);
            //var admin = await _adminRepository.GetByIdAsync(guid);

            //if (studentCreateDto.Email == admin.Email)
            //{
            //    return new ErrorResult(Messages.PermissionAuthorizationError);
            //}
            var existingIdentityUser = await _userManager.FindByEmailAsync(studentCreateDto.Email);
            var createdStudentResult = existingIdentityUser == null
                ? await CreateStudentForNewUserAsync(studentCreateDto)
                : await CreateStudentForExistingUserAsync(studentCreateDto, existingIdentityUser);

            return createdStudentResult;
        }

        /// <summary>
        /// Mevcut bir kullanıcıyı student rolüne eklemek için kullanılan private method.
        /// </summary>
        /// <param name="studentCreateDto">Kullanıcı tarafından gönderilen studentCreateDto nesnesi.</param>
        /// <param name="existingIdentityUser">AspNetUser tablosunda mevcut olan kullanıcının nesnesi.</param>
        /// <returns>ErrorDataResult<StudentDto>, SuccessDataResult<StudentDto></returns>
        private async Task<DataResult<StudentDto>> CreateStudentForExistingUserAsync(StudentCreateDto studentCreateDto, IdentityUser existingIdentityUser)
        {
            DataResult<StudentDto> result = new ErrorDataResult<StudentDto>(_stringLocalizer[Messages.AddFail]);
            var strategy = await _studentRepo.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var transactionScope = await _studentRepo.BeginTransactionAsync();
                try
                {
                    var addToRoleResult = await _userManager.AddToRoleAsync(existingIdentityUser, "Student");
                    if (!addToRoleResult.Succeeded)
                        return;

                    var toBeCreatedStudent = _mapper.Map<Student>(studentCreateDto);
                    toBeCreatedStudent.IdentityId = existingIdentityUser.Id;
                    var createdStudent = await _studentRepo.AddAsync(toBeCreatedStudent);
                    await _studentRepo.SaveChangesAsync();
                    await transactionScope.CommitAsync();
                    result = new SuccessDataResult<StudentDto>(_mapper.Map<StudentDto>(createdStudent), _stringLocalizer[Messages.AddSuccess]);
                }
                catch (Exception)
                {
                    await transactionScope.RollbackAsync();
                }
            });
            return result;
        }

        /// <summary>
        /// Sistemde olmayan bir kullanıcıyı student olarak sisteme eklemek için kullanılan private method.
        /// </summary>
        /// <param name="studentCreateDto">Kullanıcı tarafından gönderilen studentCreateDto nesnesi.</param>
        /// <returns>ErrorDataResult<StudentDto>, SuccessDataResult<StudentDto></returns>
        private async Task<DataResult<StudentDto>> CreateStudentForNewUserAsync(StudentCreateDto studentCreateDto)
        {
            DataResult<StudentDto> result = new ErrorDataResult<StudentDto>(_stringLocalizer[Messages.AddFail]);
            var strategy = await _studentRepo.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var transactionScope = await _studentRepo.BeginTransactionAsync();
                try
                {
                    IdentityUser identityStudent = _mapper.Map<IdentityUser>(studentCreateDto);
                    var randomPassword = AuthenticationHelper.GenerateRandomPassword();
                    var createIdentityUserResult = await _userManager.CreateAsync(identityStudent, randomPassword);
                    if (!createIdentityUserResult.Succeeded)
                    {
                        return;
                    }
                    var createdIdentityUser = await _userManager.FindByEmailAsync(identityStudent.Email);
                    var addToRoleResult = await _userManager.AddToRoleAsync(createdIdentityUser, "Student");
                    if (!addToRoleResult.Succeeded)
                    {
                        await transactionScope.RollbackAsync();
                        return;
                    }
                    var toBeCreatedStudent = _mapper.Map<Student>(studentCreateDto);
                    toBeCreatedStudent.IdentityId = createdIdentityUser.Id;
                    var createdStudent = await _studentRepo.AddAsync(toBeCreatedStudent);
                    await _studentRepo.SaveChangesAsync();
                    await _emailService.SendEmailAsync(createdStudent.Email, randomPassword);
                    await transactionScope.CommitAsync();
                    result = new SuccessDataResult<StudentDto>(_mapper.Map<StudentDto>(createdStudent), _stringLocalizer[Messages.AddSuccess]);
                }
                catch (Exception)
                {
                    await transactionScope.RollbackAsync();
                }
            });
            return result;
        }

        /// <summary>
        /// Bu metot ile Student silme işlemi yapılmaktadır. Kullanıcının Student dışında bir rolü varsa Student rolünü kaldırır, sadece Student rolüne sahipse kullanıcıyı tamamen siler. Her iki durumda da Student tablosundan kayıt silinir.
        /// </summary>
        /// <param name="id">Silinecek Student nesnesi için verilen id parametresi</param>
        /// <returns> <see cref="ErrorResult"/> ,<see cref="SuccessResult"/></returns>
        public async Task<Result> DeleteAsync(Guid id)
        {
            var studentUser = await _studentRepo.GetByIdAsync(id);
            if (studentUser == null)
                return new ErrorResult(_stringLocalizer[Messages.StudentNotFound]);

            var identityUser = await _userManager.FindByIdAsync(studentUser.IdentityId);
            var userRoles = await _userManager.GetRolesAsync(identityUser);

            var hasDifferentRoleFromStudent = userRoles.Any(role => role != "Student");
            var result = hasDifferentRoleFromStudent == true ? await DeleteStudentRoleFromUserAsync(identityUser, studentUser) : await DeleteUserAsync(identityUser, studentUser);

            return result;
        }

        /// <summary>
        /// Birden fazla rolü olan kullanıcıların Student rolünü ve Student tablosundan verisini silmek için kullanılan method.
        /// </summary>
        /// <param name="identityUser"></param>
        /// <param name="adminUser"></param>
        /// <returns><see cref="ErrorResult"/>,<see cref="SuccessResult"/></returns>
        private async Task<Result> DeleteStudentRoleFromUserAsync(IdentityUser identityUser, Student studentUser)
        {
            Result result = new ErrorResult(_stringLocalizer[Messages.DeleteFail]);

            using var transactionScope = await _studentRepo.BeginTransactionAsync();
            try
            {
                var deleteStudentTask = _studentRepo.DeleteAsync(studentUser);
                var deleteStudentRoleTask = _userManager.RemoveFromRoleAsync(identityUser, "Student");
                var tasks = new List<Task> { deleteStudentTask, deleteStudentRoleTask };
                await Task.WhenAll(tasks);
                await _studentRepo.SaveChangesAsync();
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
        /// Sadece Student rolü olan kullanıcıyı AspNetUser ve Student tablosundan silen method.
        /// </summary>
        /// <param name="identityUser"></param>
        /// <param name="adminUser"></param>
        /// <returns><see cref="ErrorResult"/>,<see cref="SuccessResult"/></returns>
        private async Task<Result> DeleteUserAsync(IdentityUser identityUser, Student studentUser)
        {
            Result result = new ErrorResult(_stringLocalizer[Messages.DeleteFail]);

            using var transactionScope = await _studentRepo.BeginTransactionAsync();
            try
            {
                var deleteStudentTask = _studentRepo.DeleteAsync(studentUser);
                var deleteUserTask = _userManager.DeleteAsync(identityUser);
                var tasks = new List<Task> { deleteStudentTask, deleteUserTask };
                await Task.WhenAll(tasks);
                await _studentRepo.SaveChangesAsync();
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
        /// Bu metot güncellenecek verileri gönderilen Student nesnesinin veritabanında güncelleme işleminin yapılmasında kullanılmaktadır.
        /// </summary>
        /// <param name="studentUpdateDto">güncellenmek istenen student nesnesinin StudentUpdateDto tipinde entity'si</param>
        /// <returns>ErrorResult, SuccessResult</returns>
        public async Task<IResult> UpdateAsync(StudentUpdateDto studentUpdateDto)
        {
            using (var transactionScope = await _studentRepo.BeginTransactionAsync())
            {
                try
                {
                    var student = await _studentRepo.GetByIdAsync(studentUpdateDto.Id);
                    if (student == null)
                        return new ErrorResult(_stringLocalizer[Messages.StudentNotFound]);

                    try
                    {
                        var phoneNumber = await _phoneNumberManager.PhoneNumberFormaterAsync(studentUpdateDto.PhoneNumber, studentUpdateDto.Country);
                        var countryCode = await _phoneNumberManager.GetCountryCodeAsync(studentUpdateDto.Country);
                        studentUpdateDto.PhoneNumber = phoneNumber;
                        studentUpdateDto.CountryCode = countryCode;
                    }
                    catch (Exception)
                    {
                        return new ErrorResult(_stringLocalizer[Messages.PhoneNumberError]);
                    }

                    var toBeUpdated = _mapper.Map(studentUpdateDto, student);
                    await _studentRepo.UpdateAsync(toBeUpdated);
                    await _studentRepo.SaveChangesAsync();

                    //Identity Update
                    var identityUser = await _userManager.FindByIdAsync(toBeUpdated.IdentityId);
                    _mapper.Map(toBeUpdated, identityUser);
                    await _userManager.UpdateAsync(identityUser);
                    await transactionScope.CommitAsync();
                    return new SuccessResult(_stringLocalizer[Messages.UpdateSuccess]);
                }
                catch (Exception ex)
                {
                    await transactionScope.RollbackAsync();
                    return new ErrorResult(_stringLocalizer[Messages.UpdateFail] + " " + ex.Message);
                }
                finally { await transactionScope.DisposeAsync(); }
            }
        }

        /// <summary>
        /// Bu metot veritabanındaki tüm öğrencileri çeker ve bu öğrenci listesini StudentListDto ile eşleyip çıktısını verir.
        /// </summary>
        /// <returns>SuccessDataResult<StudentListDto>, ErrorDataResult<List<StudentListDto>></returns>
        public async Task<IResult> GetAllAsync()
        {
            var students = await _studentRepo.GetAllAsync();
            if (students.Count() <= 0)
                return new ErrorDataResult<List<StudentListDto>>(_stringLocalizer[Messages.ListHasNoElements]);

            return new SuccessDataResult<List<StudentListDto>>(_mapper.Map<List<StudentListDto>>(students), _stringLocalizer[Messages.ListedSuccess]);
        }

        /// <summary>
        /// GetByIdAsync() metodu database de kayıtlı olan ve id si verilen Student'i çeker ve StudentDto'ya Map'leyerek StudentDto nesnesine çevirir.Bu nesneyi ve işlemin durumuna göre verilmek istenen mesajı ile birlikte döner.
        /// </summary>
        /// <param name="id">Detayları getirilmek istenen student nesnesinin Guid tipinde Id si</param>
        /// <returns>SuccessDataResult<StudentDto>, ErrorDataResult<StudentDto></returns> 
        public async Task<IResult> GetByIdAsync(Guid id)
        {
            var student = await _studentRepo.GetByIdAsync(id);
            if (student == null)
                return new ErrorDataResult<StudentDto>(_stringLocalizer[Messages.StudentNotFound]);
            var country = await _phoneNumberManager.GetCountryByCountryCodeAsync(student.CountryCode);
           
            var classroomStudentAll = await _classroomStudentRepo.GetAllAsync();
            var classroomInfo = classroomStudentAll.FirstOrDefault(x => x.StudentId == id);
            var classroomAll = await _classroomRepository.GetAllAsync();
            var classroomId = classroomAll.FirstOrDefault(x => x.Id == classroomInfo.ClassroomId);
           
            var studentDto = _mapper.Map<StudentDto>(student);
            studentDto.Country = country;
            
            studentDto.ClassroomName = classroomId.Name;

            return new SuccessDataResult<StudentDto>(studentDto, _stringLocalizer[Messages.FoundSuccess]);
        }

        /// <summary>
        ///  Bu metot veritabanındaki tüm aktif öğrencileri çeker ve bu öğrenci listesini StudentListDto ile eşleyip çıktısını verir.
        /// </summary>
        /// <returns>SuccessDataResult<StudentListDto>, ErrorDataResult<StudentListDto></returns>
        public async Task<IResult> ActiveListAsync()
        {
            var students = await _studentRepo.GetAllAsync(x => x.Status == Status.Active);
            if (students.Count() <= 0)
                return new ErrorDataResult<List<StudentListDto>>(_stringLocalizer[Messages.ListHasNoElements]);

            return new SuccessDataResult<List<StudentListDto>>(_mapper.Map<List<StudentListDto>>(students), _stringLocalizer[Messages.ListedSuccess]);
        }

        public async Task<IResult> UpdateActiveAsync(StudentActiveUpdateDto studentActiveUpdateDto)
        {
            int classroomCount = 0;
            Student? student = await _studentRepo.GetByIdAsync(studentActiveUpdateDto.Id);

            if (student == null)
                return new ErrorResult(_stringLocalizer[Messages.StudentNotFound]);
            else if (student.Status == studentActiveUpdateDto.Status)
                return new ErrorResult(_stringLocalizer[Messages.StudentActivityNotChanged]);

            var classroomStudent = await _classroomStudentRepo.GetAllAsync(x => x.StudentId == student.Id);
            if (classroomStudent.Count() > 0)
            {
                foreach (var item in classroomStudent)
                {
                    Classroom? classroom = await _classroomRepository.GetByIdAsync(item.ClassroomId);
                    if (classroom == null || classroom.Status == Status.Active)
                    {
                        item.Status = Status.Active;
                        classroomCount++;
                    }

                    await _classroomStudentRepo.UpdateAsync(item);
                }
                await _classroomStudentRepo.SaveChangesAsync();
            }

            Student? newStudent = _mapper.Map(studentActiveUpdateDto, student);
            ActivityStateLogCreateDto? activityStateLogCreateDto = _mapper.Map<ActivityStateLogCreateDto>(studentActiveUpdateDto);

            await _studentRepo.UpdateAsync(newStudent);
            await _activityStateLogService.CreateAsync(activityStateLogCreateDto);
            await _studentRepo.SaveChangesAsync();

            return new SuccessResult(Messages.UpdateSuccess + $" etkilenen Classroom Student = {classroomCount}");
        }

        public async Task<IResult> DeletedListAsync()
        {
            var student = await _studentRepo.GetAllDeletedAsync();
            if (student.Count() <= 0)
                return new ErrorDataResult<List<StudentDeletedListDto>>(_stringLocalizer[Messages.ListedFail]);

            return new SuccessDataResult<List<StudentDeletedListDto>>(_mapper.Map<List<StudentDeletedListDto>>(student), _stringLocalizer[Messages.ListedSuccess]);
        }


        /// <summary>
        /// Bu metot sisteme giriş yapan student rolündeki kullanıcı nesnesini getirmek için kullanılır.
        /// </summary>
        /// <param name="currentUserId"></param>
        /// <returns>ErrorDataResult<StudentDetailsDto>,SuccessDataResult<StudentDetailsDto></returns>

        public async Task<IResult> GetCurrentStudentDetailsAsync(string currentUserId)
        {

            var student = await _studentRepo.GetByIdentityId(currentUserId);
            if (student == null)
                return new ErrorDataResult<StudentDetailsDto>(_stringLocalizer[Messages.StudentNotFound]);

            var studentDto = _mapper.Map<StudentDetailsDto>(student);
            return new SuccessDataResult<StudentDetailsDto>(studentDto, _stringLocalizer[Messages.UserDetailsViewSuccess]);
        }

        /// <summary>
        /// Bu metot giriş yapan student rolündeki kullanıcının kendi bilgilerini güncellemesi için kullanılır.
        /// </summary>
        /// <param name="studentUpdateDto">Güncellemesi yapılacak olan verileri içeren StudentCurrentUserUpdateDto nesnesi</param>
        /// <returns>ErrorResult,SuccessResult</returns>
        public async Task<IResult> UpdateCurrentStudentAsync(StudentCurrentUserUpdateDto studentUpdateDto)
        {


            var hasStudent = await _studentRepo.GetByIdentityId(studentUpdateDto.IdentityId);
            if (hasStudent == null)
                return new ErrorResult(_stringLocalizer[Messages.StudentNotFound]);

            try
            {
                var phoneNumber = await _phoneNumberManager.PhoneNumberFormaterAsync(studentUpdateDto.PhoneNumber, studentUpdateDto.CountryCode);
                var countryCode = await _phoneNumberManager.GetCountryCodeAsync(studentUpdateDto.CountryCode);
                studentUpdateDto.PhoneNumber = phoneNumber;
                studentUpdateDto.CountryCode = countryCode;
            }
            catch (Exception)
            {
                return new ErrorResult(_stringLocalizer[Messages.PhoneNumberError]);
            }

            if (string.IsNullOrEmpty(studentUpdateDto.Image))
                studentUpdateDto.Image = hasStudent.Image;

            var updatedStudent = _mapper.Map(studentUpdateDto, hasStudent);

            await _studentRepo.UpdateAsync(updatedStudent);
            await _studentRepo.SaveChangesAsync();

            return new SuccessResult(_stringLocalizer[Messages.UpdateSuccess]);
        }

        /// <summary>
        /// Bu metot studentId göre student bilgilerini getirmek için kullanılır.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ErrorDataResult<StudentDetailsDto>,SuccessDataResult<StudentDetailsDto></returns>

        public async Task<IResult> GetDetailsByStudentIdAsync(Guid id)
        {

            var student = await _studentRepo.GetByIdAsync(id);
            if (student == null)
                return new ErrorDataResult<StudentDetailsDto>(_stringLocalizer[Messages.StudentNotFound]);
            var country = await _phoneNumberManager.GetCountryByCountryCodeAsync(student.CountryCode);
            var studentDto = _mapper.Map<StudentDetailsDto>(student);
            studentDto.Country = country;
            return new SuccessDataResult<StudentDetailsDto>(studentDto, _stringLocalizer[Messages.FoundSuccess]);
        }

    }

}

