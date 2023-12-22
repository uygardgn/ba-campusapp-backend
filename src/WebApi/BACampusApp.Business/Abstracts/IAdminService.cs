namespace BACampusApp.Business.Abstracts
{
    public interface IAdminService
    {
        /// <summary>
        /// Bu metot Admin nesnesi oluşturma işlemini yapmaktadır.
        /// </summary>
        /// <param name="adminCreateDto"></param>
        /// <returns>IResult</returns>
        Task<IResult> CreateAsync(AdminCreateDto adminCreateDto);

        /// <summary>
        /// Bu metot Admin nesnesinin silme işlemini yapmaktadır.
        /// </summary>
        /// <param name="id">Silinmesi istenen Admin nesnesi için verilen id parametresi.</param>
        /// <returns>IResult</returns>
        Task<Result> DeleteAsync(Guid id);

        /// <summary>
        /// Bu metot Admin nesnesinin güncelleme işlemini yapmaktadır.
        /// </summary>
        /// <param name="adminUpdateDto">Güncellenmesi istenen Admmin nesnesi için güncellenecek verileri içeren AdminUpdateDto nesnesi.</param>
        /// <returns>IResult</returns>
        Task<IResult> UpdateAsync(AdminUpdateDto adminUpdateDto);

        /// <summary>
        /// Bu metot verilen id ye göre eşleşen Admin nesnesinin getirilmesini yapmaktadır.
        /// </summary>
        /// <param name="id">Getirilmesi istenen Admin nesnesi için verilen id parametresi.</param>
        /// <returns>IResult</returns>
        Task<IResult> GetByIdAsync(Guid id);

        /// <summary>
        /// Bu metot veritabında kayıtlı tüm Admin nesnelerinin getirilmesi işlemini yapmaktadır.
        /// </summary>
        /// <returns>IResult</returns>
        Task<IResult> GetAllAsync();

        /// <summary>
        /// Bu metot verilen identityid'ye göre admin döndürür
        /// </summary>
        /// <param name="identityId"></param>
        /// <returns></returns>
        Task<IResult> GetByIdentityId(string identityId);


        /// <summary>
        /// Bu metot sisteme giriş yapan admin rolündeki kullanıcı nesnesini getirmek için kullanılır.
        /// </summary>
        /// <param name="currentUserId"></param>
        /// <returns>IResult</returns>
        Task<IResult> GetCurrentAdminDetailsAsync(string currentUserId);

        /// <summary>
        /// Bu metot giriş yapan admin rolündeki kullanıcının kendi bilgilerini güncellemesi için kullanılır.
        /// </summary>
        /// <param name="adminUpdateDto">Güncellemesi yapılacak olan verileri içeren AdminCurrentUserUpdateDto nesnesi</param>
        /// <returns>IResult</returns>
        Task<IResult> UpdateCurrentAdminAsync(AdminCurrentUserUpdateDto adminUpdateDto);

        /// <summary>
        /// Bu metot giriş yapan admin rolündeki kullanıcının kendi bilgilerini güncellemesi için kullanılır.
        /// </summary>
        /// <param name="adminLoggedInUserUpdateDto">Güncellemesi yapılacak olan verileri içeren AdminLoggedInUserUpdateDto nesnesi</param>
        /// <returns>IResult</returns>
        Task<IResult> UpdateLoggedInAdminAsync(AdminLoggedInUserUpdateDto adminLoggedInUserUpdateDto);
    }
}
