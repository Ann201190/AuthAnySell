using Auth.Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Business.Service.Interfaces
{
   public interface IJWTService
    {
        string CreateJWT(Account account);
    }
}
