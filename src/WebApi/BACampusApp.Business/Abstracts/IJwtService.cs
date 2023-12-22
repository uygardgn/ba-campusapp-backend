namespace BACampusApp.Business.Abstracts
{
    public interface IJwtService
    {
        /// <summary>
        /// Bu metot ile verilen bir BaseUser için JWT token'ı oluşturulmaktadır.
        /// </summary>
        /// <param name="user">Token'ı oluşturulacak BaseUser nesnesi</param>
        /// <returns>JwtSecurityTokenHandler().WriteToken(token)</returns>
        Task<string> GenerateTokenAsync(IdentityUser user);
    }
}
