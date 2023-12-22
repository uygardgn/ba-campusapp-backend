using BACampusApp.Dtos.Educations;
using Microsoft.Extensions.Localization;

namespace BACampusApp.Business.Concretes
{
    public class EducationManager : IEducationService
    {
        private readonly IEducationRepository _educationRepository;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IStringLocalizer<Resource> _stringLocalizer;


        public EducationManager(IEducationRepository educationRepository, IMapper mapper, ICategoryRepository categoryRepository, IStringLocalizer<Resource> stringLocalizer)
        {
            _educationRepository = educationRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _stringLocalizer = stringLocalizer;
        }

        /// <summary>
        ///  Bu metot Education nesnesi oluşturma işlemini yapmaktadır.
        /// </summary>
        /// <param name="educationCreateDto"></param>
        ///  <returns>SuccessDataResult<EducationCreateDto>, ErrorDataResult<EducationCreateDto></returns>
        public async Task<IResult> AddAsync(EducationCreateDto educationCreateDto)
        {
            bool hasEducation = await _educationRepository.AnyAsync(s => s.Name.ToLower() == educationCreateDto.Name.ToLower());
            if (hasEducation)
                return new ErrorResult(_stringLocalizer[Messages.EducationAlreadyExists]);
            var education = await _educationRepository.AddAsync(_mapper.Map<Education>(educationCreateDto));
            bool categoryExists = await _categoryRepository.AnyAsync(c => c.Id == educationCreateDto.SubCategoryId);
            if (!categoryExists)
                return new ErrorResult(_stringLocalizer[Messages.CategoryNotFound]);
            await _educationRepository.SaveChangesAsync();
            return new SuccessDataResult<EducationDto>(_mapper.Map<EducationDto>(education), _stringLocalizer[Messages.AddSuccess]);
        }

        /// <summary>
        /// Bu metod database de kayıtlı id si verilen Educationu siler.
        /// </summary>
        /// <param name="id">silinmek istenen education nesnesinin Guid tipinde Id si </param>
        /// <returns>SuccessResult, ErrorResult</returns>
        public async Task<IResult> DeleteAsync(Guid id)
        {
            var deletingEducation = await _educationRepository.GetByIdAsync(id);

            if (deletingEducation == null)
                return new ErrorResult(_stringLocalizer[Messages.EducationNotFound]);

            if (deletingEducation.Classrooms.Any())
                return new ErrorResult(_stringLocalizer[Messages.EducationHasClassrooms]);

            await _educationRepository.DeleteAsync(deletingEducation);
            await _educationRepository.SaveChangesAsync();
            return new SuccessResult(_stringLocalizer[Messages.DeleteSuccess]);

        }

        /// <summary>
        ///  Bu metot veritabanındaki tüm eğitimleri çeker ve bu eğitim listesini EducationListDto ile eşleyip çıktısını verir.
        /// </summary>
        /// <returns>SuccessDataResult<EducationListDto>, ErrorResult</returns>
        public async Task<IResult> GetListAsync()
        {
            var educations = await _educationRepository.GetAllAsync();
            if (educations.Count() <= 0)
                return new SuccessResult(_stringLocalizer[Messages.ListHasNoElements]);
            return new SuccessDataResult<List<EducationListDto>>(_mapper.Map<List<EducationListDto>>(educations), _stringLocalizer[Messages.ListedSuccess]);
        }

        /// <summary>
        ///  Bu metot veritabanındaki educationsubject içeren tüm eğitimleri çeker ve bu eğitim listesini EducationListDto ile eşleyip çıktısını verir.
        /// </summary>
        /// <returns>SuccessDataResult<EducationListDto>, ErrorResult</returns>
        public async Task<IResult> GetEducationListThatHaveEducationSubjectAsync()
        {
            var educations = await _educationRepository.GetAllAsync(x => x.EducationSubjects.Any(x => x.Status == Status.Active));
            if (educations.Count() <= 0)
                return new SuccessResult(_stringLocalizer[Messages.ListHasNoElements]);
            return new SuccessDataResult<List<EducationListDto>>(_mapper.Map<List<EducationListDto>>(educations), _stringLocalizer[Messages.ListedSuccess]);
        }

        /// <summary>
        /// Bu metodu database de kayıtlı id si verilen Educationu çeker ve gösterir.
        /// </summary>
        /// /// <param name="id">detayları getirilmek istenen education nesnesinin Guid tipinde Id si </param>
        /// <returns>SuccessDataResult<EducationDetailsDto>,ErrorResult</returns>
        public async Task<IResult> GetDetailsAsync(Guid id)
        {
            var education = await _educationRepository.GetByIdAsync(id);
            if (education == null)
                return new ErrorResult(_stringLocalizer[Messages.EducationNotFound]);
            var educationDto = _mapper.Map<EducationDetailsDto>(education);
            return new SuccessDataResult<EducationDetailsDto>(educationDto, _stringLocalizer[Messages.FoundSuccess]);
        }

        /// <summary>
        ///  Bu metot Education nesnesini güncelleme işlemini  yapmaktadır.
        /// </summary>
        /// <param name="educationUpdateDto">update edilecek nesne</param>
        ///  <returns>SuccessResult, ErrorDataResult<EducationUpdateDto></returns>
        public async Task<IResult> UpdateAsync(EducationUpdateDto educationUpdateDto)
        {
            bool hasEducation = await _educationRepository.AnyAsync(s => s.Name.ToLower() == educationUpdateDto.Name.ToLower());
            var education = await _educationRepository.GetByIdAsync(educationUpdateDto.Id);
            if(education.Name != educationUpdateDto.Name)
            if (hasEducation)
                return new ErrorResult(_stringLocalizer[Messages.EducationAlreadyExists]);

            if (education == null)
                return new ErrorResult(_stringLocalizer[Messages.EducationNotFound]);
            bool categoryExists = await _categoryRepository.AnyAsync(c => c.Id == educationUpdateDto.SubCategoryId);
            if (!categoryExists)
                return new ErrorResult(_stringLocalizer[Messages.CategoryNotFound]);
            await _educationRepository.UpdateAsync(_mapper.Map(educationUpdateDto, education));
            await _educationRepository.SaveChangesAsync();
            return new SuccessResult(_stringLocalizer[Messages.UpdateSuccess]);
        }

        public async Task<IResult> DeletedListAsync()
        {
            var education = await _educationRepository.GetAllDeletedAsync();
            if (education.Count() <= 0)
            {
                return new ErrorResult(_stringLocalizer[Messages.ListedFail]);
            }
            return new SuccessDataResult<List<EducationDeletedListDto>>(_mapper.Map<List<EducationDeletedListDto>>(education), _stringLocalizer[Messages.ListedSuccess]);
        }
    }
}
