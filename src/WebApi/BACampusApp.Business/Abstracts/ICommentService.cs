namespace BACampusApp.Business.Abstracts
{
    public interface ICommentService
    {
        /// <summary>
        ///  Bu metot Comment nesnesi oluşturma işlemini yapmaktadır
        /// </summary>
        /// <param name="newComment"></param>
        ///  <returns></returns>
        Task<IResult> AddAsync(CommentCreateDto newComment);


        /// <summary>
        ///  Bu metot Comment nesnesi silme işlemini yapacaktır.
        /// </summary>
        /// <param name="id">silinmek  istenen comment nesnesinin Guid tipinde Id si </param>
        ///
        Task<IResult> DeleteAsync(Guid id);
        /// <summary>
        /// Bu metot Comment nesnesinin güncelleme işlemini yapacaktır.
        /// </summary>
        /// <param name="entity">güncellenmek istenen student nesnesinin CommentUpdateDto tipinde entity'si</param>
        /// <returns></returns>
        Task<IResult> UpdateAsync(CommentUpdateDto entity);

        /// <summary>
        ///  CommentDto ve Comment nesnelerini listeleme işlemlerini yapmaktadır.
        /// </summary>
        /// <param name="id">detayları getirilmek istenen Comment nesnesinin Guid tipinde Id si</param>
        /// <returns></returns>
        Task<IResult> GetByIdAsync(Guid id);

        /// <summary>
        ///  Bu metot Eğitmenin,tüm yorumları listelemesini sağlamaktadır.
        /// </summary>        
        /// <returns></returns>       
        Task<IResult> GetAllAsync();

        /// <summary>
        ///  Bu metot Yöneticinin,tüm yorumları listelemesini sağlamaktadır.
        /// </summary>        
        /// <returns></returns>         
        Task<IResult> DeletedListAsync();
    }
}
