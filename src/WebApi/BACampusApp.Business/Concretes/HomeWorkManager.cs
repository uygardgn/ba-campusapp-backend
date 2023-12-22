using BACampusApp.Business.Abstracts;
using BACampusApp.Core.Enums;
using BACampusApp.Entities.DbSets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace BACampusApp.Business.Concretes
{
    public class HomeWorkManager : IHomeWorkService
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly ITrainerRepository _trainerRepository;
        private readonly IMapper _mapper;
        private readonly IHomeWorkRepository _homeWorkRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        private readonly IClassroomTrainersRepository _classroomTrainersRepository;
        private readonly IClassroomStudentRepository _classroomStudentRepository;
        private readonly IStudentHomeworkRepository _studentHomeworkRepository;

        public HomeWorkManager(IHomeWorkRepository homeWorkRepository, ISubjectRepository subjectRepository, IMapper mapper, IWebHostEnvironment webHostEnvironment, IStringLocalizer<Resource> stringLocalizer, IClassroomTrainersRepository classroomTrainersRepository, IClassroomStudentRepository classroomStudentRepository, IStudentHomeworkRepository studentHomeworkRepository, IStudentRepository studentRepository, IAdminRepository adminRepository, ITrainerRepository trainerRepository)
        {
            _subjectRepository = subjectRepository;
            _mapper = mapper;
            _homeWorkRepository = homeWorkRepository;
            _webHostEnvironment = webHostEnvironment;
            _stringLocalizer = stringLocalizer;
            _classroomTrainersRepository = classroomTrainersRepository;
            _classroomStudentRepository = classroomStudentRepository;
            _studentHomeworkRepository = studentHomeworkRepository;
            _studentRepository = studentRepository;
            _adminRepository = adminRepository;
            _trainerRepository = trainerRepository;
        }
        public async Task<IResult> AddAsync(HomeWorkCreateDto newHomeWork)
        {
            var homeWork = await _homeWorkRepository.AddAsync(_mapper.Map<HomeWork>(newHomeWork));
            if (newHomeWork.ReferanceFile != null && newHomeWork.ReferanceFile.Length > 0)
            {
                //Video eklemek için ResourceType parametresi eklendi. Burada ihtiyaç olmadığı için null yazıldı.
                var uploadedFile = await FileManagementHelper.UploadFileAsync(newHomeWork.ReferanceFile, FileCategory.Homework,null);

                homeWork.ReferansFile = uploadedFile.FileName;
                homeWork.FileType = uploadedFile.FileType;
                homeWork.ByteArrayFormat = uploadedFile.FileByteArrayFormat;
            }
            await _homeWorkRepository.SaveChangesAsync();
            return new SuccessDataResult<HomeWorkDto>(_mapper.Map<HomeWorkDto>(homeWork), _stringLocalizer[Messages.AddSuccess]);
        }

        /// <summary>
        ///  Bu metot veritabanındaki tüm ödev çeker ve bu ödev listesini HomeWorkListDto ile eşleyip çıktısını verir.
        /// </summary>
        /// <returns>SuccessDataResult<HomeWorkListDto>, ErrorDataResult<HomeWorkListDto></returns>
        public async Task<IResult> ListAsync()
        {
            var homeWorks = await _homeWorkRepository.GetAllAsync();
            var homeWorkDtos = _mapper.Map<List<HomeWorkListDto>>(homeWorks);

            foreach (var homeWorkDto in homeWorkDtos)
            {
                var admin = await _adminRepository.GetByIdentityId(homeWorkDto.CreatedBy);
                var trainer = await _trainerRepository.GetByIdentityId(homeWorkDto.CreatedBy);

                if (admin != null)
                {
                    homeWorkDto.Assignor = $"{admin.FirstName} {admin.LastName}";
                }
                else if (trainer != null)
                {
                    homeWorkDto.Assignor = $"{trainer.FirstName} {trainer.LastName}";
                }
            }

            if (homeWorkDtos.Count <= 0)
            {
                return new ErrorResult(_stringLocalizer[Messages.ListHasNoElements]);
            }

            return new SuccessDataResult<List<HomeWorkListDto>>(homeWorkDtos, _stringLocalizer[Messages.ListedSuccess]);
        }





        public async Task<IResult> PermanentlyDocumentDeleteAsync(HomeWorkDeleteDto homeWorkDeleteDto)
        {
            var deletingHomeWork = await _homeWorkRepository.GetByIdAsync(homeWorkDeleteDto.Id);
            if (deletingHomeWork != null && deletingHomeWork.ReferansFile is not null)
            {
                if (homeWorkDeleteDto.IsHardDelete)
                {
                    var deletedfile = await FileManagementHelper.DeleteFileAsync(deletingHomeWork.ReferansFile, FileCategory.Homework, deletingHomeWork.FileType);
                    deletingHomeWork.ReferansFile = deletedfile.FileName;
                    deletingHomeWork.FileType = deletedfile.FileType;
                    deletingHomeWork.ByteArrayFormat = deletedfile.FileByteArrayFormat;
                }
                else
                {
                    FileManagementHelper.MoveToDeletedFiles(deletingHomeWork.ReferansFile, FileCategory.Homework, deletingHomeWork.FileType);
                }
                await _homeWorkRepository.DeleteAsync(deletingHomeWork);
                await _homeWorkRepository.SaveChangesAsync();
                return new SuccessResult(_stringLocalizer[Messages.DeleteSuccess]);
            }

            return new ErrorResult(_stringLocalizer[Messages.DeleteFail]);


        }
        public async Task<IResult> DeleteAsync(Guid id)
        {
            var hasHomework = await _homeWorkRepository.GetByIdAsync(id);
            if (hasHomework == null)
                return new ErrorResult(_stringLocalizer[Messages.HomeWorkNotFound]);
            await _homeWorkRepository.DeleteAsync(hasHomework);
            await _homeWorkRepository.SaveChangesAsync();
            return new SuccessResult(_stringLocalizer[Messages.DeleteSuccess]);

        }

        /// <summary>
        /// Bu metot HomeWork nesnesinin güncelleme işlemini yapacaktır.
        /// </summary>
        /// <param name="entity">güncellenmek istenen student nesnesinin StudentUpdateDto tipinde entity'si</param>
        /// <returns>ErrorDataResult<StudentUpdateDto>, SuccessDataResult<StudentUpdateDto></returns>

        public async Task<IResult> UpdateAsync(HomeWorkUpdateDto updateHomework)
        {
            var homeWork = await _homeWorkRepository.GetByIdAsync(updateHomework.Id);
            if (homeWork == null)
            {
                return new ErrorResult(_stringLocalizer[Messages.HomeWorkUpdate]);
            }
            var updatedHomeworkEntity = _mapper.Map(updateHomework, homeWork);
            if (updateHomework.ReferanceFile != null && updateHomework.ReferanceFile.Length > 0)
            {
                //Video eklemek için ResourceType parametresi eklendi. Burada ihtiyaç olmadığı için null yazıldı.
                var uploadedFile = await FileManagementHelper.UpdateFileAsync(homeWork.ReferansFile, updateHomework.ReferanceFile, FileCategory.Homework, homeWork.FileType, updateHomework.IsHardDelete,null);

                updatedHomeworkEntity.ReferansFile = uploadedFile.FileName;
                updatedHomeworkEntity.FileType = uploadedFile.FileType;
                updatedHomeworkEntity.ByteArrayFormat = uploadedFile.FileByteArrayFormat;
            }
            await _homeWorkRepository.UpdateAsync(updatedHomeworkEntity);
            await _homeWorkRepository.SaveChangesAsync();
            return new SuccessResult(_stringLocalizer[Messages.UpdateSuccess]);
        }

        /// <summary>
        /// GetByIdAsync() metodu database de kayıtlı id si verilen HomeWork'i çeker ve HomeWorkDto'ya Map'leyerek HomeWorkDto nesnesine çevirir. En son olarak bu nesneyi ve işlemin durumuna göre verilmek istenen mesajı birlikte döner.
        /// </summary>
        /// /// <param name="id">detayları getirilmek istenen homework nesnesinin Guid tipinde Id si </param>
        /// <returns>SuccessDataResult<HomeWorkDto>(HomeWorkDto, Messages.FoundSuccess)</returns> 
        public async Task<IResult> GetByIdAsync(Guid id)
        {
            var homeWork = await _homeWorkRepository.GetByIdAsync(id);
            if (homeWork == null)
            {
                return new ErrorResult(_stringLocalizer[Messages.HomeWorkNotFound]);
            }
            var homeWorkDto = _mapper.Map<HomeWorkDto>(homeWork);
            return new SuccessDataResult<HomeWorkDto>(homeWorkDto, _stringLocalizer[Messages.FoundSuccess]);
        }

        public async Task<IResult> DeletedListAsync()
        {
            var homeWork = await _homeWorkRepository.GetAllDeletedAsync();
            if (homeWork.Count() <= 0)
            {
                return new ErrorResult(_stringLocalizer[Messages.ListedFail]);
            }
            return new SuccessDataResult<List<HomeWorkDeletedListDto>>(_mapper.Map<List<HomeWorkDeletedListDto>>(homeWork), _stringLocalizer[Messages.ListedSuccess]);
        }

        public FileContentResult DownloadDocumentHomework(string filePath, Guid homeworkId)
        {
            string dosyaYolu = "";
            string mimeType = "";
            byte[] fileBytes;
            string fileName = filePath;
            string extension = Path.GetExtension(fileName);
            if (extension == ".pdf" || extension == ".zip")
            {
                dosyaYolu = Path.Combine(_webHostEnvironment.WebRootPath, "documents", "Homework", filePath);

                if (!System.IO.File.Exists(dosyaYolu))
                {
                    return null;
                }
                fileBytes = System.IO.File.ReadAllBytes(dosyaYolu);
                mimeType = FileManagementHelper.GetMimeType(fileName);

            }
            else
            {
                var homework = _homeWorkRepository.GetByIdAsync(homeworkId);
                fileBytes = homework.Result.ByteArrayFormat;
                mimeType = FileManagementHelper.GetMimeType(fileName);
            }

            var fileContentResult = new FileContentResult(fileBytes, mimeType)
            {
                FileDownloadName = fileName
            };
            return fileContentResult;

        }

        /// <summary>
        /// Bu method wwwroot>documenta>Homework>DeletedHomework içerisindeki dosyaların linklerini döndürür
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="homeworkId"></param>
        /// <returns></returns>
        public List<FileContentResult> GetAllDeletedFiles()
        {
            string targetPath = Path.Combine(_webHostEnvironment.WebRootPath, "documents", "Homework", "DeletedHomework");
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
        /// Bu method uygulamaya giriş yapan trainer'ın id'si ile sadece kendisi tarafından atanan ödevlerin listelenmesi
        /// </summary>
        /// <param name="trainerId"></param>
        /// <returns></returns>
        public async Task<IResult> GetHomeworkByTrainer(Guid trainerId)
        {
            var homeworkList = await _homeWorkRepository.GetAllAsync(x => x.CreatedBy == trainerId.ToString());


            var homeList = _mapper.Map<List<HomeWorkListByTrainerDto>>(homeworkList);
            if (homeList.Count > 0)
            {
                return new SuccessDataResult<List<HomeWorkListByTrainerDto>>(homeList, _stringLocalizer[Messages.ListedSuccess]);
            }

            return new ErrorResult(_stringLocalizer[Messages.ListedFail]);

        }

        /// <summary>
        /// Bu method uygulamaya giriş yapan öğrencinin kendi id sine göre kendisine atanan ödevleri listeler. 
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>

        public async Task<IResult> GetAllHomeworkByStudentId(Guid studentId)
        {
            string userId = studentId.ToString();
            var student = _studentRepository.GetAllAsync(x => x.IdentityId == userId).Result.FirstOrDefault();
            var studentHomeworks = await _studentHomeworkRepository.GetAllAsync(s => s.StudentId == student.Id);

            List<HomeworkListByStudentDto> homeworkList = new List<HomeworkListByStudentDto>();

            foreach (var studentHomework in studentHomeworks)
            {
                var homework = await _homeWorkRepository.GetAllAsync(x => x.Id == studentHomework.HomeWorkId);
                if (homework.Any())
                {
                    homeworkList.AddRange(_mapper.Map<List<HomeworkListByStudentDto>>(homework));
                }
            }

            var studentHomeWList = _mapper.Map<List<HomeworkListByStudentDto>>(homeworkList);
            if (studentHomeWList.Count > 0)
            {
                return new SuccessDataResult<List<HomeworkListByStudentDto>>(studentHomeWList, _stringLocalizer[Messages.ListedSuccess]);
            }

            return new ErrorResult(_stringLocalizer[Messages.ListedFail]);
        }
    }
}
