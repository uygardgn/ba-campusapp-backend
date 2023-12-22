namespace BACampusApp.Business.Abstracts
{
    public interface ISubjectService
    {
        /// <summary>
        ///  Bu metot Subject nesnesi oluşturma işlemini işlemlerini yapmaktadır.
        /// </summary>
        /// <param name="newSubject"></param>
        ///  <returns>></returns>
        Task<IResult> AddAsync(SubjectCreateDto newSubject);

        /// <summary>
        ///  Bu metot Subject nesnesi silme işlemini yapacaktır.
        /// </summary>
        /// <param name="id">silinmek  istenen subject nesnesinin Guid tipinde Id si </param>
        Task<IResult> DeleteAsync(Guid id);

        /// <summary>
        ///  Bu metot Yöneticinin,tüm konularını listelemesini sağlamaktadır.
        /// </summary>        
        /// <returns></returns>       
        Task<IResult> GetListAsync();

        /// <summary>
        ///  SubjectDto ve Subject nesnelerini listeleme işlemlerini yapmaktadır.
        /// </summary>
        /// <param name="id">detayları getirilmek istenen Subject nesnesinin Guid tipinde Id si</param>
        /// <returns></returns>
        Task<IResult> GetByIdAsync(Guid id);

        /// <summary>
        /// Bu metot Subject nesnesinin güncelleme işlemini yapacaktır.
        /// </summary>
        /// <param name="entity">güncellenmek istenen subject nesnesinin SubjectUpdateDto tipinde entity'si</param>
        /// <returns></returns>
        Task<IResult> UpdateAsync(SubjectUpdateDto entity);

        /// <summary>
        ///  Bu metot Yöneticinin,silinen tüm Subject nesnelerini listelemesini sağlamaktadır.
        /// </summary>        
        /// <returns></returns>         
        Task<IResult> DeletedListAsync();

    }
}
