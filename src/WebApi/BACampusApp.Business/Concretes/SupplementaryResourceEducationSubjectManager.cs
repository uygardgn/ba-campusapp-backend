using BACampusApp.Dtos.SupplementaryResources;
using BACampusApp.Dtos.SupplementaryResourcesEducationsSubjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Business.Concretes
{
    public class SupplementaryResourceEducationSubjectManager : ISupplementaryResourceEducationSubjectService
    {

        private readonly ISupplementaryResourceEducationSubjectRepository _repository;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<Resource> _stringLocalizer;

        public SupplementaryResourceEducationSubjectManager(ISupplementaryResourceEducationSubjectRepository repository, IMapper mapper, IStringLocalizer<Resource> stringLocalizer)
        {
            _repository = repository;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<IResult> CreateAsync(SupplementaryResourceEducationSubjectCreateDto supplementaryResourceEducationSubjectsCreateDto)
        {
            foreach (var subjectId in supplementaryResourceEducationSubjectsCreateDto.SubjectId)
            {
                var hasEntity = await _repository.AnyAsync(x => x.EducationId == supplementaryResourceEducationSubjectsCreateDto.EducationId && x.SubjectId == subjectId);
                if (hasEntity)
                    return new ErrorResult(_stringLocalizer[Messages.AddFailAlreadyExists]);

                var entity = new SupplementaryResourceEducationSubject
                {
                    EducationId = (Guid)supplementaryResourceEducationSubjectsCreateDto.EducationId,
                    SubjectId = subjectId,
                };

                await _repository.AddAsync(entity);
            }

            await _repository.SaveChangesAsync();
            return new SuccessResult(_stringLocalizer[Messages.AddSuccess]);
        }

        public async Task<IResult> DeleteAsync(Guid id)
        {
            var hasEntity = await _repository.GetByIdAsync(id);
            if (hasEntity == null)
                return new ErrorResult(_stringLocalizer[Messages.SupplementaryResourceTagNotFound]);
            await _repository.DeleteAsync(hasEntity);
            await _repository.SaveChangesAsync();
            return new SuccessResult(_stringLocalizer[Messages.DeleteSuccess]);
        }

        public async Task<IResult> DeletedListAsync()
        {
            var educationSubjects = await _repository.GetAllDeletedAsync();
            if (educationSubjects.Count() <= 0)
            {
                return new ErrorResult(_stringLocalizer[Messages.ListedFail]);
            }
            return new SuccessDataResult<List<SupplementaryResourceEducationSubjectDeleteListDto>>(_mapper.Map<List<SupplementaryResourceEducationSubjectDeleteListDto>>(educationSubjects), _stringLocalizer[Messages.ListedSuccess]);
        }

        public async Task<IResult> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            if (entities.Count() <= 0)
                return new SuccessResult(_stringLocalizer[Messages.ListHasNoElements]);
            var entitiesDtos = _mapper.Map<IEnumerable<SupplementaryResourceEducationSubjectListDto>>(entities);
            return new SuccessDataResult<IEnumerable<SupplementaryResourceEducationSubjectListDto>>(entitiesDtos, _stringLocalizer[Messages.ListedSuccess]);
        }

        public async Task<IResult> GetByIdAsync(Guid id)
        {
            var hasEntity = await _repository.GetByIdAsync(id);
            if (hasEntity == null)
                return new ErrorResult(_stringLocalizer[Messages.SupplementaryResourceTagNotFound]);
            var entityDto = _mapper.Map<SupplementaryResourceEducationSubjectDto>(hasEntity);
            return new SuccessDataResult<SupplementaryResourceEducationSubjectDto>(entityDto, _stringLocalizer[Messages.FoundSuccess]);
        }

        public async Task<IResult> UpdateAsync(SupplementaryResourceEducationSubjectUpdateDto supplementaryResourceEducationSubjectsUpdateDto)
        {
            var hasEntity = await _repository.GetByIdAsync(supplementaryResourceEducationSubjectsUpdateDto.Id);
            if (hasEntity == null)
                return new ErrorResult(_stringLocalizer[Messages.SupplementaryResourceTagNotFound]);
            var updatedEntity = _mapper.Map(supplementaryResourceEducationSubjectsUpdateDto, hasEntity);
            await _repository.UpdateAsync(updatedEntity);
            await _repository.SaveChangesAsync();
            return new SuccessResult(_stringLocalizer[Messages.UpdateSuccess]);
        }
    }
}