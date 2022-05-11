using GeoComment.DTO.v1;
using GeoComment.DTO.v2;
using GeoComment.Models;
using GeoComment.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GeoComment.Controllers
{
    [Route("api/geo-comments")]
    [ApiController]
    public class GeoController : ControllerBase
    {
        private readonly GeoService _geoService;
        private readonly UserManager<User> _userManager;

        public GeoController(GeoService geoService, UserManager<User> userManager)
        {
            _geoService = geoService;
            _userManager = userManager;
        }
        [HttpGet]
        [ApiVersion("0.1")]
        public async Task<ActionResult<List<ResultDto>>> CommentsFilter(double? minLon, double? maxLon, double? minLat, double? maxLat)
        {
            if (minLon == null || maxLon == null || minLat == null || maxLat == null)
                return StatusCode(400);
            if (!ModelState.IsValid)
                return BadRequest();
            var result = await _geoService.RangeFilter(minLon, maxLon, minLat, maxLat);
            result.Select(x => new ResultDto
            {
                author = x.Author,
                message = x.Message,
                latitude = x.Latitude,
                longitude = x.Longitude,
                id = x.Id
            });
            return Ok(result);
        }
        [HttpPost]
        [ApiVersion("0.1")]
        public async Task<ActionResult> SaveComment(InputDto geoComment)
        {
            var geo = new MyGeoComment
            {
                Author = geoComment.author,
                Message = geoComment.message,
                Latitude = geoComment.latitude,
                Longitude = geoComment.longitude
            };
            var result = await _geoService.Add(geo);
            return Created("", result);
        }
        [HttpGet]
        [Route("{id}")]
        [ApiVersion("0.1")]
        public async Task<ActionResult<ResultDto>> GetId(int id)
        {
            var result = await _geoService.GetId(id);
            if (result == null)
                return NotFound();
            var myNewDto = new ResultDto()
            {
                id = result.Id,
                author = result.Author,
                message = result.Message,
                longitude = result.Longitude,
                latitude = result.Latitude
            };
            return Ok(myNewDto);
        }
        [ApiVersion("0.2")]
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<ResultDtoV2>> GetIdv2(int id)
        {
            var result = await _geoService.GetId(id);
            if (result == null)
                return NotFound();
            var myNewDto = new ResultDtoV2
            {
                body = new ResultBody
                {
                    author = result.Author,
                    message = result.Message,
                    title = result.Title
                },
                id = result.Id,
                longitude = result.Longitude,
                latitude = result.Latitude
            };
            return Ok(myNewDto);
        }
        [HttpPost]
        [ApiVersion("0.2")]
        [Authorize]
        public async Task<ActionResult<ResultDtoV2>> SaveComment(InputDtoV2 model)
        {
            var user = await _userManager.GetUserAsync(User);
            var comment = new MyGeoComment
            {
                Author = user.UserName,
                Latitude = model.latitude,
                Longitude = model.longitude,
                Message = model.body.message,
                Title = model.body.title,
                User = user
            };
            var result = await _geoService.Add(comment);
            var newcomment = new ResultDtoV2()
            {
                id = result.Id,
                longitude = result.Longitude,
                latitude = result.Latitude,
                body = new ResultBody
                {
                    author = result.Author,
                    message = result.Message,
                    title = result.Title
                }
            };
            return Created("", newcomment);
        }
        [HttpGet]
        [Route("{username}")]
        [ApiVersion("0.2")]
        public async Task<ActionResult<List<ResultDtoV2>>> GetByUserName(string username)
        {
            var result = await _geoService.FindByName(username);
            if (result.Length <= 0)
                return NotFound();
            var model = new List<ResultDtoV2>();
            foreach (var item in result)
            {
                var geo = new ResultDtoV2()
                {
                    id = item.Id,
                    latitude = item.Latitude,
                    longitude = item.Longitude,
                    body = new ResultBody
                    {
                        author = item.Author,
                        title = item.Title,
                        message = item.Message
                    }
                };
                model.Add(geo);
            }
            return Ok(model);
        }
        [HttpGet]
        [ApiVersion("0.2")]
        [Produces("application/json")]
        public async Task<ActionResult<List<ResultDtoV2>>> CommentsFilterv2(double? minLon, double? maxLon, double? minLat, double? maxLat)
        {
            if (minLon == null || maxLon == null || minLat == null || maxLat == null)
                return StatusCode(400);
            if (!ModelState.IsValid)
                return BadRequest();
            var result = await _geoService.RangeFilter(minLon, maxLon, minLat, maxLat);
            var models = result.Select(item => new ResultDtoV2()
            {
                body = new ResultBody
                {
                    author = item.Author,
                    message = item.Message,
                    title = item.Title
                },
                latitude = item.Latitude,
                longitude = item.Longitude,
                id = item.Id
            }).ToList();
            return Ok(models);
        }

    }
}
