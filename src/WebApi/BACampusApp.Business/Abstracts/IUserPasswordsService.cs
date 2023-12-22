namespace BACampusApp.Business.Abstracts
{
    public interface IUserPasswordsService
    {
        /// <summary>
        /// Bu metot userpasswords nesnesinin veritabanında oluşturma işleminde kullanılmaktadır.
        /// </summary>
        /// <param name="userPasswordsCreateDto"></param>
        /// <returns>IResult</returns>

        Task<IResult> CreateAsync(UserPasswordsCreateDto userPasswordsCreateDto);

        /// <summary>
        /// Bu metot tüm kullanıcıların son üç şifrelerinin listelenmesinde kullanılmaktadır.
        /// </summary>        
        /// <returns>IResult</returns>

        Task<IResult> GetAllAsync();

        /// <summary>
        /// Bu metot verilen id ye göre eşleşen UserPasswords nesnesinin getirilmesini yapmaktadır.
        /// </summary>
        /// <param name = "id" > Getirilmesi istenen UserPasswords nesnesi için verilen id parametresi.</param>
        /// <returns>IResult</returns>

        Task<IResult> GetByIdAsync(Guid id);

        /// <summary>
        /// Bu metot tüm userpasswordlerin kullanıcı id sine göre listelenmesinde kullanılmaktadır.
        /// </summary>
        /// <param name = "id" > Getirilmesi istenen userpassword nesneleri için kullanıcının id parametresi.</param>
        /// <returns>IResult</returns>

        Task<IResult> GetAllByUserIdAsync(Guid id);
    }
}
