using BACampusApp.Core.Utilities.Results;
using System.Diagnostics;
using Microsoft.Extensions.Localization;

namespace BACampusApp.Business.Concretes
{
    public class CommentManager : ICommentService
    {
        private readonly IStudentRepository _studentRepo;
        private readonly IHomeWorkRepository _homeWorkRepo;
        private readonly ISupplementaryResourceRepository _supplementaryResourceRepositoryRepo;
        private readonly ICommentRepository _commentRepo;
        private readonly IMapper _mapper;
        private readonly IAdminRepository _adminRepo;
        private readonly ITrainerRepository _trainerRepo;
        private readonly IStringLocalizer<Resource> _stringLocalizer;

        public CommentManager(IStudentRepository studentRepo, IHomeWorkRepository homeWorkRepo, ISupplementaryResourceRepository supplementaryResourceRepositoryRepo, ICommentRepository commentRepo, IMapper mapper, IHttpContextAccessor httpContextAccessor, IAdminRepository adminRepo, ITrainerRepository trainerRepo, IStringLocalizer<Resource> stringLocalizer)
        {
            _studentRepo = studentRepo;
            _homeWorkRepo = homeWorkRepo;
            _supplementaryResourceRepositoryRepo = supplementaryResourceRepositoryRepo;
            _commentRepo = commentRepo;
            _mapper = mapper;
            _adminRepo = adminRepo;
            _trainerRepo = trainerRepo;
            _stringLocalizer = stringLocalizer;
        }
        /// <summary>
        ///  Bu metot Comment nesnesi oluşturma işlemini yapmaktadır
        /// </summary>
        /// <param name="newComment"></param>
        ///  <returns></returns>
        public async Task<IResult> AddAsync(CommentCreateDto newComment)
        {
            {
                var hasAdminUser = await _adminRepo.GetByIdAsync(newComment.UserId);
                var hasStudentUser = await _studentRepo.GetByIdAsync(newComment.UserId);
                var hasTranierUser = await _trainerRepo.GetByIdAsync(newComment.UserId);


                //dto dan gelen ıdf kulşlanılacağı için save changes in üst sarına eklenemsi daha doğru
                var creatingComment = await _commentRepo.AddAsync(_mapper.Map<Comment>(newComment));
                if (hasAdminUser == null && hasStudentUser == null && hasTranierUser == null)
                {
                    return new ErrorResult(_stringLocalizer[Messages.UserNotFound]);
                }
                else
                {
                    switch (newComment.ItemType)
                    {

                        case Entities.Enums.ItemType.Student:
                            bool hasStudent = await _studentRepo.AnyAsync(x => x.Id == newComment.ItemId);
                            if (!hasStudent)
                            {
                                return new ErrorResult(_stringLocalizer[Messages.StudentNotFound]);
                            }
                            break;
                        case Entities.Enums.ItemType.HomeWork:
                            bool hasHomeWork = await _homeWorkRepo.AnyAsync(x => x.Id == newComment.ItemId);
                            if (!hasHomeWork)
                            {
                                return new ErrorResult(_stringLocalizer[Messages.HomeWorkNotFound]);
                            }
                            break;
                        case Entities.Enums.ItemType.SupplementaryResource:
                            bool hasSupplementaryResourceRepository = await _supplementaryResourceRepositoryRepo.AnyAsync(x => x.Id == newComment.ItemId);
                            if (!hasSupplementaryResourceRepository)
                            {
                                return new ErrorResult(_stringLocalizer[Messages.SupplementaryResourceNotFound]);
                            }
                            break;
                        default:
                            return new ErrorResult(_stringLocalizer[Messages.ItemTypeIsNotCorrect]);

                    }
                }

                await _commentRepo.SaveChangesAsync();
                return new SuccessDataResult<CommentDto>(_mapper.Map<CommentDto>(creatingComment), _stringLocalizer[Messages.CommentAddSuccess]);



            }
        }
        /// <summary>
        /// Belirtilen Id'ye sahip Comment nesnesini silinmiş olarak işaretler.
        /// Eğer belirtilen Id'ye sahip bir Comment nesnesi bulunamazsa, hata döner.
        /// </summary>
        /// <param name="id">Silinmiş olarak işaretlenecek Comment nesnesinin Id'si</param>
        /// <returns></returns>
        public async Task<IResult> DeleteAsync(Guid id)
        {
            var deletingComment = await _commentRepo.GetByIdAsync(id);

            if (deletingComment == null)
            {
                return new ErrorResult(_stringLocalizer[Messages.CommentNotFound]);
            }

            await _commentRepo.DeleteAsync(deletingComment);
            await _commentRepo.SaveChangesAsync();
            return new SuccessResult(_stringLocalizer[Messages.DeleteSuccess]);
        }

