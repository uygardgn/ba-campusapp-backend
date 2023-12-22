using AutoMapper;
using BACampusApp.Dtos.ClassroomStudent;
using BACampusApp.Dtos.Students;
using BACampusApp.Entities.DbSets;
using Microsoft.Extensions.Localization;

namespace BACampusApp.Business.Concretes
{

    public class ClassroomTrainerManager : IClassroomTrainersService
    {
        private readonly IClassroomTrainersRepository _classTrainersRepository;
        private readonly IMapper _mapper;
        private readonly ITrainerRepository _trainerRepository;
        private readonly IClassroomRepository _classroomRepository;
        private readonly IActivityStateLogSevices _activityStateLogService;
        private readonly IStringLocalizer<Resource> _stringLocalizer;

        public ClassroomTrainerManager(IClassroomTrainersRepository classTrainersRepository, IMapper mapper, ITrainerRepository trainerRepository, IClassroomRepository classroomRepository, IActivityStateLogSevices activityStateLogService, IStringLocalizer<Resource> stringLocalizer)
        {
            _classTrainersRepository = classTrainersRepository;
            _mapper = mapper;
            _trainerRepository = trainerRepository;
            _classroomRepository = classroomRepository;
            _activityStateLogService = activityStateLogService;
            _stringLocalizer = stringLocalizer;
        }
        /// <summary>
        /// Bu metot yeni bir ClassroomTrainer nesnesi oluşturmak için kullanılmaktadır.
        /// </summary>
        /// <param name="classroomTrainerCreateDto"></param>
        /// <returns>SuccessDataResult<ClassroomTrainerDto>, ErrorResult</returns>

        public async Task<IResult> CreateAsync(ClassroomTrainersCreateDto classroomTrainerCreateDto)
        {
            var hasEntity = await _classTrainersRepository.AnyAsync(x => x.TrainerId == classroomTrainerCreateDto.TrainerId && x.ClassroomId == classroomTrainerCreateDto.ClassroomId);
            if (hasEntity)
                return new ErrorResult(_stringLocalizer[Messages.TrainerAlreadyExistsInClassroom]);

            var newClassTrainer = _mapper.Map<ClassroomTrainer>(classroomTrainerCreateDto);

            bool trainerExists = await _trainerRepository.AnyAsync(c => c.Id == newClassTrainer.TrainerId);
            if (!trainerExists)
                return new ErrorResult(_stringLocalizer[Messages.TrainerNotFound]);

            var classroom = await _classroomRepository.GetByIdAsync(newClassTrainer.ClassroomId);
            if (classroom == null)
                return new ErrorResult(_stringLocalizer[Messages.ClassroomNotFound]);

            bool dateControl = (classroom != null) ? true : false;

            if (!dateControl)
                return new ErrorResult(_stringLocalizer[Messages.AddFail]);
            await _classTrainersRepository.AddAsync(newClassTrainer);
            await _classTrainersRepository.SaveChangesAsync();
            return new SuccessDataResult<ClassroomTrainerDto>(_mapper.Map<ClassroomTrainerDto>(newClassTrainer), _stringLocalizer[Messages.AddSuccess]);
        }

        /// <summary>
        /// Bu metot mevcut ClassroomTrainer nesnesini silmek için kullanılmaktadır.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>SuccessResult, ErrorResult</returns>

        public async Task<IResult> DeleteAsync(Guid id)
        {
            var deletingClassTrainer = await _classTrainersRepository.GetByIdAsync(id);
            if (deletingClassTrainer == null)
                return new ErrorResult(_stringLocalizer[Messages.ClassroomTrainersNotFound]);
            await _classTrainersRepository.DeleteAsync(deletingClassTrainer);
            await _classTrainersRepository.SaveChangesAsync();
            return new SuccessResult(_stringLocalizer[Messages.DeleteSuccess]);
        }
        /// <summary>
        /// Bu metot veritabanındaki tüm ClassroomTrainer nesnelerini liste şeklinde göstermek için kullanılmaktadır.
        /// </summary>
        /// <returns>SuccessDataResult<List<ClassroomTrainersListDto>>, ErrorResult</returns>

        public async Task<IResult> GetAllAsync()
        {
            var listClassTrainers = await _classTrainersRepository.GetAllAsync();
            if (listClassTrainers.Count() <= 0)
                return new SuccessResult(_stringLocalizer[Messages.ListHasNoElements]);
            return new SuccessDataResult<List<ClassroomTrainersListDto>>(_mapper.Map<List<ClassroomTrainersListDto>>(listClassTrainers), _stringLocalizer[Messages.ListedSuccess]);
        }


