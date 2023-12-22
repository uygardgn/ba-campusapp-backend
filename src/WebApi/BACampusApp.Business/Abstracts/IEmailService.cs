namespace BACampusApp.Business.Abstracts
{
    public interface IEmailService
    {
        /// <summary>
        /// Bu metot mail gönderme işlemini yapacaktır.
        /// </summary>
        /// <param name="toEmail"></param>
        /// <param name="newPassword"></param>
        Task SendEmailAsync(string toEmail, string newPassword);

        /// <summary>
        /// Bu metot şifre unutulduktan sonra mail gönderme  işlemini yapacaktır.
        /// </summary>
        /// <param name="mailInformation">
        /// Dizinin 0.indisi gönderilecek mail.
        /// Dizinin 1.indisi konu.
        /// Dizinin 2.indisi mesaj body'si.
        /// </param>
        Task SendResetEmailLinkAsync(string toEmail, string passwordResetLink);
    }
}
