using BACampusApp.Core.Enums;
using BACampusApp.Entities.DbSets;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;

namespace BACampusApp.Business.Concretes
{
    public class ClassroomStudentManager : IClassroomStudentService
    {
        private readonly IClassroomStudentRepository _classroomStudentRepository;
        private readonly IMapper _mapper;
        private readonly IActivityStateLogSevices _activityStateLogService;
        private readonly ITrainerRepository _trainerRepository;
        private readonly IClassroomRepository _classroomRepository;
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        private readonly IClassroomTrainersRepository _classroomTrainersRepository;
        private readonly IStudentRepository _studentRepository;

        public ClassroomStudentManager(IClassroomStudentRepository classroomStudentRepository, IMapper mapper, IActivityStateLogSevices activityStateLogService, IStudentRepository studentRepository, IClassroomRepository classroomRepository, IStringLocalizer<Resource> stringLocalizer, IClassroomTrainersRepository classroomTrainersRepository, ITrainerRepository trainerRepository)
        {
            _classroomStudentRepository = classroomStudentRepository;
            _mapper = mapper;
            _activityStateLogService = activityStateLogService;
            _studentRepository = studentRepository;
            _classroomRepository = classroomRepository;
            _stringLocalizer = stringLocalizer;
            _classroomTrainersRepository = classroomTrainersRepository;
            _trainerRepository = trainerRepository;
        }

