namespace BACampusApp.Business.Abstracts
{
    public interface IBranchService
    {
        /// <summary>
        /// Bu metot Branch nesnesi oluşturma işlemini yapmaktadır.
        /// </summary>
        /// <param name="branchCreateDto"></param>
        /// <returns></returns>
        Task<IResult> CreateAsync(BranchCreateDto branchCreateDto);
        /// <summary>
        /// Bu metot Branch nesnesi silme işlemini yapmaktadır.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResult> DeleteAsync(Guid id);
        /// <summary>
        /// Bu metot branch nesnesi güncelleme işlemini yapmaktadır.
        /// </summary>
        /// <param name="branchUpdateDto"></param>
        /// <returns></returns>
        Task<IResult> UpdateAsync(BranchUpdateDto branchUpdateDto);
        /// <summary>
        /// Bu metot tüm branch listesini getirme işlemi yapmaktadır.
        /// </summary>
        /// <returns></returns>
        Task<IResult> GetAllAsync();
        /// <summary>
        /// Bu metot girilen id ye göre branch nesnesi getirme işlemi yapmaktadır.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResult> GetByIdAsync(Guid id);
        /// <summary>
        ///  Bu metot Yöneticinin,tüm branchların listelemesini sağlamaktadır.
        /// </summary>        
        /// <returns></returns>         
        Task<IResult> DeletedListAsync();
    }
}
