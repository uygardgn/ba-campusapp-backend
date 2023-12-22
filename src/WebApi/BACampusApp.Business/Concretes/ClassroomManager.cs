using BACampusApp.Dtos.Classroom;
using BACampusApp.Entities.DbSets;
using Microsoft.Extensions.Localization;
using System.Collections;

namespace BACampusApp.Business.Concretes
{
    public class ClassroomManager : IClassroomService
    {
        private readonly IClassroomRepository _classroomRepository;
        private readonly IMapper _mapper;
        private readonly IEducationRepository _educationRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IActivityStateLogSevices _activityStateLogService;
        private readonly IClassroomStudentRepository _classroomStudentRepo;
        private readonly IClassroomTrainersRepository _classroomTrainersRepository;
        private readonly IStudentRepository _studentRepo;
        private readonly ITrainerRepository _trainerRepo;
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        public ClassroomManager(IClassroomRepository classroomRepository, IMapper mapper, IEducationRepository educationRepository, ICategoryRepository categoryRepository, IActivityStateLogSevices activityStateLogService, IClassroomStudentRepository classroomStudentRepository, IClassroomTrainersRepository classroomTrainersRepository, IStudentRepository studentRepo, ITrainerRepository trainerRepo, IStringLocalizer<Resource> stringLocalizer)
        {
            _classroomRepository = classroomRepository;
            _mapper = mapper;
            _educationRepository = educationRepository;
            _categoryRepository = categoryRepository;
            _activityStateLogService = activityStateLogService;
            _classroomStudentRepo = classroomStudentRepository;
            _classroomTrainersRepository = classroomTrainersRepository;
            _studentRepo = studentRepo;
            _trainerRepo = trainerRepo;
            _stringLocalizer = stringLocalizer;
        }
        /// <summary>
        /// Bu metot yeni bir Classroom nesnesi oluşturmak için kullanılmaktadır.
        /// </summary>
        /// <param name="classroomCreateDto"></param>
        /// <returns></returns>
        public async Task<IResult> CreateAsync(ClassroomCreateDto classroomCreateDto)
        {
            if (await _classroomRepository.AnyAsync(x => x.Name.ToLower() == classroomCreateDto.Name.ToLower()))
                return new ErrorResult(_stringLocalizer[Messages.ClassroomAlreadyExists]);
            var toBeCreated = _mapper.Map<Classroom>(classroomCreateDto);
            await _classroomRepository.AddAsync(toBeCreated);
            await _classroomRepository.SaveChangesAsync();
            var classroomDto = _mapper.Map<ClassroomDto>(toBeCreated);
            return new SuccessDataResult<ClassroomDto>(classroomDto, _stringLocalizer[Messages.AddSuccess]);
        }
        /// <summary>
        /// Bu metot bir Classroom nesnesi silmek için kullanılmaktadır.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IResult> DeleteAsync(Guid id)
        {
            var hasClassroom = await _classroomRepository.GetByIdAsync(id, true);
            if (hasClassroom == null)
                return new ErrorResult(_stringLocalizer[Messages.ClassroomNotFound]);
            await _classroomRepository.DeleteAsync(hasClassroom);
            await _classroomRepository.SaveChangesAsync();
            return new SuccessResult(_stringLocalizer[Messages.DeleteSuccess]);
        }
        /// <summary>
        /// Bu metot bir Classroom nesnesini güncellemek için kullanılmaktadır.
        /// </summary>
        /// <param name="classroomUpdateDto"></param>
        /// <returns></returns>
        public async Task<IResult> UpdateAsync(ClassroomUpdateDto classroomUpdateDto)
        {
            bool classroomControl = await _classroomRepository.AnyAsync(x => x.Name.ToLower() == classroomUpdateDto.Name.ToLower());
            var entity = await _classroomRepository.GetByIdAsync(classroomUpdateDto.Id);
            if(entity.Name != classroomUpdateDto.Name)
            if (classroomControl)
                return new ErrorResult(_stringLocalizer[Messages.ClassroomAlreadyExists]);

            if (entity == null)
                return new ErrorResult(_stringLocalizer[Messages.ClassroomNotFound]);
            var updatedEntity = _mapper.Map(classroomUpdateDto, entity);
            await _classroomRepository.UpdateAsync(updatedEntity);
            await _classroomRepository.SaveChangesAsync();
            return new SuccessDataResult<ClassroomUpdateDto>(classroomUpdateDto, _stringLocalizer[Messages.UpdateSuccess]);
        }
        /// <summary>
        /// Bu metot veritabanındaki tüm classroom nesnelerini liste şeklinde göstermek için kullanılmaktadır.
        /// </summary>
        /// <returns></returns>
        public async Task<IResult> GetAllAsync()
        {
            var classrooms = await _classroomRepository.GetAllAsync();
            if (classrooms.Count() <= 0)
                return new SuccessResult(_stringLocalizer[Messages.ListHasNoElements]);
            var classroomsDtos = _mapper.Map<IEnumerable<ClassroomListDto>>(classrooms);
            return new SuccessDataResult<IEnumerable<ClassroomListDto>>(classroomsDtos, _stringLocalizer[Messages.ListedSuccess]);
        }
        /// <summary>
        /// Bu metot parametre olarak verilen id'ye sahip Classroom nesnesinin gösterilmesi için kullanılmaktadır.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IResult> GetByIdAsync(Guid id)
        {
            var classroom = await _classroomRepository.GetByIdAsync(id);
            if (classroom == null)
                return new ErrorResult(_stringLocalizer[Messages.ClassroomNotFound]);
            var classroomDto = _mapper.Map<ClassroomDto>(classroom);
            return new SuccessDataResult<ClassroomDto>(classroomDto, _stringLocalizer[Messages.FoundSuccess]);
        }
        /// <summary>
        /// Bu metot girilen id ye göre classroom nesnesinin teknik detaylarını getirme işlemi yapmaktadır.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IResult> GetDetails(Guid id)
        {
            var classroom = await _classroomRepository.GetByIdAsync(id);
            if (classroom == null)
                return new ErrorResult(_stringLocalizer[Messages.ClassroomNotFound]);
            var technicDto = _mapper.Map<ClassroomTechnicDto>(classroom);
            return new SuccessDataResult<ClassroomTechnicDto>(technicDto, _stringLocalizer[Messages.FoundSuccess]);
        }

