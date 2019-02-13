using Azure.Server.Utils.Email;
using IglooSmartHome.DataObjects;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace IglooSmartHome.Services
{
    public class SendgridService : IEmailService<Account>
    {
        public void SendEmail(string subject, string message, Account to)
        {
            var msg = new SendGridMessage();
            msg.SetFrom(new EmailAddress("team@igloo.com", "Igloo smart home"));
            msg.AddTo(to.Email);
            msg.SetSubject(subject);
            msg.AddContent(MimeType.Html, message);

            var apiKey = System.Environment.GetEnvironmentVariable("SENDGRID_APIKEY");
            var client = new SendGridClient(apiKey);
            client.SendEmailAsync(msg);
        }
    }
}