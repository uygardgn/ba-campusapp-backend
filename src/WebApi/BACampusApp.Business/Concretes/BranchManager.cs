namespace BACampusApp.Business.Concretes
{
    public class BranchManager : IBranchService
    {
        private readonly IBranchRepository _branchRepository;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        public BranchManager(IBranchRepository branchRepository, IMapper mapper, IStringLocalizer<Resource> stringLocalizer)
        {
            _branchRepository = branchRepository;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }

        /// <summary>
        /// Bu metot yeni bir Branch nesnesi oluşturmak için kullanılmaktadır.
        /// </summary>
        /// <param name="branchCreateDto"></param>
        /// <returns></returns>
        public async Task<IResult> CreateAsync(BranchCreateDto branchCreateDto)
        {
            if (await _branchRepository.AnyAsync(x => x.Name.ToLower() == branchCreateDto.Name.ToLower()))
                return new ErrorResult(_stringLocalizer[Messages.BranchAlreadyExists]);
            var toBeCreated = _mapper.Map<Branch>(branchCreateDto);
            await _branchRepository.AddAsync(toBeCreated);
            await _branchRepository.SaveChangesAsync();
            var branchDto = _mapper.Map<BranchDto>(toBeCreated);
            return new SuccessDataResult<BranchDto>(branchDto, _stringLocalizer[Messages.AddSuccess]);
        }
        /// <summary>
        /// Bu metot bir Branch nesnesi silmek için kullanılmaktadır.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IResult> DeleteAsync(Guid id)
        {
            var hasClassroom = await _branchRepository.GetByIdAsync(id, true);
            if (hasClassroom == null)
                return new ErrorResult(_stringLocalizer[Messages.ClassroomNotFound]);
            await _branchRepository.DeleteAsync(hasClassroom);
            await _branchRepository.SaveChangesAsync();
            return new SuccessResult(_stringLocalizer[Messages.DeleteSuccess]);
        }
        /// <summary>
        /// Bu metot bir Branch nesnesini güncellemek için kullanılmaktadır.
        /// </summary>
        /// <param name="branchUpdateDto"></param>
        /// <returns></returns>
        public async Task<IResult> UpdateAsync(BranchUpdateDto branchUpdateDto)
        {
            var entity = await _branchRepository.GetByIdAsync(branchUpdateDto.Id);
            if (entity == null)
                return new ErrorResult(_stringLocalizer[Messages.BranchNotFound]);
            var updatedEntity = _mapper.Map(branchUpdateDto, entity);
            await _branchRepository.UpdateAsync(updatedEntity);
            await _branchRepository.SaveChangesAsync();
            return new SuccessDataResult<BranchUpdateDto>(branchUpdateDto, _stringLocalizer[Messages.UpdateSuccess]);
        }
        /// <summary>
        /// Bu metot veritabanındaki tüm branch nesnelerini liste şeklinde göstermek için kullanılmaktadır.
        /// </summary>
        /// <returns></returns>
        public async Task<IResult> GetAllAsync()
        {
            var branches = await _branchRepository.GetAllAsync();
            if (branches.Count() <= 0)
                return new SuccessResult(_stringLocalizer[Messages.ListHasNoElements]);
            var branchesDtos = _mapper.Map<IEnumerable<BranchDto>>(branches);
            return new SuccessDataResult<IEnumerable<BranchDto>>(branchesDtos, _stringLocalizer[Messages.ListedSuccess]);
        }
        /// <summary>
        /// Bu metot parametre olarak verilen id'ye sahip branch nesnesinin gösterilmesi için kullanılmaktadır.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IResult> GetByIdAsync(Guid id)
        {
            var branch = await _branchRepository.GetByIdAsync(id);
            if (branch == null)
                return new ErrorResult(_stringLocalizer[Messages.BranchNotFound]);
            var branchDto = _mapper.Map<BranchDto>(branch);
            return new SuccessDataResult<BranchDto>(branchDto, _stringLocalizer[Messages.FoundSuccess]);
        }

        public async Task<IResult> DeletedListAsync()
        {
            var entites = await _branchRepository.GetAllDeletedAsync();
            if (entites.Count() <= 0)
            {
                return new ErrorResult(_stringLocalizer[Messages.ListedFail]);
            }
            return new SuccessDataResult<List<BranchDeletedListDto>>(_mapper.Map<List<BranchDeletedListDto>>(entites), _stringLocalizer[Messages.ListedSuccess]);
        }
    }
}