        /// <summary>
        /// GetByIdAsync() metodu database de kayıtlı id si verilen Comment'i çeker ve CommentDto'ya Map'leyerek CommentDto nesnesine çevirir. En son olarak bu nesneyi ve işlemin durumuna göre verilmek istenen mesajı birlikte döner.
        /// </summary>
        /// /// <param name="id">detayları getirilmek istenen comment nesnesinin Guid tipinde Id si </param>
        /// <returns>SuccessDataResult<CommentDto>(CommentDto, Messages.FoundSuccess)</returns> 
        public async Task<Core.Utilities.Results.IResult> GetByIdAsync(Guid id)
        {
            var comment = await _commentRepo.GetByIdAsync(id);
            if (comment == null)
            {
                return new ErrorResult(_stringLocalizer[Messages.CommentNotFound]);
            }
            return new SuccessDataResult<CommentDto>(_mapper.Map<CommentDto>(comment), _stringLocalizer[Messages.FoundSuccess]);
        }

        /// <summary>
        /// Bu metot Comment nesnesinin güncelleme işlemini yapacaktır.
        /// </summary>
        /// <param name="entity">güncellenmek istenen comment nesnesinin CommentUpdateDto tipinde entity'si</param>
        /// <returns>ErrorDataResult<CommentUpdateDto>, SuccessDataResult<CommentUpdateDto></returns>
        public async Task<IResult> UpdateAsync(CommentUpdateDto entity)
        {
            var comment = await _commentRepo.GetByIdAsync(entity.Id);
            if (comment == null)
            {
                return new ErrorResult(_stringLocalizer[Messages.CommentNotFound]);
            }



            switch (entity.ItemType)
            {

                case Entities.Enums.ItemType.Student:
                    bool hasStudent = await _studentRepo.AnyAsync(x => x.Id == entity.ItemId);
                    if (!hasStudent)
                    {
                        return new ErrorResult(_stringLocalizer[Messages.StudentNotFound]);
                    }
                    break;
                case Entities.Enums.ItemType.HomeWork:
                    bool hasHomeWork = await _homeWorkRepo.AnyAsync(x => x.Id == entity.ItemId);
                    if (!hasHomeWork)
                    {
                        return new ErrorResult(_stringLocalizer[Messages.HomeWorkNotFound]);
                    }
                    break;
                case Entities.Enums.ItemType.SupplementaryResource:
                    bool hasSupplementaryResourceRepository = await _supplementaryResourceRepositoryRepo.AnyAsync(x => x.Id == entity.ItemId);
                    if (!hasSupplementaryResourceRepository)
                    {
                        return new ErrorResult(_stringLocalizer[Messages.SupplementaryResourceNotFound]);
                    }
                    break;
                default:
                    return new ErrorResult(_stringLocalizer[Messages.ItemTypeIsNotCorrect]);

            }

            var commentDto = _mapper.Map(entity, comment);
            await _commentRepo.UpdateAsync(commentDto);
            await _commentRepo.SaveChangesAsync();

            return new SuccessResult(_stringLocalizer[Messages.UpdateSuccess]);

        }
        /// <summary>
        ///  Bu metot veritabanındaki tüm yorumları çeker ve bu yorum listesini CommentListDto ile eşleyip çıktısını verir.
        /// </summary>
        /// <returns>SuccessDataResult<CommentListDto>, ErrorDataResult<CommentListDto></returns>
        public async Task<Core.Utilities.Results.IResult> GetAllAsync()
        {
            var comments = await _commentRepo.GetAllAsync();
            if (comments == null)
            {
                return new ErrorResult(_stringLocalizer[Messages.ListedFail]);
            }
            return new SuccessDataResult<List<CommentListDto>>(_mapper.Map<List<CommentListDto>>(comments), _stringLocalizer[Messages.ListedSuccess]);
        }

        public async Task<IResult> DeletedListAsync()
        {
            var comment = await _commentRepo.GetAllDeletedAsync();
            if (comment.Count() <= 0)
            {
                return new ErrorResult(_stringLocalizer[Messages.ListedFail]);
            }
            return new SuccessDataResult<List<CommentDeletedListDto>>(_mapper.Map<List<CommentDeletedListDto>>(comment), _stringLocalizer[Messages.ListedSuccess]);
        }
    }
}