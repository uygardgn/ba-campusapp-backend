namespace BACampusApp.Business.Abstracts
{
    public interface ISupplementaryResourceTagService
    {
        /// <summary>
        /// Bu metot SupplementaryResourceTag nesnesinin oluşturulması için kullanılmakatadır.
        /// </summary>
        /// <param name="supplementaryResourceTagCreateDto"></param>
        /// <returns></returns>
        Task<IResult> CreateAsync(SupplementaryResourceTagCreateDto supplementaryResourceTagCreateDto);

        /// <summary>
        /// Bu metot verilen id'ye uygun olarak SupplementaryResourceTag nesnesinin silinmesi için kullanılmaktadır.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResult> DeleteAsync(Guid id);

        /// <summary>
        /// Bu metot verilen nesne içerisindeki id'ye uygun olarak SupplementaryResourceTag nesnesinin güncellenmesi için kullanılmaktadır.
        /// </summary>
        /// <param name="supplementaryResourceTagUpdateDto"></param>
        /// <returns></returns>
        Task<IResult> UpdateAsync(SupplementaryResourceTagUpdateDto supplementaryResourceTagUpdateDto);

        /// <summary>
        /// Bu metot tüm SupplementaryResourceTag nesnelerinin listelenmesi için kullanılmaktadır.
        /// </summary>
        /// <returns></returns>
        Task<IResult> GetAllAsync();

        /// <summary>
        /// Bu metot verilen id'ye uygun SupplementaryResourceTag nesnesinin gösterilmesi için kullanılmaktadır.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResult> GetByIdAsync(Guid id);

        /// <summary>
        ///  Bu metot Yöneticinin,silinen tüm SupplemantaryResource nesnesi listelemesini sağlamaktadır.
        /// </summary>        
        /// <returns></returns>         
        Task<IResult> DeletedListAsync();
    }
}
