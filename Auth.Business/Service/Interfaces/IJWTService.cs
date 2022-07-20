using Auth.Business.Entities;

namespace Auth.Business.Service.Interfaces
{
    public interface IJWTService
    {
        string CreateJWT(Account account);
    }
}
