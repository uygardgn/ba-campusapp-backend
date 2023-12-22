namespace BACampusApp.Business.Concretes
{
    public class EmailManager : IEmailService
    {

        private readonly EmailOptions _emailSettings;
        private readonly ForgotPasswordEmailOptions _forgotPasswordEmailSettings;

        public EmailManager(IOptions<EmailOptions> emailSettings, IOptions<ForgotPasswordEmailOptions> forgotPasswordEmailSettings)
        {
            _emailSettings = emailSettings.Value;
            _forgotPasswordEmailSettings = forgotPasswordEmailSettings.Value;
        }

        public async Task SendResetEmailLinkAsync(string toEmail, string passwordResetLink)
        {

            string body = $@"Şifrenizi yenilemek için linke tıklayınız: <br> <a href='{passwordResetLink}'>{passwordResetLink}</a>";
            var client = new SmtpClient() { Host = _forgotPasswordEmailSettings.SmtpHost, Port = _forgotPasswordEmailSettings.SmtpPort };
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(_forgotPasswordEmailSettings.FromEmail, _forgotPasswordEmailSettings.SmtpPassword);
            MailMessage message = new MailMessage(_forgotPasswordEmailSettings.FromEmail, toEmail, _forgotPasswordEmailSettings.Subject, body);
            message.IsBodyHtml = true;
            await client.SendMailAsync(message);
        }

        public async Task SendEmailAsync(string toEmail, string newPassword)
        {
            string subject = _emailSettings.Subject;
            string body = string.Format(_emailSettings.Body, newPassword);

            var client = new SmtpClient() { Host = _emailSettings.SmtpHost, Port = _emailSettings.SmtpPort };
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(_emailSettings.FromEmail, _emailSettings.SmtpPassword);

            MailMessage message = new MailMessage(_emailSettings.FromEmail, toEmail, subject, body);
            message.IsBodyHtml = true;

            await client.SendMailAsync(message);
        }

        public async Task SendChangePasswordEmailAsync(string toEmail, string newPassword)
        {
            string subject = _emailSettings.Subject;
            string body = string.Format(_emailSettings.ForgotPasswordBody, newPassword);

            var client = new SmtpClient() { Host = _emailSettings.SmtpHost, Port = _emailSettings.SmtpPort };
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(_emailSettings.FromEmail, _emailSettings.SmtpPassword);

            MailMessage message = new MailMessage(_emailSettings.FromEmail, toEmail, subject, body);
            message.IsBodyHtml = true;

            await client.SendMailAsync(message);
        }
    }
}

