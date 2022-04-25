using Auth.Business.Service.Interfaces;
using AuthAnySell.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AuthAnySell.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : Controller
    {
        public readonly IRegisterService _registerService;

        public RegisterController (IRegisterService registerService, IJWTService JWTService)
        {
            _registerService = registerService;
        }


        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] Authorization request)
        {
           var isRegistr = await _registerService.RegisterUserAsync(request.Email, request.Password);
        // придумать что вернуть
            
            if (isRegistr)
            {
                return Ok("ok");
            }

            return Ok("error");
        }

    }
}