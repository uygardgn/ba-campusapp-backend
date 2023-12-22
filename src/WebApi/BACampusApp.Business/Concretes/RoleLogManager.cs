namespace BACampusApp.Business.Concretes
{
    public class RoleLogManager : IRoleLogService
    {
        private readonly IRoleLogRepository _roleLogRepo;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        public RoleLogManager(IRoleLogRepository roleLogRepo, IMapper mapper, UserManager<IdentityUser> userManager, IStringLocalizer<Resource> stringLocalizer)
        {
            _roleLogRepo = roleLogRepo;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }
        public async Task<IResult> CreateAsync(RoleLogCreateDto roleLogCreateDto)
        {
            if (roleLogCreateDto == null)
            {
                return new ErrorResult(_stringLocalizer[Messages.FailedAddActivityState]);
            }
            var roleLog = _mapper.Map<RoleLog>(roleLogCreateDto);
            await _roleLogRepo.AddAsync(roleLog);
            await _roleLogRepo.SaveChangesAsync();
            return new SuccessResult(_stringLocalizer[Messages.AddSuccess]);
        }

        public async Task<IResult> GetAllAsync()
        {
            var roleLog = await _roleLogRepo.GetAllAsync();
            if (roleLog.Count() <= 0)
                return new ErrorResult(_stringLocalizer[Messages.ListHasNoElements]);
            return new SuccessDataResult<List<RoleLogDto>>(_mapper.Map<List<RoleLogDto>>(roleLog), _stringLocalizer[Messages.ListedSuccess]);
        }

        public async Task<IResult> GetAllByUserIdAsync(Guid id)
        {
            var roleLog = await _roleLogRepo.GetAllAsync(a => a.CreatedBy == id.ToString());
            if (roleLog.Count() <= 0)
                return new ErrorResult(_stringLocalizer[Messages.ListHasNoElements]);
            return new SuccessDataResult<List<RoleLogDto>>(_mapper.Map<List<RoleLogDto>>(roleLog), _stringLocalizer[Messages.ListedSuccess]);
        }

        public async Task<IResult> GetByIdAsync(Guid id)
        {
            var roleLog = await _roleLogRepo.GetByIdAsync(id);
            if (roleLog == null)
                return new ErrorResult(_stringLocalizer[Messages.RoleLogNotFound]);
            return new SuccessDataResult<RoleLogDto>(_mapper.Map<RoleLogDto>(roleLog), _stringLocalizer[Messages.FoundSuccess]);
        }

        public async Task<IResult> GetLastRoleLogByUserIdAsync(Guid id)
        {
            var roleLog = await _roleLogRepo.GetAllAsync(a => a.CreatedBy == id.ToString());
            if (roleLog.Count() <= 0)
                return new SuccessResult(_stringLocalizer[Messages.RoleLogNotFound]);
            var roleLogList = _mapper.Map<List<RoleLogDto>>(roleLog);
            return new SuccessDataResult<RoleLogDto>(roleLogList.LastOrDefault(), _stringLocalizer[Messages.FoundSuccess]);
        }
    }
}