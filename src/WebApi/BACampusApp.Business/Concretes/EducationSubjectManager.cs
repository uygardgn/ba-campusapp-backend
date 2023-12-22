using BACampusApp.Dtos.EducationSubject;
using BACampusApp.Entities.DbSets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace BACampusApp.Business.Concretes
{
	public class EducationSubjectManager : IEducationSubjectService
	{
		private readonly IMapper _mapper;
		private readonly IEducationSubjectRepository _educationSubjectRepository;
		private readonly ISubjectRepository _subjectRepository;
		private readonly IEducationRepository _educationRepository;
		private readonly IStringLocalizer<Resource> _stringLocalizer;


		public EducationSubjectManager(IMapper mapper, IEducationSubjectRepository educationSubjectRepository, IEducationRepository educationRepository, ISubjectRepository subjectRepository, IStringLocalizer<Resource> stringLocalizer)
		{
			_mapper = mapper;
			_educationSubjectRepository = educationSubjectRepository;
			_subjectRepository = subjectRepository;
			_educationRepository = educationRepository;
			_stringLocalizer = stringLocalizer;

		}
		/// <summary>
		/// Bu metot eğitim için konu ekleme işlemini yapar.
		/// </summary>
		/// <param name="educationSubjectDto"></param>
		public async Task<Core.Utilities.Results.IResult> AddAsync(EducationSubjectCreateDto educationSubjectCreateDto)
		{
			//Bu kod parçası ile girilen EducationId ve SubjectId'nin daha önceden oluşturulan bir EducationSubject ögesinde aynı anda kullanılmış olup olmadığı kontrol ediliyor.Kullanılmış ise EducationSubjectsAlreadyExists mesajı dönüyor. 
			var hasEntity = await _educationSubjectRepository.AnyAsync(x => x.EducationId == educationSubjectCreateDto.EducationId && x.SubjectId == educationSubjectCreateDto.SubjectId);
			if (hasEntity)
			{
				return new ErrorDataResult<EducationSubjectCreateDto>(_stringLocalizer[Messages.EducationSubjectsAlreadyExists]);
			}

			bool isEducation = await _educationRepository.AnyAsync(x => x.Id == educationSubjectCreateDto.EducationId);
			if (!isEducation)
			{
				return new ErrorResult(_stringLocalizer[Messages.EducationNotFound]);
			}
			bool isSubject = await _subjectRepository.AnyAsync(x => x.Id == educationSubjectCreateDto.SubjectId);
			if (!isSubject)
			{
				return new ErrorResult(_stringLocalizer[Messages.SubjectNotFound]);
			}

			var educationSubject = await _educationSubjectRepository.AddAsync(_mapper.Map<EducationSubject>(educationSubjectCreateDto));

			await _educationSubjectRepository.SaveChangesAsync();
			return new SuccessDataResult<EducationSubjectDto>(_mapper.Map<EducationSubjectDto>(educationSubject), _stringLocalizer[Messages.AddSuccess]);
		}
		/// <summary>
		/// Bu metot EduıcationSubject nesnesini silme işlemini yapar.
		/// </summary>
		/// <param name="id"></param>
		public async Task<Core.Utilities.Results.IResult> DeleteAsync(Guid id)
		{
			var educationSubject = await _educationSubjectRepository.GetByIdAsync(id);
			if (educationSubject == null)
			{
				return new ErrorResult(_stringLocalizer[Messages.EducationSubjectNotFound]);
			}


			await _educationSubjectRepository.DeleteAsync(educationSubject);
			await _educationSubjectRepository.SaveChangesAsync();
			return new SuccessResult(_stringLocalizer[Messages.DeleteSuccess]);
		}
		/// <summary>
		/// Bu metot EducationSubject nesnelerini listelemeyi sağlar.
		/// </summary>
		/// <returns></returns>
		public async Task<Core.Utilities.Results.IResult> GetListAsync()
		{
			var educationSubjects = await _educationSubjectRepository.GetAllAsync();
			if (educationSubjects.Count() <= 0)
			{
				return new ErrorResult(_stringLocalizer[Messages.ListedFail]);
			}
			return new SuccessDataResult<List<EducationSubjectListDto>>(_mapper.Map<List<EducationSubjectListDto>>(educationSubjects), _stringLocalizer[Messages.ListedSuccess]);
		}
		/// <summary>
		/// Bu metot içerisine gönderilen id'ye ait eğitim ve konu  getirir.
		/// </summary>
		/// <param name="id"></param>
		public async Task<Core.Utilities.Results.IResult> GetByIdAsync(Guid id)
		{
			var educationSubject = await _educationSubjectRepository.GetByIdAsync(id);

			if (educationSubject == null)
			{
				return new ErrorResult(_stringLocalizer[Messages.EducationSubjectNotFound]);
			}
			var educationSubjectdto = _mapper.Map<EducationSubjectDetailDto>(educationSubject);
			return new SuccessDataResult<EducationSubjectDetailDto>(educationSubjectdto, _stringLocalizer[Messages.FoundSuccess]);
		}
		/// <summary>
		/// Bu metot EducationSubject nesnesinin güncelleme işlemini yapar.
		/// </summary>
		/// <param name="educationSubjectUpdateDto"></param>
		public async Task<Core.Utilities.Results.IResult> UpdateAsync(EducationSubjectUpdateDto educationSubjectUpdateDto)
		{
			var educationSubject = await _educationSubjectRepository.GetByIdAsync(educationSubjectUpdateDto.Id);

			if (educationSubject == null)
			{
				return new ErrorResult(_stringLocalizer[Messages.EducationSubjectNotFound]);
			}


			var newEducationSubject = _mapper.Map(educationSubjectUpdateDto, educationSubject);

			await _educationSubjectRepository.UpdateAsync(newEducationSubject);
			await _educationSubjectRepository.SaveChangesAsync();
			return new SuccessDataResult<EducationSubjectUpdateDto>(educationSubjectUpdateDto, _stringLocalizer[Messages.UpdateSuccess]);

		}
		public async Task<IResult> DeletedListAsync()
		{
			var educationSubject = await _educationSubjectRepository.GetAllDeletedAsync();
			if (educationSubject.Count() <= 0)
			{
				return new ErrorResult(_stringLocalizer[Messages.ListedFail]);
			}
			return new SuccessDataResult<List<EducationSubjectDeletedListDto>>(_mapper.Map<List<EducationSubjectDeletedListDto>>(educationSubject), _stringLocalizer[Messages.ListedSuccess]);
		}

		public async Task<IResult> GetSubjectsByEducationIdAsync(Guid? educationId)
		{
			var educationSubjects = await _educationSubjectRepository.GetAllAsync(es => es.EducationId == educationId.Value);
			
			var targetSubjectDtos = educationSubjects.Select(es => new EducationSubjectListByEducationIdDto
			{
				Id = es.Id,
				EducationId = es.EducationId,
				SubjectId = es.SubjectId,
				EducationName = es.Education.Name,
				SubjectName = es.Subject.Name,
				SubjectDescription = es.Subject.Description
			}).ToList();
			return new SuccessDataResult<List<EducationSubjectListByEducationIdDto>>(_mapper.Map<List<EducationSubjectListByEducationIdDto>>(targetSubjectDtos), _stringLocalizer[Messages.ListedSuccess]);


		}
		public async Task<IResult> GetResourceSubjectsListAsync(Guid? educationId)
		{
			var educationSubjects = await _educationSubjectRepository.GetAllAsync(es => es.EducationId == educationId.Value);
			var subjectsInEducation = educationSubjects.Select(es => es.SubjectId).ToList();
			var resourceSubject = await _subjectRepository.GetAllAsync(x => !subjectsInEducation.Contains(x.Id));
			var resourceSubjectDtos = resourceSubject.Select(x => new ResourceSubjectsDto
			{
				SubjectId = x.Id,
				SubjectName = x.Name,
			}).ToList();
			return new SuccessDataResult<List<ResourceSubjectsDto>>(_mapper.Map<List<ResourceSubjectsDto>>(resourceSubjectDtos), _stringLocalizer[Messages.ListedSuccess]);
		}

        public async Task<IResult> CreateWithListAsync(EducationSubjectListCreateDto createListDto)
        {
            var educationSubjects = await _educationSubjectRepository.GetAllAsync(es => es.EducationId == createListDto.EducationId);

            // Delete subjects that are not in the new list
            foreach (var item in educationSubjects.Where(x => !createListDto.SubjectIds.Contains(x.SubjectId)))
            {
                await _educationSubjectRepository.DeleteAsync(item);
            }

            int order = 1;

            foreach (var item in createListDto.SubjectIds)
            {
                var existingEducationSubject = educationSubjects.FirstOrDefault(x => x.SubjectId == item);

                if (existingEducationSubject != null)
                {
                    existingEducationSubject.Order = order;
                    await _educationSubjectRepository.UpdateAsync(existingEducationSubject);
                }
                else
                {
                    var newEducationSubject = await _educationSubjectRepository.AddAsync(_mapper.Map<EducationSubject>(new EducationSubjectCreateDto()
                    {
                        EducationId = createListDto.EducationId,
                        SubjectId = item,
                        Order = order
                    }));
                }

                order++;
            }

            await _educationSubjectRepository.SaveChangesAsync();
            return new SuccessResult(_stringLocalizer[Messages.AddSuccess]);
        }




        /// <summary>
        /// Bu metot içerisine gönderilen eğitim idlerine ait konuları listeler.
        /// </summary>
        /// <param name="educationIds"></param>
        public async Task<IResult> GetSubjectsByEducationIdsAsync(List<Guid> educationIds)
        {
         
            var educationSubjects = await _educationSubjectRepository
                .GetAllAsync(es => educationIds.Contains(es.EducationId));

            if (!educationSubjects.Any())
                return new ErrorResult(_stringLocalizer[Messages.EducationSubjectListHasNot]);


			return new SuccessDataResult<List<EducationSubjectsListByEducationIdsDto>>(_mapper.Map<List<EducationSubjectsListByEducationIdsDto>>(educationSubjects), _stringLocalizer[Messages.ListedSuccess]);
			
        }


    }
}