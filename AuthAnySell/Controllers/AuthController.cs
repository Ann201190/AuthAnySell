using Auth.Business.Service.Interfaces;
using AuthAnySell.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AuthAnySell.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public readonly IAuthService _authService;
        public readonly IJWTService _JWTService;
        public AuthController(IAuthService authService, IJWTService JWTService)
        {
            _authService = authService;
            _JWTService = JWTService;
        }

        [Route("confirm/{stringConfirm}")]
        [HttpGet]
        public async Task<IActionResult> Confirm(string stringConfirm)
        {
            var user = await _authService.ConfirmUserAsync(stringConfirm);
            if (user)
            {
                return Ok("Регистрация успешна");
            }

            return Ok("Регистрация НЕ успешна");
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Authorization request)
        {
            var user = await _authService.AuthenticateUserAsync(request.Email, request.Password);
            if (user!=null)
            {
               return Ok( new 
               { 
                   access_token =_JWTService.CreateJWT(user)
               });
            }

            return Unauthorized(); //401 код ошибки
        }
    }
}
