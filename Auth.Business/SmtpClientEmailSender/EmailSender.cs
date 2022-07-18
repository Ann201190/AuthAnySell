using Auth.Business.SmtpClientEmailSender.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
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

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
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
                mailMessage.Body = "<html><head><style>@import url('https://fonts.googleapis.com/css2?family=Nunito+Sans:wght@300;600;800&family=Nunito:ital,wght@0,900;1,900&display=swap');.btn {padding: 0;border: none;font: inherit;color: inherit;background-color: transparent;cursor: pointer;outline: none;display: inline-block;text-align: center;text-decoration: none;margin: 2px 0;border: solid 1px transparent;border-radius: 4px;padding: 0.5em 1em;color: rgba(254, 161, 22);background-color: #ffffff;}.btn:active {transform: translateY(1px);filter: saturate(150%);}.btn:hover,.btn:focus {color: #ffffff;border-color: currentColor;background-color: #0F172B;}.btn::-moz-focus-inner {border: none;}h5 {color: #0F172B;}p {color: black;}body {margin: auto;font-family: 'Nunito', sans-serif;font-family: 'Nunito Sans', sans-serif;}.content {height: 600px;width: 100%;background: rgba(254, 161, 22);font-size: 25px;}.info {text-align: center;padding-top: 50px;}.ii a[href] {color: white !important}.logo {margin-left: 30px;}</style></head><body>" + htmlMessage + "</body></html>";

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
            }
            catch (Exception ex)
            {

            }
        }

    }
}
