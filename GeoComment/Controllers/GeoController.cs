using GeoComment.Models;
using GeoComment.Services;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        [ApiVersion("0.1")]
        public async Task<ActionResult> CommentsWithin(int minlon, int maxlon, int minlat, int maxlat)
        {
            var result = await _geoService.RangeFilter(minlon, maxlon, minlat, maxlat);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost]
        [ApiVersion("0.1")]
        public async Task<ActionResult> SaveMyComment(MyDTO geoComment)
        {
            var result = await _geoService.Add(geoComment);
            return Created("", result);
        }
        [HttpGet("{id}")]
        [ApiVersion("0.1")]
        public async Task<ActionResult> GetID(int id)
        {
            var respons = await _geoService.GetId(id);
            if (respons == null)
                return NotFound("This value not in the db");
            return Ok(respons);
        }
        [HttpGet("~/test/reset-db")]
        [ApiVersion("0.1")]
        public async Task<bool> ResetDatabase()
        {
            return await _geoService.ResetDB();
        }
    }
}
