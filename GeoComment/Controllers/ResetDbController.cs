using GeoComment.Services;
using Microsoft.AspNetCore.Mvc;

namespace GeoComment.Controllers
{
    [ApiController]
    [Route("test")]
    public class ResetDbController : Controller
    {
        private readonly GeoService _geoService;

        public ResetDbController(GeoService geoService)
        {
            _geoService = geoService;
        }

        [ApiVersion("0.1")]
        [ApiVersion("0.2")]
        [Route("reset-db")]
        [HttpGet]
        public async Task<IActionResult> ResetDatabase()
        {
            await _geoService.ResetDb();
            return Ok();
        }
    }
}