        /// <summary>
        /// Bu metot yeni bir ClassroomStudent nesnesi oluşturmak için kullanılmaktadır.
        /// </summary>
        /// <param name="classroomStudentCreateDto">Oluşturulacak nesnenin verilerini içeren ClassroomStudentCreateDto nesnesi.</param>
        /// <returns>ErrorResult, SuccessDataResult<ClassroomStudentDto></returns>
        public async Task<IResult> CreateAsync(ClassroomStudentCreateDto classroomStudentCreateDto)
        {
            var student = await _studentRepository.GetByIdAsync(classroomStudentCreateDto.StudentId);

            var classroom = await _classroomRepository.GetByIdAsync(classroomStudentCreateDto.ClassroomId);
            var classroomTrainer = await _classroomTrainersRepository.GetAllAsync(c => c.ClassroomId == classroom.Id);
            if (classroomTrainer != null)
            {
                foreach (var t in classroomTrainer)
                {

                    var trainer = await _trainerRepository.GetByIdAsync(t.TrainerId);

                    // trainer nesnesine erişerek yapmak istediğiniz işlemleri yapabilirsiniz.
                    // Örnek olarak trainer.Id'yi kullanabilirsiniz.
                    if (trainer != null)
                    {
                        if (trainer.Email == student.Email)
                        {
                            return new ErrorResult(_stringLocalizer[Messages.TrainerAndStudentCannotBeTheSamePerson]);
                        }
                    }

                }
            }

            bool dateControl = (classroom != null ) ? true : false;
            if (!dateControl)
                return new ErrorResult(_stringLocalizer[Messages.AddFail]);
            var hasEntity = await _classroomStudentRepository.AnyAsync(x => x.StudentId == classroomStudentCreateDto.StudentId && x.ClassroomId == classroomStudentCreateDto.ClassroomId);
            if (hasEntity)
                return new ErrorResult(_stringLocalizer[Messages.StudentAlreadyExistsInClassroom]);

            ClassroomStudent entity = _mapper.Map<ClassroomStudent>(classroomStudentCreateDto);
            await _classroomStudentRepository.AddAsync(entity);
            await _classroomStudentRepository.SaveChangesAsync();
            return new SuccessDataResult<ClassroomStudentDto>(_mapper.Map<ClassroomStudentDto>(entity), _stringLocalizer[Messages.AddSuccess]);
        }
        /// <summary>
        /// Bu metot verilen id'ye göre ClassroomStudent nesnesi silmek için kullanılmaktadır.
        /// </summary>
        /// <param name="id">Silinecek nesne için verilen id</param>
        /// <returns>ErrorResult,SuccessResult</returns>
        public async Task<IResult> DeleteAsync(Guid id)
        {
            var hasEntity = await _classroomStudentRepository.GetByIdAsync(id);
            if (hasEntity == null)
                return new ErrorResult(_stringLocalizer[Messages.ClassroomStudentNotFound]);
            await _classroomStudentRepository.DeleteAsync(hasEntity);
            await _classroomStudentRepository.SaveChangesAsync();
            return new SuccessResult(_stringLocalizer[Messages.DeleteSuccess]);
        }
        /// <summary>
        /// Bu metot tüm ClassromStudent nesnlerini liste olarak dönmek için kullanılmaktadır.
        /// </summary>
        /// <returns>SuccessResult, SuccessDataResult<IEnumerable<ClassroomStudentListDto>></returns>
        public async Task<IResult> GetAllAsync()
        {
            var entities = await _classroomStudentRepository.GetAllAsync();
            if (entities.Count() <= 0)
                return new SuccessResult(_stringLocalizer[Messages.ListHasNoElements]);
            var entitiesDtos = _mapper.Map<IEnumerable<ClassroomStudentListDto>>(entities);
            return new SuccessDataResult<IEnumerable<ClassroomStudentListDto>>(entitiesDtos, _stringLocalizer[Messages.ListedSuccess]);
        }
        /// <summary>
        /// Bu metot verilen id'ye göre ClassroomStudent nesnesini geri göndürmek için kullanılmaktadır.
        /// </summary>
        /// <param name="id">Veritabanından getirilecek olan ClassroomStudent nesnesi için verilen id</param>
        /// <returns>ErrorResult, SuccessDataResult<ClassroomStudentDto></returns>
        public async Task<IResult> GetByIdAsync(Guid id)
        {
            var hasEntity = await _classroomStudentRepository.GetByIdAsync(id);
            if (hasEntity == null)
                return new ErrorResult(_stringLocalizer[Messages.ClassroomStudentNotFound]);
            var entityDto = _mapper.Map<ClassroomStudentDto>(hasEntity);
            return new SuccessDataResult<ClassroomStudentDto>(entityDto, _stringLocalizer[Messages.FoundSuccess]);
        }
        /// /// <summary>
        /// Bu metot verilen ClassroomStudent nesnesini güncellemek için kullanılmaktadır.
        /// </summary>
        /// <param name="classroomStudentUpdateDto">Güncellenecek nesne için verilen güncel bilgilerin yer aldığı ClassroomStudentUpdateDto nesnesi</param>
        /// <returns>ErrorResult,SuccessResult</returns>
        public async Task<IResult> UpdateAsync(ClassroomStudentUpdateDto classroomStudentUpdateDto)
        {
            var hasEntity = await _classroomStudentRepository.GetByIdAsync(classroomStudentUpdateDto.Id);
            if (hasEntity == null)
                return new ErrorResult(_stringLocalizer[Messages.ClassroomStudentNotFound]);
            var updatedEntity = _mapper.Map(classroomStudentUpdateDto, hasEntity);
            await _classroomStudentRepository.UpdateAsync(updatedEntity);
            await _classroomStudentRepository.SaveChangesAsync();
            return new SuccessResult(_stringLocalizer[Messages.UpdateSuccess]);
        }
        /// <summary>
        /// Bu metot tüm ClassromStudent nesnlerinin aktif olanalrını liste olarak dönmek için kullanılmaktadır.
        /// </summary>
        /// <returns>SuccessResult, SuccessDataResult<IEnumerable<ClassroomStudentListDto>></returns>
        public async Task<IResult> ActiveListAsync()
        {
            var entities = await _classroomStudentRepository.GetAllAsync(x => x.Status== Status.Active);
            if (entities.Count() <= 0)
                return new SuccessResult(_stringLocalizer[Messages.ListHasNoElements]);
            var entitiesDtos = _mapper.Map<IEnumerable<ClassroomStudentListDto>>(entities);
            return new SuccessDataResult<IEnumerable<ClassroomStudentListDto>>(entitiesDtos, _stringLocalizer[Messages.ListedSuccess]);
        }
        /// <summary>
        /// Bu metot tüm ClassromStudent nesnlerinin aktif olanalrını liste olarak dönmek için kullanılmaktadır.
        /// </summary>
        /// <returns>SuccessResult, SuccessDataResult<IEnumerable<ClassroomStudentListDto>></returns>
        public async Task<IResult> UpdateActiveAsync(ClassroomStudentActiveUpdateDto classroomStudentActiveUpdateDto)
        {
            ClassroomStudent? clasroomStudent = await _classroomStudentRepository.GetByIdAsync(classroomStudentActiveUpdateDto.Id);
            if (clasroomStudent == null)
                return new ErrorResult(_stringLocalizer[Messages.ClassroomStudentNotFound]);
            else if (clasroomStudent.Status == classroomStudentActiveUpdateDto.Status)
                return new ErrorResult(_stringLocalizer[Messages.ClassroomStudentNotFound]);

            Student? student = await _studentRepository.GetByIdAsync(clasroomStudent.StudentId);
            Classroom? clasroom = await _classroomRepository.GetByIdAsync(clasroomStudent.ClassroomId);

            if (student == null && classroomStudentActiveUpdateDto.Status == Status.Active)
                return new ErrorResult(_stringLocalizer[Messages.StudentNotFound]);
            else if (clasroom == null && classroomStudentActiveUpdateDto.Status == Status.Active)
                return new ErrorResult(_stringLocalizer[Messages.ClassroomNotFound]);
            else if (classroomStudentActiveUpdateDto.Status == Status.Active == true && student.Status == Status.Deleted)
                return new ErrorResult(_stringLocalizer[Messages.StudentNotActive]);
            else if (classroomStudentActiveUpdateDto.Status == Status.Active == true && clasroom.Status == Status.Deleted)
                return new ErrorResult(_stringLocalizer[Messages.ClassroomNotActive]);


            ClassroomStudent? newClasroomStudent = _mapper.Map(classroomStudentActiveUpdateDto, clasroomStudent);
            ActivityStateLogCreateDto? activityStateLogCreateDto = _mapper.Map<ActivityStateLogCreateDto>(classroomStudentActiveUpdateDto);


            await _classroomStudentRepository.UpdateAsync(newClasroomStudent);
            await _activityStateLogService.CreateAsync(activityStateLogCreateDto);
            await _classroomStudentRepository.SaveChangesAsync();
            return new SuccessResult(_stringLocalizer[Messages.UpdateSuccess]);
        }
        public async Task<IResult> DeletedListAsync()
        {
            var entities = await _classroomStudentRepository.GetAllDeletedAsync();
            if (entities.Count() <= 0)
            {
                return new ErrorResult(_stringLocalizer[Messages.ListedFail]);
            }
            return new SuccessDataResult<List<ClassromStudentDeletedListDto>>(_mapper.Map<List<ClassromStudentDeletedListDto>>(entities), _stringLocalizer[Messages.ListedSuccess]);
        }

