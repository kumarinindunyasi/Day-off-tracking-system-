using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Net.Mail;

namespace API.Service.EmailSenderServices
{
    public class EmailSenderService: IEmailSenderService
    {
        public Task SendEmailAsync(string email, string subject, string message) 
        {
            var mail = "goldtagizin@hotmail.com";
            var pw = "Goldtag1";

            var client = new SmtpClient("smtp-mail.outlook.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(mail, pw)
            };

            return client.SendMailAsync(
                new MailMessage(from: mail,
                                to: email,
                                subject,
                                message
                                ));
        }
    }
}
