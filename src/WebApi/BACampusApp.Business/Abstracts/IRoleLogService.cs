namespace BACampusApp.Business.Abstracts
{
    public interface IRoleLogService
    {
        /// <summary>
        /// Bu metot roleLog nesnesinin veritabanında oluşturma işleminde kullanılmaktadır.
        /// </summary>
        /// <param name="roleLogCreateDto"></param>
        /// <returns>IResult</returns>

        Task<IResult> CreateAsync(RoleLogCreateDto roleLogCreateDto);


        /// <summary>
        /// Bu metot tüm logların listelenmesinde kullanılmaktadır.
        /// </summary>        
        /// <returns>IResult</returns>

        Task<IResult> GetAllAsync();

        /// <summary>
        /// Bu metot verilen id ye göre eşleşen RoleLog nesnesinin getirilmesini yapmaktadır.
        /// </summary>
        /// <param name = "id" > Getirilmesi istenen RoleLog nesnesi için verilen id parametresi.</param>
        /// <returns>IResult</returns>

        Task<IResult> GetByIdAsync(Guid id);


        /// <summary>
        /// Bu metot tüm logların kullanıcı id sine göre listelenmesinde kullanılmaktadır.
        /// </summary>
        /// <param name = "id" > Getirilmesi istenen RoleLog nesneleri için kullanıcının id parametresi.</param>
        /// <returns>IResult</returns>

        Task<IResult> GetAllByUserIdAsync(Guid id);

        /// <summary>
        /// Bu metot kullanıcı için son olarak oluşturulan RoleLog nesnesinin getirilmesi için kullanılmaktadır.
        /// </summary>
        /// <param name = "id" > Getirilmesi istenen son RoleLog nesnesi için kullanıcının id parametresi.</param>
        /// <returns>IResult</returns>

        Task<IResult> GetLastRoleLogByUserIdAsync(Guid id);
    }
}
