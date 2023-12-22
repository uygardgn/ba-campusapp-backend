using BACampusApp.Dtos.Tag;
using Microsoft.Extensions.Localization;

namespace BACampusApp.Business.Concretes
{
    public class TagManager : ITagService
    {
        private readonly ITagRepository _tagRepo;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        public TagManager(ITagRepository tagRepo, IMapper mapper, IStringLocalizer<Resource> stringLocalizer)
        {
            _tagRepo = tagRepo;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }

        /// <summary>
        ///  Bu metot Tag nesnesi oluşturma işlemini işlemlerini yapmaktadır.
        /// </summary>
        /// <param name="tagCreateDto"></param>
        ///  <returns>></returns>
        public async Task<IResult> AddAsync(TagCreateDto tagCreateDto)
        {
            bool hasTag = await _tagRepo.AnyAsync(x => x.Name.ToLower() == tagCreateDto.Name.ToLower());
            if (hasTag)
                return new ErrorResult(_stringLocalizer[Messages.TagAlreadyExists]);
            var creatingTag = await _tagRepo.AddAsync(_mapper.Map<Tag>(tagCreateDto));
            await _tagRepo.SaveChangesAsync();
            return new SuccessDataResult<TagDto>(_mapper.Map<TagDto>(creatingTag), Messages.AddSuccess);
        }

        /// <summary>
        ///  Bu metot Tag nesnesi silme işlemini yapacaktır.
        /// </summary>
        /// <param name="id">silinmek  istenen tag nesnesinin Guid tipinde Id si </param>
        public async Task<IResult> DeleteAsync(Guid id)
        {
            var deletingTag = await _tagRepo.GetByIdAsync(id);
            if (deletingTag==null)
                return new ErrorResult(_stringLocalizer[Messages.TagIsNotFound]);
            await _tagRepo.DeleteAsync(deletingTag);
            await _tagRepo.SaveChangesAsync();
            return new SuccessResult(_stringLocalizer[Messages.DeleteSuccess]);
        }

        /// <summary>
        ///  Tag nesnesinin detaylarını getirme işlemini yapmaktadır.
        /// </summary>
        /// <param name="id">detayları getirilmek istenen Tag nesnesinin Guid tipinde Id si</param>
        /// <returns></returns>
        public async Task<IResult> GetByIdAsync(Guid id)
        {
            var tag = await _tagRepo.GetByIdAsync(id);
            if (tag == null)
                return new ErrorResult(_stringLocalizer[Messages.TagNotFound]);
            var tagDto = _mapper.Map<TagDetailsDto>(tag);
            return new SuccessDataResult<TagDetailsDto>(tagDto, _stringLocalizer[Messages.FoundSuccess]);
        }

        /// <summary>
        ///  Bu metot Yöneticinin,tüm tagleri listelemesini sağlamaktadır.
        /// </summary>        
        /// <returns></returns>
        public async Task<IResult> GetListAsync()
        {
            var tags = await _tagRepo.GetAllAsync();
            if (tags.Count()<=0)
                return new SuccessResult(_stringLocalizer[Messages.ListHasNoElements]);
            return new SuccessDataResult<List<TagListDto>>(_mapper.Map<List<TagListDto>>(tags), _stringLocalizer[Messages.ListedSuccess]);
        }

        /// <summary>
        /// Bu metot Tag nesnesinin güncelleme işlemini yapacaktır.
        /// </summary>
        /// <param name="tagUpdateDto">güncellenmek istenen tag nesnesinin TagUpdateDto tipinde entity'si</param>
        /// <returns></returns>
        public async Task<IResult> UpdateAsync(TagUpdateDto tagUpdateDto)
        {
            bool hasTag = await _tagRepo.AnyAsync(x => x.Name.ToLower() == tagUpdateDto.Name.ToLower());
            if (hasTag)
                return new ErrorResult(_stringLocalizer[Messages.TagAlreadyExists]);

            var tag = await _tagRepo.GetByIdAsync(tagUpdateDto.Id);
            if (tag == null)
                return new ErrorResult(_stringLocalizer[Messages.TagNotFound]);
            await _tagRepo.UpdateAsync(_mapper.Map(tagUpdateDto,tag));
            await _tagRepo.SaveChangesAsync();
            return new SuccessResult(_stringLocalizer[Messages.UpdateSuccess]);
        }

        public async Task<IResult> DeletedListAsync()
        {
            var tag = await _tagRepo.GetAllDeletedAsync();
            if (tag.Count() <= 0)
            {
                return new ErrorResult(_stringLocalizer[Messages.ListedFail]);
            }
            return new SuccessDataResult<List<TagDeletedListDto>>(_mapper.Map<List<TagDeletedListDto>>(tag), _stringLocalizer[Messages.ListedSuccess]);
        }
    }
}