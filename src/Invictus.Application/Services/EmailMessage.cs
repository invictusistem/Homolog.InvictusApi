using Invictus.Application.Dtos;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Threading.Tasks;

namespace Invictus.Application.Services
{
    public class EmailMessage
    {
        public class AuthMessageSender : IEmailSender
        {
            private IOptions<AppSettings> _settings;
            public AuthMessageSender(IOptions<AppSettings> settings)
            {
                _settings = settings;
            }

            public async Task SendEmailAsync(string email, string subject, string message)
            {
                var emailMessage = new MimeMessage();
                emailMessage.From.Add(new MailboxAddress(_settings.Value.SMTPAccount));
                emailMessage.To.Add(new MailboxAddress(email));
                emailMessage.Subject = subject;
                emailMessage.Body = new TextPart("html") { Text = message };

                

                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    await client.ConnectAsync(_settings.Value.SMTPServer, _settings.Value.SMTPPort, false);
                    await client.AuthenticateAsync(_settings.Value.SMTPAccount, _settings.Value.SMTPPassword);
                    await client.SendAsync(emailMessage);
                    await client.DisconnectAsync(true);

                }
            }

            //public Task SendSmsAsync(string number, string message)
            //{
            //    // Plug in your SMS service here to send a text message.
            //    return Task.FromResult(0);
            //}
        }

        //public interface ISmsSender
        //{
        //    Task SendSmsAsync(string number, string message);
        //}

        public interface IEmailSender
        {
            Task SendEmailAsync(string email, string subject, string message);
        }
    }
}
