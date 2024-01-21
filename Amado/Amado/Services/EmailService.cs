using System.Net.Mail;
using System.Net;

namespace Amado.Services
{
    public class EmailService : IEmailService
    {
        public  async Task SendMail(string to, string subject, string body)
        {
            string smtpServer = "smtp.yandex.com";
            int smtpPort = 587;
            string smtpUsername = "dotnetp335@yandex.com";
            string smtpPassword = "knccjgorlgetlxuo";

            using (MailMessage mail = new MailMessage(smtpUsername, to))
            {
                mail.Subject = subject;
                mail.Body = body;

                using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
                {
                    smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                    smtpClient.EnableSsl = true;

                    
                       smtpClient.Send(mail);
                    
                }
            }
        }
    }
}