        /// <summary>
        /// Bu metot veritabanındaki tüm classroom nesnelerini liste şeklinde göstermek için kullanılmaktadır.
        /// </summary>
        /// <returns></returns>
        public async Task<IResult> ActiveListAsync()
        {
            var classrooms = await _classroomRepository.GetAllAsync(x => x.Status == Status.Active);
            if (classrooms.Count() <= 0)
                return new SuccessResult(_stringLocalizer[Messages.ListHasNoElements]);
            var classroomsDtos = _mapper.Map<IEnumerable<ClassroomListDto>>(classrooms);
            return new SuccessDataResult<IEnumerable<ClassroomListDto>>(classroomsDtos, _stringLocalizer[Messages.ListedSuccess]);
        }
        public async Task<IResult> UpdateActiveAsync(ClassroomActiveUpdateDto classroomActiveUpdateDto)
        {
            int countStudent = 0;
            int countTrainer = 0;
            Classroom? classroom = await _classroomRepository.GetByIdAsync(classroomActiveUpdateDto.Id);
            if (classroom == null)
                return new ErrorResult(_stringLocalizer[Messages.ClassroomNotFound]);
            else if (classroom.Status == classroomActiveUpdateDto.Status)
                return new ErrorResult(_stringLocalizer[Messages.ClassroomActivityNotChanged]);

            var classroomStudent = await _classroomStudentRepo.GetAllAsync(x => x.ClassroomId == classroom.Id && x.Status == Status.Active);
            if (classroomStudent.Count() > 0)
            {
                foreach (var item in classroomStudent)
                {
                    Student? student = await _studentRepo.GetByIdAsync(item.StudentId);
                    if (student == null || student.Status== Status.Deleted)
                    {
                        item.Status = Status.Deleted;
                        countStudent++;
                    }
                    await _classroomStudentRepo.UpdateAsync(item);
                }
                await _classroomStudentRepo.SaveChangesAsync();
            }
            var classroomTrainer = await _classroomTrainersRepository.GetAllAsync(x => x.ClassroomId == classroom.Id && x.Status == Status.Active);
            if (classroomTrainer.Count() > 0)
            {
                foreach (var item in classroomTrainer)
                {
                    Trainer? trainer = await _trainerRepo.GetByIdAsync(item.TrainerId);
                    if (trainer == null || trainer.Status == Status.Deleted)
                    {
                        item.Status = Status.Deleted;
                        countTrainer++;
                    }
                    await _classroomTrainersRepository.UpdateAsync(item);
                }
                await _classroomTrainersRepository.SaveChangesAsync();
            }
            Classroom? newClassroom = _mapper.Map(classroomActiveUpdateDto, classroom);
            ActivityStateLogCreateDto? activityStateLogCreateDto = _mapper.Map<ActivityStateLogCreateDto>(classroomActiveUpdateDto);
            await _classroomRepository.UpdateAsync(newClassroom);
            await _activityStateLogService.CreateAsync(activityStateLogCreateDto);
            await _classroomRepository.SaveChangesAsync();
            return new SuccessResult(_stringLocalizer[Messages.UpdateSuccess] + $" etkilenen classroomStudent = {countStudent} classroomTrainer = {countTrainer}");
        }
        public async Task<IResult> DeletedListAsync()
        {
            var entites = await _classroomRepository.GetAllDeletedAsync();
            if (entites.Count() <= 0)
            {
                return new ErrorResult(_stringLocalizer[Messages.ListedFail]);
            }
            return new SuccessDataResult<List<ClassromDeletedListDto>>(_mapper.Map<List<ClassromDeletedListDto>>(entites), _stringLocalizer[Messages.ListedSuccess]);
        }

