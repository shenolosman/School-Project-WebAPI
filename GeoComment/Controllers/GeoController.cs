
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

    }
}
