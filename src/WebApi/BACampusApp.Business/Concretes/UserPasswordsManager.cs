using BACampusApp.Business.Password;

namespace BACampusApp.Business.Concretes
{
    public class UserPasswordsManager : IUserPasswordsService
    {
        private readonly IUserPasswordsRepository _userPasswordsRepository;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<Resource> _stringLocalizer;

        public UserPasswordsManager(IUserPasswordsRepository userPasswordsRepository, IMapper mapper, IStringLocalizer<Resource> stringLocalizer)
        {
            _userPasswordsRepository = userPasswordsRepository;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<IResult> CreateAsync(UserPasswordsCreateDto userPasswordsCreateDto)
        {
            if (userPasswordsCreateDto == null)
            {
                return new ErrorResult(_stringLocalizer[Messages.FailedAddActivityState]);
            }
            userPasswordsCreateDto.Password = PasswordHasher.HashPassword(userPasswordsCreateDto.Password);
            var userPassword = _mapper.Map<UserPasswords>(userPasswordsCreateDto);
            var userPasswords = await _userPasswordsRepository.GetAllAsync(a => a.UserId == userPasswordsCreateDto.UserId);
            bool containsPassword = userPasswords.Any(up => up.Password == userPasswordsCreateDto.Password);
            if (containsPassword)
            {
                return new ErrorResult(_stringLocalizer[Messages.CannotBeOneOfTheLastThreePasswords]);
            }
            if (userPasswords.Count() >= 3)
            {               
                var passwordToDelete = userPasswords.FirstOrDefault();
                await _userPasswordsRepository.DeleteAsync(passwordToDelete);
            }            
            await _userPasswordsRepository.AddAsync(userPassword);
            await _userPasswordsRepository.SaveChangesAsync();
            return new SuccessResult(_stringLocalizer[Messages.AddSuccess]);
        }

        public async Task<IResult> GetAllAsync()
        {
            var userPasswords = await _userPasswordsRepository.GetAllAsync();
            if (userPasswords.Count() <= 0)
                return new ErrorResult(_stringLocalizer[Messages.ListHasNoElements]);
            return new SuccessDataResult<List<UserPasswordsListDto>>(_mapper.Map<List<UserPasswordsListDto>>(userPasswords), _stringLocalizer[Messages.ListedSuccess]);
        }

        public async Task<IResult> GetAllByUserIdAsync(Guid id)
        {
            var userPasswords = await _userPasswordsRepository.GetAllAsync(a => a.UserId == id);
            if (userPasswords.Count() <= 0)
                return new ErrorResult(_stringLocalizer[Messages.ListHasNoElements]);
            return new SuccessDataResult<List<UserPasswordsListDto>>(_mapper.Map<List<UserPasswordsListDto>>(userPasswords), _stringLocalizer[Messages.ListedSuccess]);
        }

        public async Task<IResult> GetByIdAsync(Guid id)
        {
            var userPasswords = await _userPasswordsRepository.GetByIdAsync(id);
            if (userPasswords == null)
                return new ErrorResult(_stringLocalizer[Messages.UserPasswordsNotFound]);
            return new SuccessDataResult<UserPasswordsListDto>(_mapper.Map<UserPasswordsListDto>(userPasswords), _stringLocalizer[Messages.FoundSuccess]);
        }
    }
}
