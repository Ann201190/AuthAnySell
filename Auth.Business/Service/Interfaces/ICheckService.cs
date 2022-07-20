using Auth.Business.ViewModels;
using System.Threading.Tasks;

namespace Auth.Business.Service.Interfaces
{
    public interface ICheckService
    {
        Task<bool> CheckSend(Check check);
    }
}