        /// <summary>
        /// Bu metot ClassromStudent tablosunda secili sınıfa ait öğrenci listesini dönmek için kullanılmaktadır.
        /// </summary>
        /// <returns>SuccessResult, SuccessDataResult<IEnumerable<StudentListByClassroomIdDto>></returns>
        public async Task<IResult> StudentsByClassroomIdAsync(Guid? clasroomId)
        {
            var classroomStudents = await _classroomStudentRepository.GetAllAsync(cs => cs.ClassroomId == clasroomId.Value);
            var classroomStudentsList = classroomStudents.ToList();
            var deletedStudents = classroomStudents.Where(a => a.Student.Status == Status.Deleted).ToList();
            if (deletedStudents.Count()>0)
            {
                foreach (var item in deletedStudents)
                {
                    
                    classroomStudentsList.RemoveAll(s => s.Student == item.Student);
                }
            }
            if (classroomStudentsList.Count() < 0)
            {
                return new ErrorResult(_stringLocalizer[Messages.ListedFail]);
            }
            return new SuccessDataResult<List<StudentListByClassroomIdDto>>(_mapper.Map<List<StudentListByClassroomIdDto>>(classroomStudentsList), _stringLocalizer[Messages.ListedSuccess]);
        }

        /// <summary>
        /// Bu metot ClassromStudent tablosunda secili öğrenciye ait tüm sınıf bilgisini dönmek için kullanılır
        /// </summary>
        /// <returns>SuccessResult, SuccessDataResult<IEnumerable<StudentListByClassroomIdDto>></returns>
        public async Task<IResult> AllClassroomsByStudentIdAsync(Guid? studentId)
        {
            Student student= await _studentRepository.GetByIdentityId(studentId.ToString());
            var classroomStudents = await _classroomStudentRepository.GetAllAsync(cs => cs.StudentId == student.Id);

            if (classroomStudents.Count() <= 0)
            {
                return new ErrorResult(_stringLocalizer[Messages.ListedFail]);
            }
            return new SuccessDataResult<List<ActiveClassroomByEducationDto>>(_mapper.Map<List<ActiveClassroomByEducationDto>>(classroomStudents), _stringLocalizer[Messages.ListedSuccess]);
        }

        /// <summary>
        /// Bu metot ClassromStudent tablosunda secili sınıfa ait olmayan öğrencilerin listesini dönmek için kullanılmaktadır.
        /// </summary>
        /// <returns>SuccessResult, SuccessDataResult<IEnumerable<StudentListByClassroomIdDto>></returns>
        public async Task<IResult> ClasslessStudentList(Guid? clasroomId)
        {
            var classroomStudents = await _classroomStudentRepository.GetAllAsync(cs => cs.ClassroomId == clasroomId.Value);
            var studentInClassroom = classroomStudents.Select(cs => cs.StudentId).ToList();
            var resourceStudent = await _studentRepository.GetAllAsync(cs => !studentInClassroom.Contains(cs.Id));
            var studentListByClassroomIdDto = resourceStudent.Select(cs => new StudentListByClassroomIdDto
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

            foreach (var item1 in studentListByClassroomIdDto)
            {
                foreach (var item2 in classroomStudents)
                {
                    if (item2.StudentId == item1.Id)
                    {
                        item1.StartDate = item2.StartDate;
                        item1.EndDate = (DateTime)item2.EndDate;
                    }
                }
            }

            return new SuccessDataResult<List<StudentListByClassroomIdDto>>(_mapper.Map<List<StudentListByClassroomIdDto>>(studentListByClassroomIdDto), _stringLocalizer[Messages.ListedSuccess]);
        }
    }
}
