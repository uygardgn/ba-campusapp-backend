using BACampusApp.Dtos.TrainingType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BACampusApp.Business.Concretes
{
    public class TrainingTypeManager : ITrainingTypeService
    {
        private readonly ITrainingTypeRepository _trainingTypeRepository;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<Resource> _stringLocalizer;

        public TrainingTypeManager(ITrainingTypeRepository repository, IMapper mapper, IStringLocalizer<Resource> stringLocalizer)
        {
            _trainingTypeRepository = repository;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
        }
        /// <summary>
        /// Bu metot yeni bir TrainingType nesnesi oluşturmak için kullanılmaktadır.
        /// </summary>
        /// <param name="trainingTypeCreateDto"></param>
        /// <returns></returns>
        public async Task<IResult> CreateAsync(TrainingTypeCreateDto trainingTypeCreateDto)
        {
            if(await _trainingTypeRepository.AnyAsync(x => x.Name.ToLower() == trainingTypeCreateDto.Name.ToLower()))
                return new ErrorResult(_stringLocalizer[Messages.TrainingTypeAlreadyExists]);
            var toBeCreated = _mapper.Map<TrainingType>(trainingTypeCreateDto);
            await _trainingTypeRepository.AddAsync(toBeCreated);
            await _trainingTypeRepository.SaveChangesAsync();
            var trainingTypeDto = _mapper.Map<TrainingTypeDto>(toBeCreated);
            return new SuccessDataResult<TrainingTypeDto>(trainingTypeDto, _stringLocalizer[Messages.AddSuccess]);
        }
        /// <summary>
        /// Bu metot bir TrainingType nesnesi silmek için kullanılmaktadır.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IResult> DeleteAsync(Guid id)
        {
            var hasClassroom = await _trainingTypeRepository.GetByIdAsync(id, true);
            if (hasClassroom == null)
                return new ErrorResult(_stringLocalizer[Messages.ClassroomNotFound]);
            await _trainingTypeRepository.DeleteAsync(hasClassroom);
            await _trainingTypeRepository.SaveChangesAsync();
            return new SuccessResult(_stringLocalizer[Messages.DeleteSuccess]);
        }

        public async Task<IResult> DeletedListAsync()
        {
            var entites = await _trainingTypeRepository.GetAllDeletedAsync();
            if (entites.Count() <= 0)
            {
                return new ErrorResult(_stringLocalizer[Messages.ListedFail]);
            }
            return new SuccessDataResult<List<TrainingTypeDeletedListDto>>(_mapper.Map<List<TrainingTypeDeletedListDto>>(entites), _stringLocalizer[Messages.ListedSuccess]);
        }

        public async Task<IResult> GetAllAsync()
        {
            var trainingTypes = await _trainingTypeRepository.GetAllAsync();
            if (trainingTypes.Count() <= 0)
                return new SuccessResult(_stringLocalizer[Messages.ListHasNoElements]);
            var trainingTypeDtos = _mapper.Map<IEnumerable<TrainingTypeDto>>(trainingTypes);
            return new SuccessDataResult<IEnumerable<TrainingTypeDto>>(trainingTypeDtos, _stringLocalizer[Messages.ListedSuccess]);
        }

        public async Task<IResult> GetByIdAsync(Guid id)
        {
            var trainingType = await _trainingTypeRepository.GetByIdAsync(id);
            if (trainingType == null)
                return new ErrorResult(_stringLocalizer[Messages.BranchNotFound]);
            var trainingTypeDto = _mapper.Map<TrainingTypeDto>(trainingType);
            return new SuccessDataResult<TrainingTypeDto>(trainingTypeDto, _stringLocalizer[Messages.FoundSuccess]);
        }
        /// <summary>
        /// Bu metot bir Branch nesnesini güncellemek için kullanılmaktadır.
        /// </summary>
        /// <param name="trainingTypeUpdateDto"></param>
        /// <returns></returns>
        public async Task<IResult> UpdateAsync(TrainingTypeUpdateDto trainingTypeUpdateDto)
        {
            var entity = await _trainingTypeRepository.GetByIdAsync(trainingTypeUpdateDto.Id);
            if (entity == null)
                return new ErrorResult(_stringLocalizer[Messages.BranchNotFound]);
            var updatedEntity = _mapper.Map(trainingTypeUpdateDto, entity);
            await _trainingTypeRepository.UpdateAsync(updatedEntity);
            await _trainingTypeRepository.SaveChangesAsync();
            return new SuccessDataResult<TrainingTypeUpdateDto>(trainingTypeUpdateDto, _stringLocalizer[Messages.UpdateSuccess]);
        }
    }
}
