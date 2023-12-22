using BACampusApp.Business.Abstracts;
using BACampusApp.DataAccess.Interfaces.Repositories;
using BACampusApp.Dtos.SupplementaryResources;
using BACampusApp.Dtos.SupplementaryResourcesEducationsSubjects;
using BACampusApp.Entities.DbSets;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BACampusApp.Business.Concretes
{
    public class SupplementaryResourceManager : ISupplementaryResourceService
    {
        private readonly ISupplementaryResourceRepository _supplementaryResourceRepository;
        private readonly ITagRepository _tagRepository;
        private readonly ISupplementaryResourceTagRepository _supplementaryResourceTagRepository;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<Resource> _stringLocalizer;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IClassroomStudentRepository _classroomStudentRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IEducationSubjectRepository _educationSubjectRepository;
        private readonly ISupplementaryResourceEducationSubjectRepository _supplementaryResourceEducationSubjectRepository;
        private readonly IEducationRepository _educationRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IVideoFileService _videoFileService;
        private readonly IClassroomTrainersRepository _classroomTrainersRepository;
        private readonly ITrainerRepository _trainerRepo;
        private readonly IClassroomRepository _classroomRepository;


        public SupplementaryResourceManager(ISupplementaryResourceRepository supplementaryResourceRepository, ITagRepository tagRepository, ISupplementaryResourceTagRepository supplementaryResourceTagRepository, IMapper mapper, IStringLocalizer<Resource> stringLocalizer, IWebHostEnvironment webHostEnvironment, IClassroomStudentRepository classroomStudentRepository, IStudentRepository studentRepository, IEducationSubjectRepository educationSubjectRepository, ISubjectRepository subjectRepository, IVideoFileService videoFileService, ISupplementaryResourceEducationSubjectRepository supplementaryResourceEducationSubjectRepository, IEducationRepository educationRepository, IClassroomTrainersRepository classroomTrainersRepository, ITrainerRepository trainerRepo, IClassroomRepository classroomRepository)
        {
            _supplementaryResourceRepository = supplementaryResourceRepository;
            _tagRepository = tagRepository;
            _supplementaryResourceTagRepository = supplementaryResourceTagRepository;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
            _webHostEnvironment = webHostEnvironment;
            _classroomStudentRepository = classroomStudentRepository;
            _studentRepository = studentRepository;
            _educationSubjectRepository = educationSubjectRepository;
            _supplementaryResourceEducationSubjectRepository = supplementaryResourceEducationSubjectRepository;
            _educationRepository = educationRepository;
            _subjectRepository = subjectRepository;
            _videoFileService = videoFileService;
            _classroomTrainersRepository = classroomTrainersRepository;
            _trainerRepo = trainerRepo;
            _classroomRepository = classroomRepository;
        }

        /// <summary>
        /// Bu metot SupplementaryResource nesnesi oluşturma işlemini yapmaktadır.
        /// </summary>
        /// <param name="newSupplementaryResource"></param>
        /// <returns></returns>
        public async Task<IResult> AddAsync(SupplementaryResourceCreateDto newSupplementaryResource)
        {
            bool hasSupplementaryResource = await _supplementaryResourceRepository.AnyAsync(s => s.Name.ToLower() == newSupplementaryResource.Name.ToLower());
            if (hasSupplementaryResource)
            {
                return new ErrorResult(_stringLocalizer[Messages.AddFailAlreadyExists]);
            }

            if (newSupplementaryResource.ResourceType == (int)ResourceType.Video)
            {
                var isDurationValid = await _videoFileService.CheckVideoDurationAsync(newSupplementaryResource.FileURL);
                if (isDurationValid)
                {

                    var newFormattedVideo = await _videoFileService.ConvertTo480pAsync(newSupplementaryResource.FileURL);
                    var isSizeValid = await _videoFileService.CheckVideoSizeAsync(newFormattedVideo);

                    if (isSizeValid)
                        newSupplementaryResource.FileURL = newFormattedVideo;
                    else
                        return new ErrorResult(_stringLocalizer[Messages.VideoSizeIsNotValid]);


                }
                else
                    return new ErrorResult(_stringLocalizer[Messages.VideoDurationIsNotValid]);
            }






            var supplementaryResource = await _supplementaryResourceRepository.AddAsync(_mapper.Map<SupplementaryResource>(newSupplementaryResource));

            if (newSupplementaryResource.FileURL != null && newSupplementaryResource.FileURL.Length > 0)
            {
                var uploadedFile = await FileManagementHelper.UploadFileAsync(newSupplementaryResource.FileURL, FileCategory.SupplementaryResource, newSupplementaryResource.ResourceType);

                supplementaryResource.FileURL = uploadedFile.FileName;
                supplementaryResource.FileType = uploadedFile.FileType;
                supplementaryResource.ByteArrayFormat = uploadedFile.FileByteArrayFormat;
            }
            else if (!string.IsNullOrEmpty(newSupplementaryResource.Link))
            {
                // Linkin geçerli bir URL formatına sahip olup olmadığını kontrol et
                if (Uri.TryCreate(newSupplementaryResource.Link, UriKind.Absolute, out Uri? uriResult) &&
                    (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
                {
                    // Link geçerli bir URL formatına sahip
                    supplementaryResource.FileURL = newSupplementaryResource.Link;
                    supplementaryResource.FileType = newSupplementaryResource.FileType;
                }
                else
                {
                    // Link geçerli bir URL formatına sahip değil
                    return new ErrorResult(_stringLocalizer[Messages.LinkFormatFail]);
                }
            }

            else
            {
                supplementaryResource.FileType = newSupplementaryResource.FileType;
            }



            foreach (var item in newSupplementaryResource.Educations)
            {
                var education = await _educationRepository.GetAsync(t => t.Id == item);
                foreach (var subjectId in newSupplementaryResource.Subjects)
                {
                    var subject = await _subjectRepository.GetAsync(t => t.Id == subjectId);
                    var EducationSubject = await _educationSubjectRepository.GetAllAsync(t => t.EducationId == item && t.SubjectId == subjectId);
                    if (EducationSubject.Any())
                    {
                        SupplementaryResourceEducationSubject newSupplementaryResourceEducationSubject = new()
                        {
                            SupplementaryResourceId = supplementaryResource.Id,
                            SubjectId = subjectId,
                            EducationId = education.Id
                        };
                        SupplementaryResourceEducationSubject addSupplementaryResourceEducationSubject = await _supplementaryResourceEducationSubjectRepository.AddAsync(newSupplementaryResourceEducationSubject);
                    }

                }
            }
            foreach (Guid item in newSupplementaryResource.Tags)
            {
                var tag = await _tagRepository.GetAsync(t => t.Id == item);
                if (tag != null)
                {
                    SupplementaryResourceTag newSupplementaryResourceTag = new()
                    {
                        SupplementaryResourceId = supplementaryResource.Id,
                        TagId = tag.Id,
                    };
                    SupplementaryResourceTag addSupplementaryResourceTag = await _supplementaryResourceTagRepository.AddAsync(newSupplementaryResourceTag);
                }
                else
                {
                    return new ErrorResult(_stringLocalizer[Messages.TagIsNotFound]);
                }
            }
            await _supplementaryResourceRepository.SaveChangesAsync();
            return new SuccessDataResult<SupplementaryResourceCreateDto>(_mapper.Map<SupplementaryResourceCreateDto>(newSupplementaryResource), _stringLocalizer[Messages.AddSuccess]);
        }

        /// <summary>
        ///  Bu metod SupplementaryResource nesnesini id'ye göre getirme işlemlerini yapmaktadır.
        /// </summary>
        /// <param name="id">detayları getirilmek istenen SupplementaryResource nesnesinin Guid tipinde Id si</param>
        /// <returns>></returns>        
        public async Task<IResult> GetByIdAsync(Guid id)
        {
            var supplementaryResource = await _supplementaryResourceRepository.GetByIdAsync(id);

            if (supplementaryResource == null)
            {
                return new ErrorResult(_stringLocalizer[Messages.SupplementaryResourceNotFound]);
            }
            //var educationSubject = await _educationSubjectRepository.GetAllAsync(x => x.Id == supplementaryResource.EducationSubjectId);
            var supplementaryResourceDto = _mapper.Map<SupplementaryResourceDetailsDto>(supplementaryResource);

            return new SuccessDataResult<SupplementaryResourceDetailsDto>(supplementaryResourceDto, _stringLocalizer[Messages.FoundSuccess]);
        }

        /// <summary>
        ///  Bu metod SupplementaryResource nesnelerinin tamamını listeleme işlemlerini yapmaktadır.
        /// </summary>        
        /// <returns></returns>   
        public async Task<IResult> GetAllListsAsync()
        {
            var supplementaryResources = await _supplementaryResourceRepository.GetAllAsync();
            if (supplementaryResources.Count() <= 0)
            {
                return new ErrorResult(_stringLocalizer[Messages.ListedFail]);
            }

            return new SuccessDataResult<List<SupplementaryResourceListDto>>(_mapper.Map<List<SupplementaryResourceListDto>>(supplementaryResources), _stringLocalizer[Messages.ListedSuccess]);
        }
        public async Task<IResult> GetAllListsForResourceTypeStatusAsync(ResourcesTypeStatus status)
        {
            var supplementaryResources = await _supplementaryResourceRepository.GetAllAsync(s => s.ResourcesTypeStatus == status);
            if (supplementaryResources.Count() <= 0)
            {
                return new ErrorResult(_stringLocalizer[Messages.ListedFail]);
            }
            return new SuccessDataResult<List<SupplementaryResourceListDto>>(_mapper.Map<List<SupplementaryResourceListDto>>(supplementaryResources), _stringLocalizer[Messages.ListedSuccess]);
        }
        /// <summary>
        ///  Bu metot SupplemantaryResource nesnesi silme işlemini yapacaktır.
        /// </summary>
        /// <param name="id">silinmek  istenen SupplemantaryResource nesnesinin Guid tipinde Id si </param>
        public async Task<IResult> DeleteAsync(Guid id)
        {
            var deletingSupplementaryResources = await _supplementaryResourceRepository.GetByIdAsync(id);
            if (deletingSupplementaryResources == null)
            {
                return new ErrorResult
                    (_stringLocalizer[Messages.SupplementaryResourceNotFound]);
            }
            await _supplementaryResourceRepository.DeleteAsync(deletingSupplementaryResources);
            await _supplementaryResourceRepository.SaveChangesAsync();
            return new SuccessResult(_stringLocalizer[Messages.DeleteSuccess]);



        }
        public async Task<IResult> PermanentlyDocumentDeleteAsync(SupplementaryResourcesDeleteDto supplementaryResourcesDeleteDto)
        {
            var deletingSupplementaryResources = await _supplementaryResourceRepository.GetByIdAsync(supplementaryResourcesDeleteDto.Id);
            //Video db de fazla yer kaplamamsı için soft delete i kapatıldı. Eğer softdelete edilmek istenen kaynak video içeriyorsa video siliniyor
            if (deletingSupplementaryResources.ResourceType == ResourceType.Video)
                supplementaryResourcesDeleteDto.IsHardDelete = true;

            if (deletingSupplementaryResources == null)
            {
                return new ErrorResult
                    (_stringLocalizer[Messages.SupplementaryResourceNotFound]);
            }

            if (supplementaryResourcesDeleteDto.IsHardDelete)
            {
                var deletedfile = await FileManagementHelper.DeleteFileAsync(deletingSupplementaryResources.FileURL, FileCategory.SupplementaryResource, deletingSupplementaryResources.FileType);
                deletingSupplementaryResources.FileURL = "Deleted_File";
                deletingSupplementaryResources.FileType = deletedfile.FileType;
                deletingSupplementaryResources.ByteArrayFormat = deletedfile.FileByteArrayFormat;

            }
            else
            {
                FileManagementHelper.MoveToDeletedFiles(deletingSupplementaryResources.FileURL, FileCategory.SupplementaryResource, deletingSupplementaryResources.FileType);

            }

            await _supplementaryResourceRepository.DeleteAsync(deletingSupplementaryResources);
            await _supplementaryResourceRepository.SaveChangesAsync();
            return new SuccessResult(_stringLocalizer[Messages.DeleteSuccess]);
        }



        /// <summary>
        /// Bu metot SupplementaryResoutce nesnesinin güncelleme işlemini yapacaktır.
        /// </summary>
        /// <param name="update">güncellenmek istenen SupplementaryResource nesnesinin SupplementaryResourceUpdateDto tipinde entity'si</param>
        /// <returns></returns>
        public async Task<IResult> UpdateAsync(SupplementaryResourceUpdateDto update)
        {
            bool hasSupplementaryResource = await _supplementaryResourceRepository.AnyAsync(s => s.Name.ToLower() == update.Name.ToLower());
            var supplement = await _supplementaryResourceRepository.GetByIdAsync(update.Id);
            if (supplement.Name != update.Name)
                if (hasSupplementaryResource)
                    return new ErrorResult(_stringLocalizer[Messages.AddFailAlreadyExists]);
            if (update.ResourceType == (int)ResourceType.Video && update.FileURL != null)
            {
                var isDurationValid = await _videoFileService.CheckVideoDurationAsync(update.FileURL);
                if (isDurationValid)
                {

                    var newFormattedVideo = await _videoFileService.ConvertTo480pAsync(update.FileURL);
                    var isSizeValid = await _videoFileService.CheckVideoSizeAsync(newFormattedVideo);

                    if (isSizeValid)
                        update.FileURL = newFormattedVideo;
                    else
                        return new ErrorResult(_stringLocalizer[Messages.VideoSizeIsNotValid]);


                }
                else
                    return new ErrorResult(_stringLocalizer[Messages.VideoDurationIsNotValid]);
            }
            foreach (var educationId in update.Educations)
            {

                var existingResourceEducations = await _supplementaryResourceEducationSubjectRepository.GetAllAsync(es => es.SupplementaryResourceId == update.Id);

                var educationsToRemove = existingResourceEducations.Where(es => !update.Educations.Contains(es.EducationId)).ToList();

                // Güncellenmiş koleksiyonda bulunmayan eğitimleri kaldır
                foreach (var educationToRemove in educationsToRemove)
                {
                    await _supplementaryResourceEducationSubjectRepository.DeleteAsync(educationToRemove);
                }
                var education = await _educationRepository.GetAsync(e => e.Id == educationId);
                foreach (var subjectId in update.Subjects)
                {
                    var existingResourceSubjects = await _supplementaryResourceEducationSubjectRepository.GetAllAsync(es => es.SupplementaryResourceId == update.Id);
                    var subjectsToRemove = existingResourceSubjects.Where(es => !update.Subjects.Contains(es.SubjectId)).ToList();
                    // Güncellenmiş koleksiyonda bulunmayan konuları kaldır
                    foreach (var subjectToRemove in subjectsToRemove)
                    {
                        await _supplementaryResourceEducationSubjectRepository.DeleteAsync(subjectToRemove);
                    }
                    var subject = await _subjectRepository.GetAsync(s => s.Id == subjectId);
                    var existingEducationSubject = await _educationSubjectRepository.GetAsync(es => es.EducationId == educationId && es.SubjectId == subjectId);




                    // İlgili eğitim ve konu ilişkisi mevcut değilse, yeni ilişki oluşturulur.
                    var EducationSubject = await _educationSubjectRepository.GetAllAsync(t => t.EducationId == education.Id && t.SubjectId == subjectId);
                    if (EducationSubject.Any())
                    {
                        SupplementaryResourceEducationSubject newSupplementaryResourceEducationSubject = new()
                        {
                            SupplementaryResourceId = update.Id,
                            SubjectId = subjectId,
                            EducationId = educationId
                        };
                        await _supplementaryResourceEducationSubjectRepository.AddAsync(newSupplementaryResourceEducationSubject);
                    }
                }
            }
            var resourceTag = await _supplementaryResourceTagRepository.GetAllAsync(x => x.SupplementaryResourceId == update.Id);
            if (update.Tags is not null)
            {
                // Güncellenmiş koleksiyonda bulunmayan etiketleri kaldır
                var kaldırılacakEtiketler = resourceTag.Where(rt => !update.Tags.Contains(rt.TagId)).ToList();
                foreach (var etiketKaldır in kaldırılacakEtiketler)
                {
                    await _supplementaryResourceTagRepository.DeleteAsync(etiketKaldır);
                }

                foreach (Guid etiketId in update.Tags)
                {
                    var etiket = await _tagRepository.GetAsync(x => x.Id == etiketId);
                    if (etiket != null)
                    {
                        // Etiket zaten kaynak için mevcutsa, güncelleme gerekmez
                        var mevcutKaynakEtiketi = resourceTag.FirstOrDefault(rt => rt.TagId == etiket.Id);

                        if (mevcutKaynakEtiketi == null)
                        {
                            // Etiket kaynak için mevcut değilse, yeni bir etiket oluştur ve ekleyin
                            var yeniKaynakEtiketi = new SupplementaryResourceTag
                            {
                                SupplementaryResourceId = update.Id,
                                TagId = etiket.Id
                            };

                            await _supplementaryResourceTagRepository.AddAsync(yeniKaynakEtiketi);
                        }
                    }
                    else
                    {
                        return new ErrorDataResult<SupplementaryResourceUpdateDto>(update, _stringLocalizer[Messages.TagIsNotFound]);
                    }
                }
            }
            else
            {
                // Eğer update.Tags null ise, mevcut tüm etiketleri kaldır
                foreach (var etiketKaldır in resourceTag)
                {
                    await _supplementaryResourceTagRepository.DeleteAsync(etiketKaldır);
                }
            }
            var updatedSupplementaryResource = _mapper.Map(update, supplement);

            if (update.FileURL != null && update.FileURL.Length > 0)
            {

                var uploadedFile = await FileManagementHelper.UpdateFileAsync(supplement.FileURL, update.FileURL, FileCategory.SupplementaryResource, supplement.FileType, update.IsHardDelete, update.ResourceType);

                updatedSupplementaryResource.FileURL = uploadedFile.FileName;
                updatedSupplementaryResource.FileType = uploadedFile.FileType;
                updatedSupplementaryResource.ByteArrayFormat = uploadedFile.FileByteArrayFormat;

            }
            else if (update.Link != null)
            {
                //Video eklemek için ResourceType parametresi eklendi. Burada ihtiyaç olmadığı için null yazıldı.
                var uploadedFile = await FileManagementHelper.UpdateFileAsync(supplement.FileURL, update.FileURL, FileCategory.SupplementaryResource, supplement.FileType, update.IsHardDelete, null);

                if (Uri.TryCreate(update.Link, UriKind.Absolute, out Uri? uriResult) &&
        (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
                {
                    // Link geçerli bir URL formatına sahip
                    updatedSupplementaryResource.FileURL = update.Link;
                    updatedSupplementaryResource.FileType = update.FileType;
                }
                else
                {
                    // Link geçerli bir URL formatına sahip değil
                    return new ErrorResult(_stringLocalizer[Messages.LinkFormatFail]);
                }
            }

            else
            {
                updatedSupplementaryResource.FileType = update.FileType;
            }

            await _supplementaryResourceRepository.UpdateAsync(updatedSupplementaryResource);
            await _supplementaryResourceRepository.SaveChangesAsync();

            var updatedSupplementDto = _mapper.Map<SupplementaryResourceUpdateDto>(update);


            return new SuccessResult(_stringLocalizer[Messages.UpdateSuccess]);
        }

        public async Task<IResult> GetDeletedResources()
        {

            var deletedSupplementaryResources = await _supplementaryResourceRepository.GetAllDeletedAsync(x => x.FileURL != "Deleted_File" && x.SupplementaryResourceEducationSubjects.Any());
            if (!deletedSupplementaryResources.Any())
                return new ErrorResult(_stringLocalizer[Messages.ListedFail]);

            string targetPath = Path.Combine(_webHostEnvironment.WebRootPath, "documents", "SupplementaryResources", "DeletedSupplementaryResources");
            string[] filePaths = Directory.GetFiles(targetPath);

            var listResult = new List<object>();

            FileContentResult contentResult;
            string fileName = string.Empty;
            string mimeType = string.Empty;
            byte[] fileContent;

            foreach (var sr in deletedSupplementaryResources)
            {

                if (sr.ByteArrayFormat is null)
                {
                    var path = filePaths.FirstOrDefault(fp => Path.GetFileName(fp) == sr.FileURL);

                    if (path is not null)
                    {
                        fileName = Path.GetFileName(path);
                        mimeType = FileManagementHelper.GetMimeType(path);
                        fileContent = File.ReadAllBytes(path);

                        contentResult = new(fileContent, mimeType) { FileDownloadName = fileName };

                        listResult.Add(new { Id = sr.Id, Name = sr.Name, FileContent = contentResult, Link = "" });
                    }
                    else
                        listResult.Add(new { Id = sr.Id, Name = sr.Name, FileContent = new {}, Link = sr.FileURL });
                }
                else
                {
                    fileName = sr.FileURL;
                    mimeType = FileManagementHelper.GetMimeType(fileName);
                    fileContent = sr.ByteArrayFormat;

                    contentResult = new(fileContent, mimeType) { FileDownloadName = fileName };

                    listResult.Add(new { Id = sr.Id, Name = sr.Name, FileContent = contentResult, Link = "" });
                }
            }
            return new SuccessDataResult<List<object>>(listResult, _stringLocalizer[Messages.ListedSuccess]);
        }



        [HttpGet]
        [Route("[action]")]
        public FileContentResult DownloadDocumentSupplementaryResource(string filePath, Guid supplementaryResourceId)
        {
            string dosyaYolu = "";
            string mimeType = "";
            byte[] fileBytes;
            string fileName = filePath;
            string extension = Path.GetExtension(fileName);
            //var supplementaryResource = _supplementaryResourceRepository.GetByIdAsync(supplementaryResourceId).Result;
            if (extension == ".pdf" || extension == ".zip")
            {
                dosyaYolu = Path.Combine(_webHostEnvironment.WebRootPath, "documents", "SupplementaryResources", filePath);

                if (!System.IO.File.Exists(dosyaYolu))
                {
                    return null;
                }
                fileBytes = System.IO.File.ReadAllBytes(dosyaYolu);
                mimeType = FileManagementHelper.GetMimeType(fileName);

            }
            else if (extension == ".mp4")
            {
                dosyaYolu = Path.Combine(_webHostEnvironment.WebRootPath, "documents", "SupplementaryResources", "Videos", filePath);

                if (!System.IO.File.Exists(dosyaYolu))
                {
                    return null;
                }
                fileBytes = System.IO.File.ReadAllBytes(dosyaYolu);
                mimeType = FileManagementHelper.GetMimeType(fileName);
            }
            else
            {
                var supplementaryResource = _supplementaryResourceRepository.GetByIdAsync(supplementaryResourceId);
                fileBytes = supplementaryResource.Result.ByteArrayFormat;
                mimeType = FileManagementHelper.GetMimeType(fileName);
            }

            var fileContentResult = new FileContentResult(fileBytes, mimeType)
            {
                FileDownloadName = fileName
            };
            return fileContentResult;
        }

        /// <summary>
        /// Bu metod seçilen tag'e göre yardımcı kaynakların listelenmesi işlevini görür. 
        /// </summary>
        /// <param name="tagId"></param>
        /// <returns></returns>
        public async Task<IResult> GetAllListsByTagIdAsync(Guid tagId)
        {
            var supplementaryResources = await _supplementaryResourceRepository.GetAllAsync(x => x.SupplementaryResourceTags.Any(p => p.TagId == tagId));
            if (supplementaryResources.Count() <= 0)
            {
                return new ErrorResult(_stringLocalizer[Messages.ListedFail]);
            }
            return new SuccessDataResult<List<SupplementaryResourceListDto>>(_mapper.Map<List<SupplementaryResourceListDto>>(supplementaryResources), _stringLocalizer[Messages.ListedSuccess]);
        }
        public async Task<IResult> GetAllListsBySubjectIdAsync(Guid subjectId)
        {
            var supplementaryResources = await _supplementaryResourceRepository.GetAllAsync(x => x.SupplementaryResourceEducationSubjects.Any(p => p.SubjectId == subjectId));
            if (supplementaryResources.Count() <= 0)
            {
                return new ErrorResult(_stringLocalizer[Messages.ListedFail]);
            }
            return new SuccessDataResult<List<SupplementaryResourceListDto>>(_mapper.Map<List<SupplementaryResourceListDto>>(supplementaryResources), _stringLocalizer[Messages.ListedSuccess]);
        }

        public async Task<IResult> GetAllListsByEducationtIdAsync(Guid educationId)
        {
            var supplementaryResources = await _supplementaryResourceRepository.GetAllAsync(x => x.SupplementaryResourceEducationSubjects.Any(p => p.EducationId == educationId));
            if (supplementaryResources.Count() <= 0)
            {
                return new ErrorResult(_stringLocalizer[Messages.ListedFail]);
            }
            return new SuccessDataResult<List<SupplementaryResourceListDto>>(_mapper.Map<List<SupplementaryResourceListDto>>(supplementaryResources), _stringLocalizer[Messages.ListedSuccess]);
        }


        public async Task<IResult> GetAllListByStudentId(Guid studentId)
        {
            string userId = studentId.ToString();
            var student = await _studentRepository.GetAsync(x => x.IdentityId == userId.ToString());
            var hasStudent = await _classroomStudentRepository.GetAllAsync(x => x.StudentId == student.Id);

            var supplementaryResourceList = hasStudent.SelectMany(x =>
             x.Classroom.Education.SupplementaryResourceEducationSubjects.Where(x => x.Status != Status.Deleted).Select(p => p.SupplementaryResources).Where(x => x.Status != Status.Deleted && x.ResourcesTypeStatus == ResourcesTypeStatus.Approved));

            return new SuccessDataResult<List<SupplementaryResourceListDto>>(_mapper.Map<List<SupplementaryResourceListDto>>(supplementaryResourceList), _stringLocalizer[Messages.ListedSuccess]);
        }

        public async Task<IResult> GetDocumentsOrVideosByEducationId(Guid educationId, ResourceType resourceType)
        {
            var supplemantaryResourcesEducationSubject = await _supplementaryResourceEducationSubjectRepository
                .GetAllAsync(x => x.EducationId == educationId && x.Status == Status.Active);

            var uniqueSupplementaryResources = supplemantaryResourcesEducationSubject
                .Where(item => item.SupplementaryResources.ResourceType == resourceType &&
                               item.SupplementaryResources.Status == Status.Active)
                .Select(item => item.SupplementaryResources)
                .Distinct()
                .ToList();

            if (uniqueSupplementaryResources.Count <= 0)
            {
                return new ErrorResult(_stringLocalizer[Messages.ListedFail]);
            }

            var dto=new SuccessDataResult<List<SupplementaryResourceListDto>>(
                _mapper.Map<List<SupplementaryResourceListDto>>(uniqueSupplementaryResources),
                _stringLocalizer[Messages.ListedSuccess]);

            foreach ( var item in dto.Data)
            {
                foreach (var item2 in item.SupplementaryResourceEducationSubjects)
                {
                    item2.Order = _educationSubjectRepository.GetAllAsync().Result.FirstOrDefault(x => x.EducationId == item2.EducationId && x.SubjectId == item2.SubjectId).Order;
                }
            }
            return dto;
        }


        public async Task<FileContentResult> DownloadMp4SupplementaryResource(string filePath, Guid supplementaryResourceId, Quality quality)
        {
            string dosyaYolu = "";
            string mimeType = "";
            byte[] fileBytes;
            string fileName = filePath;
            string extension = Path.GetExtension(fileName);

            dosyaYolu = Path.Combine(_webHostEnvironment.WebRootPath, "documents", "SupplementaryResources", "Videos", filePath);

            if (!System.IO.File.Exists(dosyaYolu))
            {
                return null;
            }

            if (quality == Quality.Low)
            {
                fileBytes = await _videoFileService.ConvertTo360pAsync(System.IO.File.ReadAllBytes(dosyaYolu));
            }
            else
            {
                fileBytes = System.IO.File.ReadAllBytes(dosyaYolu);
            }

            mimeType = FileManagementHelper.GetMimeType(fileName);


            var fileContentResult = new FileContentResult(fileBytes, mimeType)
            {
                FileDownloadName = fileName
            };


            return fileContentResult;
        }

        public async Task<IResult> UpdateStatusAsync(Guid supplementaryResourceId, ResourcesTypeStatus status)
        {
            var supplement = await _supplementaryResourceRepository.GetByIdAsync(supplementaryResourceId);

            supplement.ResourcesTypeStatus = status;
            await _supplementaryResourceRepository.UpdateAsync(supplement);
            await _supplementaryResourceRepository.SaveChangesAsync();

            return new SuccessResult(_stringLocalizer[Messages.UpdateSuccess]);
        }

        public async Task<IResult> GetDeletedResourceById(Guid id)
        {
            var deletedSupplementaryResources = await _supplementaryResourceRepository.GetAllDeletedAsync(x => x.FileURL != "Deleted_File" && x.Id == id);

            if (!deletedSupplementaryResources.Any())
                return new ErrorResult(_stringLocalizer[Messages.SupplementaryResourceNotFound]);

            var supplementaryResourceDto = _mapper.Map<SupplementaryResourceDeletedDetailsDto>(deletedSupplementaryResources.First());

            return new SuccessDataResult<SupplementaryResourceDeletedDetailsDto>(supplementaryResourceDto, _stringLocalizer[Messages.FoundSuccess]);
        }

        public async Task<IResult> PermanentlyDeleteDeletedResource(Guid id)
        {
            var deletedResources = await _supplementaryResourceRepository.GetAllDeletedAsync(x => x.Id == id);

            if (!deletedResources.Any())
                return new ErrorResult(_stringLocalizer[Messages.SupplementaryResourceNotFound]);

            var deletedResource = deletedResources.FirstOrDefault();

            var deletedfile = await FileManagementHelper.PermanentlyDeleteSoftDeletedFile(deletedResource.FileURL, deletedResource.FileType);
            deletedResource.FileURL = "Deleted_File";
            deletedResource.FileType = deletedfile.FileType;
            deletedResource.ByteArrayFormat = deletedfile.FileByteArrayFormat;

            await _supplementaryResourceRepository.UpdateAsync(deletedResource);
            await _supplementaryResourceRepository.DeleteAsync(deletedResource);
            int effectedRows = await _supplementaryResourceRepository.SaveChangesAsync();

            return effectedRows > 0 ? new SuccessResult(_stringLocalizer[Messages.DeleteSuccess]) : new ErrorResult(_stringLocalizer[Messages.DeleteFail]);
        }

        public async Task<IResult> Recover(SupplementaryResourceRecoverDto recoverDto)
        {
            var softDeletedResources = await _supplementaryResourceRepository.GetAllDeletedAsync(x => x.FileURL != "Deleted_File" && x.Id == recoverDto.Id);
            if (!softDeletedResources.Any())
                return new ErrorResult(_stringLocalizer[Messages.FileCouldNotBeRecovered]);

            bool checkIfExists = await _supplementaryResourceRepository.AnyAsync(s => s.Name.ToLower() == recoverDto.Name.ToLower());
            if (checkIfExists)
                return new ErrorResult(_stringLocalizer[Messages.AddFailAlreadyExists]);

            var targetResource = softDeletedResources.First();
            //targetResource.Status = Status.Active;

            var recoveredResource = await _supplementaryResourceRepository.UpdateAsync(targetResource);
            int effectedRows = await _supplementaryResourceRepository.SaveChangesAsync();

            if (effectedRows == 0)
                return new ErrorResult(_stringLocalizer[Messages.FileCouldNotBeRecovered]);

            if (recoveredResource.ByteArrayFormat is null && recoveredResource.FileURL is not null && recoveredResource.ResourceType == ResourceType.Document)
            {
                bool checkIfFileRecovered = FileManagementHelper.Recover(recoveredResource.FileURL);

                if (!checkIfFileRecovered)
                {
                    recoveredResource.Status = Status.Deleted;

                    await _supplementaryResourceRepository.UpdateAsync(recoveredResource);
                    await _supplementaryResourceRepository.SaveChangesAsync();

                    return new ErrorResult(_stringLocalizer[Messages.FileCouldNotBeRecovered]);
                }
            }

            foreach (var educationId in recoverDto.Educations)
            {
                var existingResourceEducations = await _supplementaryResourceEducationSubjectRepository.GetAllAsync(es => es.SupplementaryResourceId == recoverDto.Id);

                var educationsToRemove = existingResourceEducations.Where(es => !recoverDto.Educations.Contains(es.EducationId)).ToList();

                // Güncellenmiş koleksiyonda bulunmayan eğitimleri kaldır
                foreach (var educationToRemove in educationsToRemove)
                {
                    await _supplementaryResourceEducationSubjectRepository.DeleteAsync(educationToRemove);
                }
                var education = await _educationRepository.GetAsync(e => e.Id == educationId);
                foreach (var subjectId in recoverDto.Subjects)
                {
                    var existingResourceSubjects = await _supplementaryResourceEducationSubjectRepository.GetAllAsync(es => es.SupplementaryResourceId == recoverDto.Id);
                    var subjectsToRemove = existingResourceSubjects.Where(es => !recoverDto.Subjects.Contains(es.SubjectId)).ToList();

                    foreach (var subjectToRemove in subjectsToRemove)
                    {
                        await _supplementaryResourceEducationSubjectRepository.DeleteAsync(subjectToRemove);
                    }
                    var subject = await _subjectRepository.GetAsync(s => s.Id == subjectId);
                    var existingEducationSubject = await _educationSubjectRepository.GetAsync(es => es.EducationId == educationId && es.SubjectId == subjectId);

                    var EducationSubject = await _educationSubjectRepository.GetAllAsync(t => t.EducationId == education.Id && t.SubjectId == subjectId);
                    if (EducationSubject.Any())
                    {
                        SupplementaryResourceEducationSubject newSupplementaryResourceEducationSubject = new()
                        {
                            SupplementaryResourceId = recoverDto.Id,
                            SubjectId = subjectId,
                            EducationId = educationId
                        };
                        await _supplementaryResourceEducationSubjectRepository.AddAsync(newSupplementaryResourceEducationSubject);
                    }
                }
            }
            var resourceTag = await _supplementaryResourceTagRepository.GetAllAsync(x => x.SupplementaryResourceId == recoverDto.Id);
            if (recoverDto.Tags is not null)
            {
                // Güncellenmiş koleksiyonda bulunmayan etiketleri kaldır
                var kaldırılacakEtiketler = resourceTag.Where(rt => !recoverDto.Tags.Contains(rt.TagId)).ToList();
                foreach (var etiketKaldır in kaldırılacakEtiketler)
                {
                    await _supplementaryResourceTagRepository.DeleteAsync(etiketKaldır);
                }

                foreach (Guid etiketId in recoverDto.Tags)
                {
                    var etiket = await _tagRepository.GetAsync(x => x.Id == etiketId);
                    if (etiket != null)
                    {
                        // Etiket zaten kaynak için mevcutsa, güncelleme gerekmez
                        var mevcutKaynakEtiketi = resourceTag.FirstOrDefault(rt => rt.TagId == etiket.Id);

                        if (mevcutKaynakEtiketi == null)
                        {
                            // Etiket kaynak için mevcut değilse, yeni bir etiket oluştur ve ekleyin
                            var yeniKaynakEtiketi = new SupplementaryResourceTag
                            {
                                SupplementaryResourceId = recoverDto.Id,
                                TagId = etiket.Id
                            };

                            await _supplementaryResourceTagRepository.AddAsync(yeniKaynakEtiketi);
                        }
                    }
                    else
                    {
                        return new ErrorDataResult<SupplementaryResourceRecoverDto>(recoverDto, _stringLocalizer[Messages.TagIsNotFound]);
                    }
                }
            }
            else
            {
                // Eğer update.Tags null ise, mevcut tüm etiketleri kaldır
                foreach (var etiketKaldır in resourceTag)
                {
                    await _supplementaryResourceTagRepository.DeleteAsync(etiketKaldır);
                }
            }
            var updatedSupplementaryResource = _mapper.Map(recoverDto, recoveredResource);

            await _supplementaryResourceRepository.UpdateAsync(updatedSupplementaryResource);
            await _supplementaryResourceRepository.SaveChangesAsync();

            return new SuccessResult(_stringLocalizer[Messages.UpdateSuccess]);
        }


        public async Task<IResult> GiveFeedback(SupplementaryResourceFeedBackDto supplementaryResourceFeedBackDto)
        {
            var supplementaryResource = await _supplementaryResourceRepository.GetByIdAsync(supplementaryResourceFeedBackDto.Id);

        if (supplementaryResource == null)
            {
                return new ErrorResult(_stringLocalizer[Messages.SupplementaryResourceNotFound]);
            }

            var updatedSupplementaryResource = _mapper.Map(supplementaryResourceFeedBackDto, supplementaryResource);

            await _supplementaryResourceRepository.UpdateAsync(updatedSupplementaryResource);

            if (updatedSupplementaryResource == null)
            {
                return new ErrorResult(_stringLocalizer[Messages.UpdateFail]);
            }

            await _supplementaryResourceRepository.SaveChangesAsync();
            return new SuccessDataResult<SupplementaryResourceFeedBackDto>(_mapper.Map<SupplementaryResourceFeedBackDto>(updatedSupplementaryResource), _stringLocalizer[Messages.FeedbackSucceeded]);
        }

        /// <summary>
        ///  Bu metod SupplementaryResource nesnelerini giriş yapan trainer a göre listeleme işlemlerini yapmaktadır.
        /// </summary>        
        /// <param name="id">giriş yapan trainer rolündeki kullanıcının identityId si</param>
        /// <returns></returns>   

        public async Task<IResult> ListsAllSupplementaryResourceByTrainersEducationsAsync(string id)
        {

            var trainer = await _trainerRepo.GetByIdentityId(id);
            var classRoomTrainers = await _classroomTrainersRepository.GetAllAsync(t => t.TrainerId == trainer.Id);
            var classRoomsIds = classRoomTrainers.Select(x => x.ClassroomId).ToList();

            List<Guid> educationIds = new List<Guid>();
            foreach (var item in classRoomsIds)
            {
                var classRooms = await _classroomRepository.GetByIdAsync(item);
                if (classRooms != null)
                {
                    educationIds.Add(classRooms.EducationId);
                }
            }
            var supplementaryResourcesEducationSubjects = await _supplementaryResourceEducationSubjectRepository.GetAllAsync();
            var resources = supplementaryResourcesEducationSubjects.Where(sres => educationIds.Contains(sres.EducationId))
                .Select(sres => sres.SupplementaryResources)
                .Distinct()
                .ToList();

            if (resources.Count() <= 0)
            {
                return new ErrorResult(_stringLocalizer[Messages.ListedFail]);
            }
            return new SuccessDataResult<List<SupplementaryResourceListDto>>(_mapper.Map<List<SupplementaryResourceListDto>>(resources), _stringLocalizer[Messages.ListedSuccess]);
        }

    }
}