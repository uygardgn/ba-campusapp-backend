namespace BACampusApp.Business.Concretes
{
    public class StudentLogTableManager : IStudentLogTableService
    {
        private readonly IMapper _mapper;
        private readonly IStudentLogTableRepository _studentLogTableRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        public StudentLogTableManager(IMapper mapper, IStudentLogTableRepository studentLogTableRepository, UserManager<IdentityUser> userManager, IStringLocalizer<Resource> stringLocalizer)
        {
            _mapper = mapper;
            _studentLogTableRepository = studentLogTableRepository;
            _userManager = userManager;
            _stringLocalizer = stringLocalizer;
        }


        public async Task<IResult> CreateAsync(StudentLogTableCreateDto studentLogTableCreateDto)
        {
            if (studentLogTableCreateDto == null)
                return new ErrorResult(_stringLocalizer[Messages.FailedAddActivityState]);

            var studentLog = _mapper.Map<StudentLogTable>(studentLogTableCreateDto);
            await _studentLogTableRepository.AddAsync(studentLog);
            await _studentLogTableRepository.SaveChangesAsync();
            return new SuccessResult(_stringLocalizer[Messages.AddSuccess]);
        }
        public async Task<IResult> GetByIdAsync(Guid id)
        {
            var studentLog = await _studentLogTableRepository.GetByIdAsync(id);
            if (studentLog == null)
            {
                return new ErrorResult(_stringLocalizer[Messages.StudentLogNotFound]);
            }
            return new SuccessDataResult<StudentLogTableDto>(_mapper.Map<StudentLogTableDto>(studentLog), _stringLocalizer[Messages.FoundSuccess]);
        }
        public async Task<IResult> ListAsync()
        {
            var studentLog = await _studentLogTableRepository.GetAllAsync();
            if (studentLog.Count() <= 0) return new ErrorResult(_stringLocalizer[Messages.ListHasNoElements]);
            return new SuccessDataResult<List<StudentLogTableDto>>(_mapper.Map<List<StudentLogTableDto>>(studentLog), _stringLocalizer[Messages.ListedSuccess]);
        }
    }
}
