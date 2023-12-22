namespace BACampusApp.Authentication.Options
{
    public class EmailOptions
    {
        public string FromEmail { get; set; }
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string ForgotPasswordBody { get; set; }
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpPassword { get; set; }

        public bool EnableSsl = true;
    }
}
