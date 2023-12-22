namespace BACampusApp.Business.Abstracts
{
    public interface ITagService
    {
        /// <summary>
        ///  Bu metot Tag nesnesi oluşturma işlemini işlemlerini yapmaktadır.
        /// </summary>
        /// <param name="newTag"></param>
        ///  <returns>></returns>
        Task<IResult> AddAsync(TagCreateDto newTag);

        /// <summary>
        ///  Bu metot Tag nesnesi silme işlemini yapacaktır.
        /// </summary>
        /// <param name="id">silinmek  istenen tag nesnesinin Guid tipinde Id si </param>
        Task<IResult> DeleteAsync(Guid id);

        /// <summary>
        /// Bu metot Tag nesnesinin güncelleme işlemini yapacaktır.
        /// </summary>
        /// <param name="entity">güncellenmek istenen tag nesnesinin TagUpdateDto tipinde entity'si</param>
        /// <returns></returns>
        Task<IResult> UpdateAsync(TagUpdateDto entity);

        /// <summary>
        ///  Tag nesnesinin detaylarını getirme işlemini yapmaktadır.
        /// </summary>
        /// <param name="id">detayları getirilmek istenen Tag nesnesinin Guid tipinde Id si</param>
        /// <returns></returns>
        Task<IResult> GetByIdAsync(Guid id);

        /// <summary>
        ///  Bu metot Yöneticinin,tüm tagleri listelemesini sağlamaktadır.
        /// </summary>        
        /// <returns></returns>
        Task<IResult> GetListAsync();

        /// <summary>
        ///  Bu metot Yöneticinin,tüm tagleri listelemesini sağlamaktadır.
        /// </summary>        
        /// <returns></returns>         
        Task<IResult> DeletedListAsync();
    }
}