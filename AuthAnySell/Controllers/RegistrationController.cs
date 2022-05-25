using Auth.Business.Service.Interfaces;
using AuthAnySell.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AuthAnySell.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : Controller
    {
        public readonly IRegistrationService _registrationService;

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



    }
}