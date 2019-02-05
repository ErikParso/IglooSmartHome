using Azure.Server.Utils.Email;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace IglooSmartHome.Services
{
    public class SendgridService : IEmailService
    {
        public void SendEmail(string subject, string message, string to)
        {
            var msg = new SendGridMessage();
            msg.SetFrom(new EmailAddress("team@balanse.com", "Balanse team"));
            msg.AddTo(to);
            msg.SetSubject(subject);
            msg.AddContent(MimeType.Html, message);

            var apiKey = System.Environment.GetEnvironmentVariable("SENDGRID_APIKEY");
            var client = new SendGridClient(apiKey);
            client.SendEmailAsync(msg);
        }
    }
}