        /// <summary>
        /// Bu metot ClassroomTrainer nesnesini güncelleme işlemini yapmaktadır.
        /// </summary>
        /// <param name="classTrainerUpdateDto"></param>
        /// <returns>SuccessResult, ErrorResult</returns>
        public async Task<IResult> UpdateAsync(ClassroomTrainersUpdateDto classTrainerUpdateDto)
        {
            var updateTrainer = await _classTrainersRepository.GetByIdAsync(classTrainerUpdateDto.Id);
            if (updateTrainer == null)
                return new ErrorResult(_stringLocalizer[Messages.ClassroomTrainersNotFound]);
            var updateresult = _mapper.Map(classTrainerUpdateDto, updateTrainer);

            bool trainerExists = await _trainerRepository.AnyAsync(c => c.Id == updateresult.TrainerId);
            if (!trainerExists)
            {
                return new ErrorResult(_stringLocalizer[Messages.TrainerNotFound]);
            }
            await _classTrainersRepository.UpdateAsync(updateTrainer);
            await _classTrainersRepository.SaveChangesAsync();
            return new SuccessResult(_stringLocalizer[Messages.UpdateSuccess]);

        }
        /// <summary>
        /// Bu metot veritabanındaki tüm ClassroomTrainers nesnelerini liste şeklinde göstermek için kullanılmaktadır.
        /// </summary>
        /// <returns></returns>
        public async Task<IResult> ActiveListAsync()
        {
            var listTrainer = await _classTrainersRepository.GetAllAsync(x => x.Status == Status.Active);
            if (listTrainer.Count() <= 0)
                return new SuccessResult(_stringLocalizer[Messages.ListHasNoElements]);
            var listTarinerDtos = _mapper.Map<IEnumerable<ClassroomTrainersListDto>>(listTrainer);
            return new SuccessDataResult<IEnumerable<ClassroomTrainersListDto>>(listTarinerDtos, _stringLocalizer[Messages.ListedSuccess]);
        }
        /// <summary>
        /// Bu metot tüm ClassromTrainer nesnlerinin aktif olanalrını liste olarak dönmek için kullanılmaktadır.
        /// </summary>
        /// <returns>SuccessResult, SuccessDataResult<IEnumerable<ClassroomTrainerListDto>></returns>
        public async Task<IResult> UpdateActiveAsync(ClassroomTrainerActiveUpdateDto classroomTrainerActiveUpdateDto)
        {
            ClassroomTrainer? classroomTrainer = await _classTrainersRepository.GetByIdAsync(classroomTrainerActiveUpdateDto.Id);


            if (classroomTrainer == null)
                return new ErrorResult(_stringLocalizer[Messages.ClassroomTrainersNotFound]);
            else if (classroomTrainer.Status == classroomTrainerActiveUpdateDto.Status)
                return new ErrorResult(_stringLocalizer[Messages.TrainerActivityNotChanged]);

            Trainer? trainer = await _trainerRepository.GetByIdAsync(classroomTrainer.TrainerId);
            Classroom? clasroom = await _classroomRepository.GetByIdAsync(classroomTrainer.ClassroomId);

            if (trainer == null && classroomTrainerActiveUpdateDto.Status == Status.Active)
                return new ErrorResult(_stringLocalizer[Messages.TrainerNotFound]);
            else if (clasroom == null && classroomTrainerActiveUpdateDto.Status == Status.Active)
                return new ErrorResult(_stringLocalizer[Messages.ClassroomNotFound]);
            else if (trainer.Status == Status.Deleted && classroomTrainerActiveUpdateDto.Status == Status.Active)
                return new ErrorResult(_stringLocalizer[Messages.TrainerNotActive]);
            else if (clasroom.Status == Status.Deleted && classroomTrainerActiveUpdateDto.Status == Status.Active)
                return new ErrorResult(_stringLocalizer[Messages.ClassroomNotActive]);


            ClassroomTrainer? newClassroomTrainer = _mapper.Map(classroomTrainerActiveUpdateDto, classroomTrainer);
            ActivityStateLogCreateDto? activityStateLogCreateDto = _mapper.Map<ActivityStateLogCreateDto>(classroomTrainerActiveUpdateDto);



            await _classTrainersRepository.UpdateAsync(newClassroomTrainer);
            await _activityStateLogService.CreateAsync(activityStateLogCreateDto);
            await _classTrainersRepository.SaveChangesAsync();


            return new SuccessResult(_stringLocalizer[Messages.UpdateSuccess]);
        }

        public async Task<IResult> DeletedListAsync()
        {
            var listTrainer = await _classTrainersRepository.GetAllDeletedAsync();
            if (listTrainer.Count() <= 0)
            {
                return new ErrorResult(Messages.ListedFail);
            }
            return new SuccessDataResult<List<ClassromTrainerDeletedListDto>>(_mapper.Map<List<ClassromTrainerDeletedListDto>>(listTrainer), _stringLocalizer[Messages.ListedSuccess]);
        }

