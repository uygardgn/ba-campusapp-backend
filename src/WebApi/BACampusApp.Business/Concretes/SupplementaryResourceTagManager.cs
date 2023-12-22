using Microsoft.Extensions.Localization;

namespace BACampusApp.Business.Concretes
{
    public class SupplementaryResourceTagManager : ISupplementaryResourceTagService
    {
        private readonly ISupplementaryResourceTagRepository _repository;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<Resource> _stringLocalizer;

        public SupplementaryResourceTagManager(ISupplementaryResourceTagRepository repository, IMapper mapper, IStringLocalizer<Resource> stringLocalizer)
        {
            _repository = repository;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }

        /// <summary>
        /// Bu metot SupplementaryResourceTag nesnesinin oluşturulması için kullanılmakatadır.
        /// </summary>
        /// <param name="supplementaryResourceTagCreateDto"></param>
        /// <returns></returns>
        public async Task<IResult> CreateAsync(SupplementaryResourceTagCreateDto supplementaryResourceTagCreateDto)
        {
            var hasEntity = await _repository.AnyAsync(x => x.SupplementaryResourceId == supplementaryResourceTagCreateDto.SupplementaryResourceId && x.TagId == supplementaryResourceTagCreateDto.TagId);
            if (hasEntity)
                return new ErrorResult(_stringLocalizer[Messages.AddFailAlreadyExists]);
            SupplementaryResourceTag entity = _mapper.Map<SupplementaryResourceTag>(supplementaryResourceTagCreateDto);
            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();
            return new SuccessDataResult<SupplementaryResourceTagCreateDto>(supplementaryResourceTagCreateDto, _stringLocalizer[Messages.AddSuccess]);
        }

        /// <summary>
        /// Bu metot verilen id'ye uygun olarak SupplementaryResourceTag nesnesinin silinmesi için kullanılmaktadır.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IResult> DeleteAsync(Guid id)
        {
            var hasEntity = await _repository.GetByIdAsync(id);
            if (hasEntity == null)
                return new ErrorResult(_stringLocalizer[Messages.SupplementaryResourceTagNotFound]);
            await _repository.DeleteAsync(hasEntity);
            await _repository.SaveChangesAsync();
            return new SuccessResult(_stringLocalizer[Messages.DeleteSuccess]);
        }

        /// <summary>
        /// Bu metot tüm SupplementaryResourceTag nesnelerinin listelenmesi için kullanılmaktadır.
        /// </summary>
        /// <returns></returns>
        public async Task<IResult> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            if (entities.Count() <= 0)
                return new SuccessResult(_stringLocalizer[Messages.ListHasNoElements]);
            var entitiesDtos = _mapper.Map<IEnumerable<SupplementaryResourceTagListDto>>(entities);
            return new SuccessDataResult<IEnumerable<SupplementaryResourceTagListDto>>(entitiesDtos, _stringLocalizer[Messages.ListedSuccess]);
        }

        /// <summary>
        /// Bu metot verilen id'ye uygun SupplementaryResourceTag nesnesinin gösterilmesi için kullanılmaktadır.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IResult> GetByIdAsync(Guid id)
        {
            var hasEntity = await _repository.GetByIdAsync(id);
            if (hasEntity == null)
                return new ErrorResult(_stringLocalizer[Messages.SupplementaryResourceTagNotFound]);
            var entityDto = _mapper.Map<SupplementaryResourceTagDto>(hasEntity);
            return new SuccessDataResult<SupplementaryResourceTagDto>(entityDto, _stringLocalizer[Messages.FoundSuccess]);
        }

        /// <summary>
        /// Bu metot verilen nesne içerisindeki id'ye uygun olarak SupplementaryResourceTag nesnesinin güncellenmesi için kullanılmaktadır.
        /// </summary>
        /// <param name="supplementaryResourceTagUpdateDto"></param>
        /// <returns></returns>
        public async Task<IResult> UpdateAsync(SupplementaryResourceTagUpdateDto supplementaryResourceTagUpdateDto)
        {
            var hasEntity = await _repository.GetByIdAsync(supplementaryResourceTagUpdateDto.Id);
            if (hasEntity == null)
                return new ErrorResult(_stringLocalizer[Messages.SupplementaryResourceTagNotFound]);
            var updatedEntity = _mapper.Map(supplementaryResourceTagUpdateDto, hasEntity);
            await _repository.UpdateAsync(updatedEntity);
            await _repository.SaveChangesAsync();
            return new SuccessResult(_stringLocalizer[Messages.UpdateSuccess]);
        }

        public async Task<IResult> DeletedListAsync()
        {
            var tag = await _repository.GetAllDeletedAsync();
            if (tag.Count() <= 0)
            {
                return new ErrorResult(_stringLocalizer[Messages.ListedFail]);
            }
            return new SuccessDataResult<List<SupplementaryResourceTagDeletedListDto>>(_mapper.Map<List<SupplementaryResourceTagDeletedListDto>>(tag), _stringLocalizer[Messages.ListedSuccess]);
        }
    }
}
