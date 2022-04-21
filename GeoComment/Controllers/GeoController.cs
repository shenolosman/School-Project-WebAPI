using GeoComment.Models;
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
            if (respons==null)
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
