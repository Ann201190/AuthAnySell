using System.Threading.Tasks;

namespace Auth.Business.SmtpClientEmailSender.Interfaces
{
    public interface IEmailSender
    {
        Task<bool> SendEmailAsync(string email, string subject, string htmlMessage);
    }
}
