using Auth.Business.ViewModels;
using System.Threading.Tasks;

namespace Auth.Business.Service.Interfaces
{
    public interface IRegistrationService
    {
        Task<bool> RegistrationManagerAsync(string email, string password);
        string HashPassword(string password);
        Task<bool> RegistrationCashierAsync(string email);
        Task<bool> ChangePassword(string userEmail, ChangePasswordModel request);
    }
}
