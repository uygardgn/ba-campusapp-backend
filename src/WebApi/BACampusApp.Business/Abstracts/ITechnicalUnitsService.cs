namespace BACampusApp.Business.Abstracts
{
    public interface ITechnicalUnitsService
    {
        /// <summary>
        ///  Bu metot TechnicalUnits nesnesi oluşturma işlemini yapmaktadır.
        /// </summary>
        /// <param name="newUnits"></param>
        ///  <returns></returns>
        Task<IResult> AddAsync(TUnitCreateDto newUnits);

        /// <summary>
        ///  TUnitsDetailsDto ve TechnicalUnits nesnelerini listeleme işlemlerini yapmaktadır.
        /// </summary>
        /// <param name="id">detayları getirilmek istenen TechnicalUnits nesnesinin Guid tipinde Id si</param>
        /// <returns></returns>
        Task<IResult> GetByIdAsync(Guid id);

        /// <summary>
        ///  Bu metot TechnicalUnits nesnesi silme işlemini yapacaktır.
        /// </summary>
        /// <param name="id">silinmek  istenen TechnicalUnits nesnesinin Guid tipinde Id si </param>        
        Task<IResult> DeleteAsync(Guid id);

        /// <summary>
        /// Bu metot TechnicalUnits nesnesinin güncelleme işlemini yapacaktır.
        /// </summary>
        /// <param name="entity">güncellenmek istenen TechnicalUnits nesnesinin TUnitsUpdateDto tipinde entity'si</param>
        /// <returns></returns>
        Task<IResult> UpdateAsync(TUnitUpdateDto entity);

        /// <summary>
        ///  Bu metot Adminin,tüm TechnicalUnitsleri listelemesini sağlamaktadır.
        /// </summary>        
        /// <returns></returns>       
        Task<IResult> GetListAsync();

        /// <summary>
        ///  Bu metot Yöneticinin,tüm eğitmenleri listelemesini sağlamaktadır.
        /// </summary>        
        /// <returns></returns>         
        Task<IResult> DeletedListAsync();
    }
}
