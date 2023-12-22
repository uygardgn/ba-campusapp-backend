namespace BACampusApp.Business.Abstracts
{
    public interface IAccountService
    {
        /// <summary>
        /// Bu metot Şifre değiştirme  işlemini yapmaktadır.
        /// </summary>
        /// <param name="resetPassDto"></param>
        Task<IResult> ResetPasswordAsync(ResetPasswordDto resetPassDto);

        /// <summary>
        /// Bu metot email ve password ü gönderilen yeni bir şifre oluşturmasını sağlar.
        /// </summary>
        /// <param name="changePassDto">database'de eski şifreyi karşılaştırıp yeni şifreyi database kaydetmesini sağlar.</param>
        Task<IResult> ChangePasswordAsync(ChangePasswordDto changePassDto);

        /// <summary>
        /// Bu method gönderilen LoginDto nesnesin login işlemlerini kontrol eder.
        /// </summary>
        /// <param name="loginDto"> login için gerekli email ve şifre içeren dto</param>
        /// <returns>
        /// <see cref="AuthResult"/>
        /// </returns>
        Task<AuthResult> LoginAsync(LoginDto loginDto);
    }
}
