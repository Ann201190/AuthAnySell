using Auth.Business.SmtpClientEmailSender.Interfaces;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Auth.Business.SmtpClientEmailSender
{
    class EmailSender: IEmailSender
    {
        private readonly string _anySellEmail = "any_sell@yahoo.com";
        private readonly string _password = "qztnrlddhxuipeca";
        public EmailSender()
        {

        }

        public async Task<bool> SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                var mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(_anySellEmail);
                mailMessage.To.Add(email);
                mailMessage.Subject = subject;
                //mailMessage.Attachments.Add(new Attachment(@"C:\\attachedfile.jpg"));//attach the file
                mailMessage.Body = htmlMessage;
                mailMessage.IsBodyHtml = true;
                
                var smtpClient = new SmtpClient("smtp.mail.yahoo.com");
                smtpClient.Port = 587;                                                     //port number for Yahoo
                smtpClient.Credentials = new NetworkCredential(_anySellEmail, _password);  //credentials to login in to yahoo account
                smtpClient.EnableSsl = true;
                smtpClient.Send(mailMessage);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
