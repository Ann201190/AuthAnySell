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
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(_anySellEmail);

                //receiver email adress
                mailMessage.To.Add(email);

                //subject of the email
                mailMessage.Subject = subject;

                //attach the file
                //mailMessage.Attachments.Add(new Attachment(@"C:\\attachedfile.jpg"));
                mailMessage.Body = htmlMessage;

                mailMessage.IsBodyHtml = true;

                //SMTP client
                SmtpClient smtpClient1 = new SmtpClient("smtp.mail.yahoo.com");
                //port number for Yahoo
                smtpClient1.Port = 587;
                //credentials to login in to yahoo account
                smtpClient1.Credentials = new NetworkCredential(_anySellEmail, _password);
                //enabled SSL
                smtpClient1.EnableSsl = true;
                //Send an email
                smtpClient1.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
