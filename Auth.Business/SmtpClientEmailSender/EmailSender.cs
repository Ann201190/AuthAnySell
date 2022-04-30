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
        public EmailSender()
        {

        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            string fromMail = "AnySellTeam@gmail.com";  //"[YOUREMAILID]";
            string fromPassword = "ann201190";  //"[APPPASSWORD]";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = subject;
            message.To.Add(new MailAddress(email));
            message.Body = "<html><head><style>.btn {padding: 0;border: none;font: inherit;color: inherit;background-color: transparent;cursor: pointer;outline: none;display: inline-block;text-align: center;text-decoration: none;margin: 2px 0;border: solid 1px transparent;border-radius: 4px;padding: 0.5em 1em;color:#ffffff;background-color: #198754;}.btn:active {transform: translateY(1px);filter: saturate(150%);}.btn:hover,.btn:focus {color: #198754;border-color: currentColor;background-color: white;}.btn::-moz-focus-inner {border: none;}p{padding: 40px;color: black;}body {margin: auto;}.content {height: 500px;width: 100%;background: linear-gradient(to right, rgba(59, 193, 160, 0.8), rgba(255, 255, 255, 1));text-align: center;font-size: 25px;} .ii a[href] {color: white !important}</style></head><body> " + htmlMessage + " </body></html>";
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };
            smtpClient.Send(message);
          //  return Task.CompletedTask;
        }

    }
}
