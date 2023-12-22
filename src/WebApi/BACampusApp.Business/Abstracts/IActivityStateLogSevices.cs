namespace BACampusApp.Business.Abstracts
{
    public interface IActivityStateLogSevices
    {
        /// <summary>
        /// Bu metot activityStateLog nesnesinin veritabanında oluşturma işleminde kullanılmaktadır.
        /// </summary>
        /// <param name="activityStateLogCreateDto"></param>
        /// <returns>IResult</returns>

        Task<IResult> CreateAsync(ActivityStateLogCreateDto activityStateLogCreateDto);


        /// <summary>
        /// Bu metot tüm logların listelenmesinde kullanılmaktadır.
        /// </summary>        
        /// <returns>IResult</returns>

        Task<IResult> GetAllAsync();

        ///// <summary>
        ///// Bu metot verilen id ye göre eşleşen ActivityStateLog nesnesinin getirilmesini yapmaktadır.
        ///// </summary>
        /// <param name = "id" > Getirilmesi istenen ActivityStateLog nesnesi için verilen id parametresi.</param>
        ///// <returns>IResult</returns>

        Task<IResult> GetByIdAsync(Guid id);


        /// <summary>
        /// Bu metot tüm logların role göre listelenmesinde kullanılmaktadır.
        /// </summary>        
        /// <returns>IResult</returns>

        Task<IResult> GetAllAsync(string role);


    }
}