        /// <summary>
        /// Bu metot ClassromTrainer tablosunda secili sınıfa ait eğitmeen listesini dönmek için kullanılmaktadır.
        /// </summary>
        /// <returns>SuccessResult, SuccessDataResult<IEnumerable<TrainerListByClassroomIdDto>></returns>
        public async Task<IResult> TrainersByClassroomIdAsync(Guid? clasroomId)
        {
            var classroomTrainers = await _classTrainersRepository.GetAllAsync(cs => cs.ClassroomId == clasroomId.Value);
            var trainerInClassroom = classroomTrainers.Select(cs => cs.TrainerId).ToList();
            var resourceTrainer = await _trainerRepository.GetAllAsync(cs => trainerInClassroom.Contains(cs.Id));
            var trainerListByClassroomIdDto = resourceTrainer.Select(cs => new TrainerListByClassroomIdDto
            {
                Id = cs.Id,
                FirstName = cs.FirstName,
                LastName = cs.LastName,
                Email = cs.Email,
                PhoneNumber = cs.PhoneNumber,
                Image = cs.Image,
                StartDate = DateTime.Now.Date,
                EndDate = DateTime.Now.Date,
                Status = cs.Status
            }).ToList();

            foreach (var item1 in trainerListByClassroomIdDto)
            {
                foreach (var item2 in classroomTrainers)
                {
                    if (item2.TrainerId == item1.Id)
                    {
                        item1.StartDate = item2.StartDate;
                        item1.EndDate = item2.EndDate;
                    }
                }
            }

            return new SuccessDataResult<List<TrainerListByClassroomIdDto>>(_mapper.Map<List<TrainerListByClassroomIdDto>>(trainerListByClassroomIdDto), _stringLocalizer[Messages.ListedSuccess]);
        }

        /// <summary>
        /// Bu metot ClassromTrainer tablosunda secili sınıfa ait olmayan eğitmenlerin listesini dönmek için kullanılmaktadır.
        /// </summary>
        /// <returns>SuccessResult, SuccessDataResult<IEnumerable<TrainerListByClassroomIdDto>></returns>
        public async Task<IResult> ClasslessTrainerList(Guid? clasroomId)
        {
            var classroomTrainers = await _classTrainersRepository.GetAllAsync(cs => cs.ClassroomId == clasroomId.Value);
            var trainerInClassroom = classroomTrainers.Select(cs => cs.TrainerId).ToList();
            var resourceTrainer = await _trainerRepository.GetAllAsync(cs => !trainerInClassroom.Contains(cs.Id));
            var trainerListByClassroomIdDto = resourceTrainer.Select(cs => new TrainerListByClassroomIdDto
            {
                Id = cs.Id,
                FirstName = cs.FirstName,
                LastName = cs.LastName,
                Email = cs.Email,
                PhoneNumber = cs.PhoneNumber,
                Image = cs.Image,
                Status = cs.Status
            }).ToList();

            foreach (var item1 in trainerListByClassroomIdDto)
            {
                foreach (var item2 in classroomTrainers)
                {
                    if (item2.TrainerId == item1.Id)
                    {
                        item1.StartDate = item2.StartDate;
                        item1.EndDate = (DateTime)item2.EndDate;
                    }
                }
            }

            return new SuccessDataResult<List<TrainerListByClassroomIdDto>>(_mapper.Map<List<TrainerListByClassroomIdDto>>(trainerListByClassroomIdDto), _stringLocalizer[Messages.ListedSuccess]);
        }

        public async Task<IResult> GetById(Guid id)
        {
            var hasEntity=await _classTrainersRepository.GetByIdAsync(id);
            if (hasEntity == null)
                return new ErrorResult(_stringLocalizer[Messages.ClassroomTrainersNotFound]);
            var entityDto = _mapper.Map<ClassroomTrainerDto>(hasEntity);
            return new SuccessDataResult<ClassroomTrainerDto>(entityDto, _stringLocalizer[Messages.FoundSuccess]);
        }

        /// <summary>
        /// Bu metot, belirli bir eğitmenin ait olduğu sınıf odalarını listelemek için kullanılır.
        /// </summary>
        /// <param name="trainerId">Eğitmenin kimliği</param>
        /// <returns>SuccessResult, SuccessDataResult<List<Classroom>> ile sınıfları döndürür.</returns>
        public async Task<IResult> ClassroomsByTrainerIdAsync(Guid trainerId)
        {

            var classroomTrainers = await _classTrainersRepository.GetAllAsync(cs => cs.Trainer.IdentityId == trainerId.ToString());
            var classroomInTrainer = classroomTrainers.Select(cs => cs.ClassroomId).ToList();
            var resourceClassroom = await _classroomRepository.GetAllAsync(cs => classroomInTrainer.Contains(cs.Id));

            var classroomListByTrainerIdDTO = _mapper.Map<List<ClassroomListByTrainerIdDTO>>(resourceClassroom);

            return new SuccessDataResult<List<ClassroomListByTrainerIdDTO>>(classroomListByTrainerIdDTO, _stringLocalizer[Messages.ListedSuccess]);


        }
    }
}

