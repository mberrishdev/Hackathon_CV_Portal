using Hackathon_CV_Portal.Application.Abstractions;
using Hackathon_CV_Portal.Application.Settings;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Hackathon_CV_Portal.Application.Implementations.EmailService
{
    public class EmailSender : IEmailSender
    {
        public EmailSettings _emailSettings { get; }

        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            Execute(email, subject, message, null).Wait();

            return Task.FromResult(0);
        }

        public Task SendEmailWithAttachmentsAsync(string email, string subject, string message, List<Attachment> attachments)
        {
            Execute(email, subject, message, attachments).Wait();

            return Task.FromResult(0);
        }

        public async Task Execute(string toEmail, string subject, string message, List<Attachment> attachments)
        {
            try
            {

                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_emailSettings.PlatformMailAddress, _emailSettings.FromName)
                };

                mail.To.Add(new MailAddress(toEmail));

                if (attachments != null)
                {
                    foreach (var item in attachments)
                    {
                        mail.Attachments.Add(item);
                    }
                }

                mail.Subject = subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.Normal;

                using (SmtpClient smtp = new SmtpClient(_emailSettings.ServerAddress, _emailSettings.ServerPort))
                {
                    smtp.Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password);
                    smtp.EnableSsl = _emailSettings.ServerUseSsl;

                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
