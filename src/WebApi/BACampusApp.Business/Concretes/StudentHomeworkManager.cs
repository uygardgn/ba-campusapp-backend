using BACampusApp.Dtos.StudentHomework;
using BACampusApp.Dtos.Students;
using BACampusApp.Entities.DbSets;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace BACampusApp.Business.Concretes
{
    public class StudentHomeworkManager : IStudentHomeworkService
    {
        private readonly IMapper _mapper;
        private readonly IStudentRepository _studentRepository;
        private readonly IHomeWorkRepository _homeWorkRepository;
        private readonly IStudentHomeworkRepository _studentHomeworkRepository;
        private readonly IClassroomStudentRepository _classroomStudentRepository;
        private readonly IClassroomRepository _classroomRepository;
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public StudentHomeworkManager(IMapper mapper, IStudentRepository studentRepository, IHomeWorkRepository homeWorkRepository, IStudentHomeworkRepository studentHomeworkRepository, IClassroomStudentRepository classroomStudentRepository, IClassroomRepository classroomRepository, IStringLocalizer<Resource> stringLocalizer, IWebHostEnvironment webHostEnvironment)
        {
            _mapper = mapper;
            _studentRepository = studentRepository;
            _homeWorkRepository = homeWorkRepository;
            _studentHomeworkRepository = studentHomeworkRepository;
            _classroomStudentRepository = classroomStudentRepository;
            _classroomRepository = classroomRepository;
            _stringLocalizer = stringLocalizer;
            _webHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// Bu metot içerisine gönderilen Liste StudentHomeworkCreateDto nesnesi ile öğrenci için ödev ataması yapar.
        /// </summary>
        /// <param name="StudentHomeworkCreateDto"></param>
        /// <returns>Öğrenciye ödev atama işleminin başarılı/başarısız olma durumu mesajı döner.</returns>
        public async Task<IResult> CreateAsync(List<StudentHomeworkCreateDto> studentHomeworkCreateDto)
        {

            foreach (var studentHomeworkDto in studentHomeworkCreateDto)
            {
                bool isHomework = await _homeWorkRepository.AnyAsync(x => x.Id == studentHomeworkDto.HomeworkId);
                bool isStudent = await _studentRepository.AnyAsync(x => x.Id == studentHomeworkDto.StudentId);
                bool isHomeworkStudent = await _studentHomeworkRepository.AnyAsync(x => x.HomeWorkId == studentHomeworkDto.HomeworkId && x.StudentId == studentHomeworkDto.StudentId);
                if (isHomeworkStudent)
                {
                    return new ErrorResult(_stringLocalizer[Messages.StudentAlreadyExistsInHomework]);
                }
                if (!isHomework)
                {
                    return new ErrorResult(_stringLocalizer[Messages.HomeWorkNotFound]);
                }
                if (!isStudent)
                {
                    return new ErrorResult(_stringLocalizer[Messages.StudentNotFound]);
                }


            }
            var studentHomework = _studentHomeworkRepository.AddRangeAsync(_mapper.Map<List<StudentHomework>>(studentHomeworkCreateDto));
            await _studentHomeworkRepository.SaveChangesAsync();
            return new SuccessResult(_stringLocalizer[Messages.AddSuccess]);

        }


        /// <summary>
        /// Bu metot içerisine gönderilen id'ye ait ödevi siler.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Silme işlemine ilişkin durum mesajı döner.</returns>
        public async Task<IResult> DeleteAsync(StudentHomeworkDeleteDto studentHomeworkDeleteDto)
        {
            var studentHomework = await _studentHomeworkRepository.GetByIdAsync(studentHomeworkDeleteDto.id);
            if (studentHomework == null)
            {
                return new ErrorResult(_stringLocalizer[Messages.HomeWorkNotFound]);
            }
            if (studentHomeworkDeleteDto.IsHardDelete)
            {
                if (studentHomework.AttachedFile is not null)
                {
                    var deletedfile = await FileManagementHelper.DeleteFileAsync(studentHomework.AttachedFile, FileCategory.StudentHomework, studentHomework.FileType);
                    studentHomework.AttachedFile = deletedfile.FileName;
                    studentHomework.FileType = deletedfile.FileType;
                    studentHomework.ByteArrayFormat = deletedfile.FileByteArrayFormat;

                }
            }
            if (studentHomework.AttachedFile is not null)
            {
                FileManagementHelper.MoveToDeletedFiles(studentHomework.AttachedFile, FileCategory.StudentHomework, studentHomework.FileType);
            }
            await _studentHomeworkRepository.DeleteAsync(studentHomework);
            await _studentHomeworkRepository.SaveChangesAsync();
            return new SuccessResult(_stringLocalizer[Messages.DeleteSuccess]);

        }

        /// <summary>
        /// Bu metot içerisine gönderilen studentHomeworkUpdateDto nesnesi ile eğitmen/admin için ödev güncellemesi yapar.
        /// </summary>
        /// <param name="studentHomeworkUpdateDto"></param>
        /// <returns>Öğrenci ödev atama güncelleme işleminin durum mesajı döner.</returns>
        public async Task<IResult> TrainerHomeworkUpdateAsync(StudentHomeworkTrainerUpdateDto studentHomeworkTrainerUpdateDto)
        {
            var studentHomework = await _studentHomeworkRepository.GetByIdAsync(studentHomeworkTrainerUpdateDto.Id);

            if (studentHomework == null)
            {
                return new ErrorResult(_stringLocalizer[Messages.HomeWorkNotFound]);
            }

            var updatedStudentHomework = _mapper.Map(studentHomeworkTrainerUpdateDto, studentHomework);

            if (studentHomeworkTrainerUpdateDto.AttachedFile != null && studentHomeworkTrainerUpdateDto.AttachedFile.Length > 0 && studentHomeworkTrainerUpdateDto.IsFileChanged)
            {
                //Video eklemek için ResourceType parametresi eklendi. Burada ihtiyaç olmadığı için null yazıldı.
                var updatedFile = await FileManagementHelper.UpdateFileAsync(studentHomework.AttachedFile, studentHomeworkTrainerUpdateDto.AttachedFile, FileCategory.StudentHomework, studentHomework.FileType, studentHomeworkTrainerUpdateDto.IsHardDelete,null);
                updatedStudentHomework.AttachedFile = updatedFile.FileName;
                updatedStudentHomework.FileType = updatedFile.FileType;
                updatedStudentHomework.ByteArrayFormat = updatedFile.FileByteArrayFormat;
            }
            if (updatedStudentHomework.HomeWork.IsHasPoint == true && updatedStudentHomework.Point != null)
            {
                return new ErrorResult(_stringLocalizer[Messages.ThisHomeworkIsHasPoint]);
            }
            await _studentHomeworkRepository.UpdateAsync(updatedStudentHomework);
            await _studentHomeworkRepository.SaveChangesAsync();
            return new SuccessDataResult<StudentHomeworkDto>(_mapper.Map<StudentHomeworkDto>(updatedStudentHomework), _stringLocalizer[Messages.UpdateSuccess]);

        }
        /// <summary>
        /// Bu metot içerisine gönderilen studentHomeworkUpdateDto nesnesi ile öğrenci için ödev güncellemesi yapar.
        /// </summary>
        /// <param name="studentHomeworkUpdateDto"></param>
        /// <returns>Öğrenci ödev atama güncelleme işleminin durum mesajı döner.</returns>
        public async Task<IResult> StudentHomeworkUpdateAsync(StudentHomeworkStudentUpdateDto studentHomeworkStudentUpdateDto)
        {
            var studentHomework = await _studentHomeworkRepository.GetByIdAsync(studentHomeworkStudentUpdateDto.Id);
            if (studentHomework == null)
            {
                return new ErrorResult(_stringLocalizer[Messages.HomeWorkNotFound]);
            }

            var updatedStudentHomework = _mapper.Map(studentHomeworkStudentUpdateDto, studentHomework);

            //Video eklemek için ResourceType parametresi eklendi. Burada ihtiyaç olmadığı için null yazıldı.
            var updatedFileStudent = await FileManagementHelper.UpdateFileAsync(studentHomework.AttachedFile, studentHomeworkStudentUpdateDto.AttachedFile, FileCategory.StudentHomework, studentHomework.FileType, studentHomeworkStudentUpdateDto.IsHardDelete, null);
            updatedStudentHomework.AttachedFile = updatedFileStudent.FileName;
            updatedStudentHomework.FileType = updatedFileStudent.FileType;
            updatedStudentHomework.ByteArrayFormat = updatedFileStudent.FileByteArrayFormat;

            updatedStudentHomework.HomeworkState = HomeworkState.TurnedIn;
            updatedStudentHomework.SubmitDate = DateTime.Now;

            if (!updatedStudentHomework.HomeWork.IsLateTurnedIn && updatedStudentHomework.SubmitDate > updatedStudentHomework.HomeWork.EndDate)
            {
                return new ErrorResult(_stringLocalizer[Messages.HomeworkCanNotBeSubmitted]);
            }
            else if (updatedStudentHomework.HomeWork.IsLateTurnedIn && updatedStudentHomework.SubmitDate > updatedStudentHomework.HomeWork.EndDate)
            {
                updatedStudentHomework.HomeworkState = HomeworkState.LateTurnedIn;
            }
            await _studentHomeworkRepository.UpdateAsync(updatedStudentHomework);
            await _studentHomeworkRepository.SaveChangesAsync();
            return new SuccessDataResult<StudentHomeworkDto>(_mapper.Map<StudentHomeworkDto>(updatedStudentHomework), _stringLocalizer[Messages.ResponseSuccess]);
        }

        /// <summary>
        /// Bu metot içerisine gönderilen id'ye ait ödevi getirir.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>İlgili ödev bilgisini döner.</returns>
        public async Task<IResult> GetByIdAsync(Guid id)
        {
            var studentHomework = await _studentHomeworkRepository.GetByIdAsync(id);

            if (studentHomework == null)
            {
                return new ErrorResult(_stringLocalizer[Messages.HomeWorkNotFound]);
            }
            var studentHomeworkDto = _mapper.Map<StudentHomeworkDto>(studentHomework);
            var classroom = await _classroomRepository.GetByIdAsync((Guid)studentHomeworkDto.ClassroomId);
            studentHomeworkDto.ClassroomName = classroom.Name;
            return new SuccessDataResult<StudentHomeworkDto>(studentHomeworkDto, _stringLocalizer[Messages.FoundSuccess]);
        }

        /// <summary>
        /// Bu metot öğrencilere atanan tüm ödevleri getirir.
        /// </summary>
        /// <returns>Ödev atama tablosu verilerinin listenip listelenmediği ile ilgili durum mesajı döner </returns>
        public async Task<IResult> GetAllListAsync()
        {
            var studentHomeworks = await _studentHomeworkRepository.GetAllAsync();
            if (studentHomeworks.Count() <= 0)
            {
                return new ErrorResult(_stringLocalizer[Messages.ListHasNoElements]);
            }
            return new SuccessDataResult<List<StudentHomeworkListDto>>(_mapper.Map<List<StudentHomeworkListDto>>(studentHomeworks), _stringLocalizer[Messages.ListedSuccess]);
        }

        /// <summary>
        /// Bu metot içerisine gönderilen studentId ve homeworkId ile öğrencinin ilgili ödevden aldığı puanı getirir.
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="homeworkId"></param>
        /// <returns>Öğrenciye atanmış ödev puanını döner.</returns>
        public async Task<IResult> GetPoint(Guid studentId, Guid homeworkId)
        {
            var student = await _studentRepository.GetByIdAsync(studentId);
            var homework = await _homeWorkRepository.GetByIdAsync(homeworkId);

            if (student == null)
            {
                return new ErrorResult(_stringLocalizer[Messages.StudentNotFound]);
            }
            if (homework == null)
            {
                return new ErrorResult(_stringLocalizer[Messages.HomeWorkNotFound]);
            }

            var studentHomework = await _studentHomeworkRepository.GetAsync(x => x.StudentId == studentId && x.HomeWorkId == homeworkId);

            if (studentHomework == null)
            {
                return new ErrorResult(_stringLocalizer[Messages.HomeWorkNotFound]);
            }

            var point = _mapper.Map<StudentHomeworkPointDto>(studentHomework);
            return new SuccessDataResult<StudentHomeworkPointDto>(point, _stringLocalizer[Messages.ListedSuccess]);
        }

        /// <summary>
        /// Bu metot içerisine gönderilen studentHomeworkPointDto ile öğrencinin ilgili ödevi için puan verilebilmektedir.
        /// </summary>
        /// <param name="studentHomeworkPointDto"></param>
        /// <returns>İlgili ödev bilgilerini döner.</returns>
        public async Task<IResult> GivePoint(StudentHomeworkPointDto studentHomeworkPointDto)
        {
            var studentHomework = await _studentHomeworkRepository.GetByIdAsync(studentHomeworkPointDto.Id);

            if (studentHomework == null)
            {
                return new ErrorResult(_stringLocalizer[Messages.HomeWorkNotFound]);
            }

            if (studentHomework.SubmitDate == null && studentHomework.HomeWork.EndDate > DateTime.Now)
            {
                return new ErrorResult(_stringLocalizer[Messages.HomeworkNotYetSubmitted]);
            }
            if (studentHomework.AttachedFile == null && studentHomework.HomeWork.EndDate > DateTime.Now)
            {
                return new ErrorResult(_stringLocalizer[Messages.NoFileUploadedToTheHomework]);
            }

            var updatedStudentHomework = _mapper.Map(studentHomeworkPointDto, studentHomework);

            await _studentHomeworkRepository.UpdateAsync(updatedStudentHomework);
            if (updatedStudentHomework == null)
            {
                return new ErrorResult(_stringLocalizer[Messages.AddFail]);
            }
            else
            {
                if (updatedStudentHomework.HomeWork.IsHasPoint == false && updatedStudentHomework.Point != null)
                {
                    return new ErrorResult(_stringLocalizer[Messages.GivePointFail]);
                }
                await _studentHomeworkRepository.SaveChangesAsync();
                return new SuccessDataResult<StudentHomeworkPointDto>(_mapper.Map<StudentHomeworkPointDto>(updatedStudentHomework), _stringLocalizer[Messages.ListedSuccess]);
            }
        }

        public async Task<IResult> DeletedListAsync()
        {
            var studentHomework = await _studentHomeworkRepository.GetAllDeletedAsync();
            if (studentHomework.Count() <= 0)
            {
                return new ErrorResult(_stringLocalizer[Messages.ListedFail]);
            }
            return new SuccessDataResult<List<StudentHomeWorkDeletedListDto>>(_mapper.Map<List<StudentHomeWorkDeletedListDto>>(studentHomework), _stringLocalizer[Messages.ListedSuccess]);
        }
        /// <summary>
        /// Bu metot ClassromStudent tablosunda secili sınıfa ait öğrenci listesini dönmek için kullanılmaktadır.
        /// </summary>
        /// <returns>SuccessResult, SuccessDataResult<IEnumerable<StudentListByHomeworkIdDto>></returns>
        public async Task<IResult> StudentsByHomeworkIdAsync(Guid? homeworkId)
        {
            var studenthomework = await _studentHomeworkRepository.GetAllAsync(a => a.HomeWorkId == homeworkId.Value);
            if (studenthomework.Count() <= 0)
            {
                return new ErrorResult(_stringLocalizer[Messages.ListedFail]);
            }
            return new SuccessDataResult<List<StudentListByHomeworkIdDto>>(_mapper.Map<List<StudentListByHomeworkIdDto>>(studenthomework), _stringLocalizer[Messages.ListedSuccess]);

        }

        public FileContentResult DownloadDocumentStudentHomework(string filePath, Guid studentHomeworkId)
        {
            string dosyaYolu = "";
            string mimeType = "";
            byte[] fileBytes;
            string fileName = filePath;
            string extension = Path.GetExtension(fileName);
            if (extension == ".pdf" || extension == ".zip")
            {
                dosyaYolu = Path.Combine(_webHostEnvironment.WebRootPath, "documents", "StudentHomework", filePath);

                if (!System.IO.File.Exists(dosyaYolu))
                {
                    return null;
                }
                fileBytes = System.IO.File.ReadAllBytes(dosyaYolu);
                mimeType = FileManagementHelper.GetMimeType(fileName);

            }
            else
            {
                var Studenthomework = _studentHomeworkRepository.GetByIdAsync(studentHomeworkId);
                fileBytes = Studenthomework.Result.ByteArrayFormat;
                mimeType = FileManagementHelper.GetMimeType(fileName);
            }

            var fileContentResult = new FileContentResult(fileBytes, mimeType)
            {
                FileDownloadName = fileName
            };
            return fileContentResult;
        }

        public List<FileContentResult> GetAllDeletedFiles()
        {
            string targetPath = Path.Combine(_webHostEnvironment.WebRootPath, "documents", "StudentHomework", "DeletedStudentHomework");
            List<FileContentResult> contentResults = new();

            string fileName = string.Empty;
            string mimeType = string.Empty;
            byte[] fileContent;

            if (Directory.Exists(targetPath))
            {
                string[] filePaths = Directory.GetFiles(targetPath);

                foreach (string filePath in filePaths)
                {
                    fileName = Path.GetFileName(filePath);
                    mimeType = FileManagementHelper.GetMimeType(filePath);
                    fileContent = File.ReadAllBytes(filePath);

                    FileContentResult contentResult = new(fileContent, mimeType) { FileDownloadName = fileName };
                    contentResults.Add(contentResult);
                }
            }
            return contentResults;
        }

        /// <summary>
        /// Bu metot öğrencinin studenthomeworkId sini bulmamızı sağlar.
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="homeworkId"></param>
        public async Task<IResult> GetByStudentIdAndHomeworkId(Guid studentId, Guid homeworkId)
        {
            string userId = studentId.ToString();
            var student = _studentRepository.GetAllAsync(x => x.IdentityId == userId).Result.FirstOrDefault();
            var homework = await _homeWorkRepository.GetByIdAsync(homeworkId);

            if (student == null)
            {
                return new ErrorResult(_stringLocalizer[Messages.StudentNotFound]);
            }
            if (homework == null)
            {
                return new ErrorResult(_stringLocalizer[Messages.HomeWorkNotFound]);
            }

            var studentHomework = await _studentHomeworkRepository.GetAsync(x => x.StudentId == student.Id && x.HomeWorkId == homework.Id);

            if (studentHomework == null)
            {
                return new ErrorResult(_stringLocalizer[Messages.HomeWorkNotFound]);
            }

            return new SuccessDataResult<StudentHomeworkDto>(_mapper.Map<StudentHomeworkDto>(studentHomework), _stringLocalizer[Messages.FoundSuccess]);
        }

        public async Task<IResult> GiveFeedback(StudentHomeworkFeedbackDto studentHomeworkFeedbackDto)
        {
            var studentHomework = await _studentHomeworkRepository.GetByIdAsync(studentHomeworkFeedbackDto.Id);

            if (studentHomework == null)
            {
                return new ErrorResult(_stringLocalizer[Messages.HomeWorkNotFound]);
            }

            var updatedStudentHomework = _mapper.Map(studentHomeworkFeedbackDto, studentHomework);

            await _studentHomeworkRepository.UpdateAsync(updatedStudentHomework);

            if (updatedStudentHomework == null)
            {
                return new ErrorResult(_stringLocalizer[Messages.UpdateFail]);
            }

            await _studentHomeworkRepository.SaveChangesAsync();
            return new SuccessDataResult<StudentHomeworkFeedbackDto>(_mapper.Map<StudentHomeworkFeedbackDto>(updatedStudentHomework), _stringLocalizer[Messages.FeedbackSucceeded]);
        }
    }
}