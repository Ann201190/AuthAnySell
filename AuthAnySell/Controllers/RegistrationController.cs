using Auth.Business.Service.Interfaces;
using Auth.Business.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthAnySell.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : Controller
    {
        public readonly IRegistrationService _registrationService;
        private string _userEmail => User.Claims.Single(c => c.Type == ClaimTypes.Email).Value;

        public RegistrationController (IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        [Route("registrationmanager")]
        [HttpPost]
        public async Task<IActionResult> RegistrationManager([FromBody] RegistrationManager request)
        {
           var isRegistr = await _registrationService.RegistrationManagerAsync(request.Email, request.Password);          
            if (isRegistr)
            {
                return Ok(true);
            }
            return Ok(false);
        }

        [Route("registrationcashier")]
        [HttpPost]
        public async Task<IActionResult> RegistrationCashier([FromBody] RegistrationCashier request)
        {
            var isRegistr = await _registrationService.RegistrationCashierAsync(request.Email);
            if (isRegistr)
            {
                return Ok(true);
            }
            return Ok(false);
        }

        [Route("changepassword")]
        [HttpPost]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel request)
        {
            try
            {
                var isChangedPassword = await _registrationService.ChangePassword(_userEmail, request);
                if (isChangedPassword)
                {
                    return Ok(true);
                }
                return Ok(false);
            }
            catch
            {
                return Ok(false);
            }
            
            
        }
    }
}