        /// <summary>
        /// Bu metot classroom ıd ye göre öğrenci sayısını getirmektedir.
        /// </summary>
        /// <returns></returns>
        public async Task<IResult> GetStudentsCountByClassroomId(Guid id)
        {
            var classroom = await _classroomRepository.GetByIdAsync(id);
            if (classroom == null)
                return new ErrorResult(_stringLocalizer[Messages.ClassroomNotFound]);
            var classroomStudents = await _classroomStudentRepo.GetAllAsync(cs => cs.ClassroomId == id);
            var studentCount = classroomStudents.Count();
            return new SuccessDataResult<int>(studentCount, _stringLocalizer[Messages.FoundSuccess]);
        }

        /// <summary>
        /// Bu metot classroom ıd ye göre trainer adı ve soyadını getirmektedir.
        /// </summary>
        /// <returns></returns>
        public async Task<IResult> GetTrainersByClassroomId(Guid id)
        {
            var classroom = await _classroomRepository.GetByIdAsync(id);
            if (classroom == null)
                return new ErrorResult(_stringLocalizer[Messages.ClassroomNotFound]);
            var classroomTrainers = await _classroomTrainersRepository.GetAllAsync(t => t.ClassroomId == classroom.Id);
            var Trainers = classroomTrainers.Select(x => x.Trainer.FullName).ToList();
            return new SuccessDataResult<IEnumerable>(Trainers, _stringLocalizer[Messages.ListedSuccess]);
        }


        /// <summary>
        /// Bu metot educationId ye göre aktif sınıf gruplarının listelenmesini sağlamaktadır
        /// </summary>
        /// <returns></returns>
        public async Task<IResult> GetActiveClassroomsByEducationId(Guid educationId)
        {

            var classroomList = await _classroomRepository.GetAllAsync(x => x.EducationId == educationId && x.Status == Status.Active);
            if (classroomList.Count() <= 0)
            {
                return new ErrorResult(_stringLocalizer[Messages.ListHasNoElements]);
            }

            var activeClassroomListByEducation = _mapper.Map<List<ActiveClassroomByEducationDto>>(classroomList);

            return new SuccessDataResult<List<ActiveClassroomByEducationDto>>(activeClassroomListByEducation, _stringLocalizer[Messages.ListedSuccess]);

        }
    }

}
