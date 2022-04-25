using Auth.Business.Entities;
using System.Threading.Tasks;

namespace Auth.Business.Service.Interfaces
{
    public interface IAuthService
    {
        Task<Account> AuthenticateUserAsync(string email, string password);
        Task<bool> ConfirmUserAsync(string stringConfirm);
    }
}
