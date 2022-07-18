using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace AuthAnySell.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VersionController : ControllerBase
    {
        private readonly IConfiguration _config;

        public VersionController(IConfiguration config)
        {
            _config = config;
        }

        [Route("unityminversion")]
        [HttpGet]
        public IActionResult GetMinVersion()
        {
            return Ok(_config.GetValue<double>("UnitySettings:MinVersion"));
        }
    }
}
