using BACampusApp.Dtos.Subjects;
using Microsoft.Extensions.Localization;

namespace BACampusApp.Business.Concretes
{
    public class SubjectManager : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<Resource> _stringLocalizer;

        public SubjectManager(ISubjectRepository subjectRepository, IMapper mapper,IStringLocalizer<Resource> stringLocalizer)
        {
            _subjectRepository = subjectRepository;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }
        /// <summary>
        ///  Bu metot Subject nesnesi oluşturma işlemini işlemlerini yapmaktadır.
        /// </summary>
        /// <param name="subjectCreateDto"></param>
        ///  <returns>></returns>
        public async Task<IResult> AddAsync(SubjectCreateDto subjectCreateDto)
        {
            string inputNameWithoutSpaces = subjectCreateDto.Name.Replace(" ", string.Empty).ToLower();

            bool hasSubject = await _subjectRepository.AnyAsync(s => s.Name.Replace(" ", string.Empty).ToLower() == inputNameWithoutSpaces);

            if (hasSubject)
                return new ErrorResult(_stringLocalizer[Messages.SubjectAlreadyExists]);
            var subject = await _subjectRepository.AddAsync(_mapper.Map<Subject>(subjectCreateDto));
            await _subjectRepository.SaveChangesAsync();
            return new SuccessDataResult<SubjectDto>(_mapper.Map<SubjectDto>(subject), _stringLocalizer[Messages.AddSuccess]);
        }

        /// <summary>
        ///  Bu metot Subject nesnesi silme işlemini yapacaktır.
        /// </summary>
        /// <param name="id">silinmek  istenen subject nesnesinin Guid tipinde Id si </param>
        public async Task<IResult> DeleteAsync(Guid id)
        {
            var deletingSubject = await _subjectRepository.GetByIdAsync(id);
            if (deletingSubject==null)
                return new ErrorResult(_stringLocalizer[Messages.SubjectNotFound]);
            await _subjectRepository.DeleteAsync(deletingSubject);
            await _subjectRepository.SaveChangesAsync();
            return new SuccessResult(_stringLocalizer[Messages.DeleteSuccess]);
        }

        /// <summary>
        ///  Bu metot Yöneticinin,tüm konularını listelemesini sağlamaktadır.
        /// </summary>        
        /// <returns></returns>
        public async Task<IResult> GetListAsync()
        {
            var subjects = await _subjectRepository.GetAllAsync();
            if (subjects.Count()<=0)
                return new SuccessResult(_stringLocalizer[Messages.ListHasNoElements]);
            return new SuccessDataResult<List<SubjectListDto>>(_mapper.Map<List<SubjectListDto>>(subjects), _stringLocalizer[Messages.ListedSuccess]);
        }

        /// <summary>
        ///  SubjectDto ve Subject nesnelerini listeleme işlemlerini yapmaktadır.
        /// </summary>
        /// <param name="id">detayları getirilmek istenen Subject nesnesinin Guid tipinde Id si</param>
        /// <returns></returns>        
        public async Task<IResult> GetByIdAsync(Guid id)
        {
            var subject = await _subjectRepository.GetByIdAsync(id);
            if (subject == null)
                return new ErrorResult(_stringLocalizer[Messages.SubjectNotFound]);
            var subjectDto = _mapper.Map<SubjectDetailsDto>(subject);
            return new SuccessDataResult<SubjectDetailsDto>(subjectDto, _stringLocalizer[Messages.FoundSuccess]);
        }

        /// <summary>
        /// Bu metot Subject nesnesinin güncelleme işlemini yapacaktır.
        /// </summary>
        /// <param name="subjectUpdateDto">güncellenmek istenen subject nesnesinin SubjectUpdateDto tipinde entity'si</param>
        /// <returns></returns>
        public async Task<IResult> UpdateAsync(SubjectUpdateDto subjectUpdateDto)
        {
            string inputNameWithoutSpaces = subjectUpdateDto.Name.Replace(" ", string.Empty).ToLower();
            bool hasSubject = await _subjectRepository.AnyAsync(s => s.Name.Replace(" ", string.Empty).ToLower() == inputNameWithoutSpaces);
            var subject = await _subjectRepository.GetByIdAsync(subjectUpdateDto.Id);
            if (subject.Name != subjectUpdateDto.Name)
            if (hasSubject)
                return new ErrorResult(_stringLocalizer[Messages.SubjectAlreadyExists]);

            if (subject == null)
                return new ErrorResult(_stringLocalizer[Messages.SubjectNotFound]);
            await _subjectRepository.UpdateAsync(_mapper.Map(subjectUpdateDto, subject));
            await _subjectRepository.SaveChangesAsync();
            return new SuccessResult(_stringLocalizer[Messages.UpdateSuccess]);

        }
        public async Task<IResult> DeletedListAsync()
        {
            var subjects = await _subjectRepository.GetAllDeletedAsync();
            if (subjects.Count() <= 0)
            {
                return new ErrorResult(_stringLocalizer[Messages.ListedFail]);
            }
            return new SuccessDataResult<List<SubjectDeletedListDto>>(_mapper.Map<List<SubjectDeletedListDto>>(subjects), _stringLocalizer[Messages.ListedSuccess]);
        }
    }
}
