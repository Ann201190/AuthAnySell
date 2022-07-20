using Auth.Business.Service.Interfaces;
using Auth.Business.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AuthAnySell.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckController : Controller
    {
        public readonly ICheckService _checkService;
        public CheckController(ICheckService checkService)
        {
            _checkService = checkService;
        }

        [Route("checksend")]
        [HttpPost]
        public async Task<IActionResult> CheckSend([FromBody] Check request)
        {
            var isSend = await _checkService.CheckSend(request);
            if (isSend)
            {
                return Ok(true);
            }
            return Ok(false);
        }
    }
}