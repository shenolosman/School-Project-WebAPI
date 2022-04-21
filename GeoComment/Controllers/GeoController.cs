
using Microsoft.AspNetCore.Mvc;
using GeoComment.Services;

namespace GeoComment.Controllers
{
    [Route("api/geo-comments")]
    [ApiController]
    public class GeoController : ControllerBase
    {
        private readonly GeoService _geoService;
        public GeoController(GeoService geoService)
        {
            _geoService = geoService;
        }
        [HttpGet("~/test/reset-db")]
        [ApiVersion("0.1")]
        //[Route("reset-db")]
        public async Task<bool> ResetDatabase()
        {
            return await _geoService.ResetDB();
        }
    }
    
}
