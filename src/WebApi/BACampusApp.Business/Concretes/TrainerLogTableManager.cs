namespace BACampusApp.Business.Concretes
{
    public class TrainerLogTableManager : ITrainerLogTableService
    {
        private readonly ITrainerLogTableRepository _trainerLogTablerepository;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        public TrainerLogTableManager(IClassroomTrainersRepository classroomTrainersRepository, IMapper mapper, ITrainerLogTableRepository repository, UserManager<IdentityUser> userManager, IStringLocalizer<Resource> stringLocalizer)
        {
            _mapper = mapper;
            _trainerLogTablerepository = repository;
            _userManager = userManager;
            _stringLocalizer = stringLocalizer;
        }
        public async Task<IResult> CreateAsync(TrainerLogTableCreateDto trainerLogTableCreateDto)
        {
            if (trainerLogTableCreateDto == null)
                return new ErrorResult(_stringLocalizer[Messages.FailedAddActivityState]);
            var trainerLog = _mapper.Map<TrainerLogTable>(trainerLogTableCreateDto);
            await _trainerLogTablerepository.AddAsync(trainerLog);
            await _trainerLogTablerepository.SaveChangesAsync();
            return new SuccessResult(_stringLocalizer[Messages.AddSuccess]);
        }
        public async Task<IResult> GetByIdAsync(Guid id)
        {
            var trainerLog = await _trainerLogTablerepository.GetByIdAsync(id);
            if (trainerLog == null) return new ErrorResult(_stringLocalizer[Messages.TrainerLogNotFound]);

            return new SuccessDataResult<TrainerLogTableDto>(_mapper.Map<TrainerLogTableDto>(trainerLog), _stringLocalizer[Messages.FoundSuccess]);
        }
        public async Task<IResult> ListAsync()
        {
            var trainerLog = await _trainerLogTablerepository.GetAllAsync();
            if (trainerLog.Count() <= 0) return new ErrorResult(_stringLocalizer[Messages.ListHasNoElements]);
            return new SuccessDataResult<List<TrainerLogTableDto>>(_mapper.Map<List<TrainerLogTableDto>>(trainerLog), _stringLocalizer[Messages.ListedSuccess]);
        }
    }
}
