using GeoComment.DTO.v1;
using GeoComment.DTO.v2;
using GeoComment.Models;
using GeoComment.Services;
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
            return Ok(result);
        }
        [HttpPost]
        [ApiVersion("0.1")]
        public async Task<ActionResult> SaveMyComment(InputDto geoComment)
        {
            var result = await _geoService.Add(geoComment);
            return Created("", result);
        }
        [HttpGet]
        [Route("{id}")]
        [ApiVersion("0.1")]
        public async Task<ActionResult> GetID(int id)
        {
            var response = await _geoService.GetId(id);
            if (response == null)
                return NotFound("This value not in the db");
            return Ok(response);
        }

        [ApiVersion("0.2")]
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult> GetByID(int id)
        {
            //var user = await _userManager.GetUserAsync(principal);
            //if (user == null)
            //{
            //    return NotFound();
            //}
            var respons = await _geoService.GetIdv2(id);
            if (respons == null)
                return NotFound("This value not in the db");
            return Ok(respons);
        }
        [HttpPost]
        [ApiVersion("0.2")]
        public async Task<ActionResult<ResultDtoV2>> SaveComment(InputDtoV2 model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }

            if (user.UserName == null)
            {
                return NotFound();
            }
            // model.body.author = user.UserName;
            //if (model.body.title == null)
            //{
            //    model.body.title = _geoService.GetTitle(model.body.message);
            //}
            var comment = new MyGeoComment
            {
                Author = user.UserName,
                Latitude = model.latitude,
                Longitude = model.longitude,
                Message = model.body.message,
                Title = model.body.title,
                User = user
            };
            var result = await _geoService.Addv2(comment);
            // model.id = result.Entity.id;
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
            if (newcomment.id == null || newcomment.id > 888 || newcomment.body.author == null)
            {
                return NotFound();
            }
            return Created("", newcomment);
            //return CreatedAtAction("", new { id = newcomment.id }, newcomment);
        }

        [HttpGet]
        [Route("{username}")]
        [ApiVersion("0.2")]
        public async Task<ActionResult<List<ResultDtoV2>>> GetByUserName(string username)
        {
            // var user = await _userManager.GetUserAsync(User);
            //if (user.UserName == username)
            //{
            //    return NotFound();
            //}

            var result = await _geoService.FindByName(username);
            if (result.Length <= 0)
                return NotFound();
            //var a = result.ToList();

            //if (result == null)
            //   return NotFound();
            //foreach (var item in result.ToList())
            //{
            //    if (item.Author == null || item.Author != username)
            //    {
            //        return NotFound();
            //    }
            //}
            var model = new List<ResultDtoV2>();
            foreach (var myGeoComment in result)
            {

                var item = new ResultDtoV2()
                {
                    body = new ResultBody
                    {
                        author = myGeoComment.Author,
                        title = myGeoComment.Title,
                        message = myGeoComment.Message
                    },
                    latitude = myGeoComment.Latitude,
                    longitude = myGeoComment.Longitude
                };
                model.Add(item);
            }

            return Ok(model);
        }
        [HttpGet]
        [ApiVersion("0.2")]
        public async Task<ActionResult<List<ResultDtoV2>>> CommentsFilterv2(double? minLon, double? maxLon, double? minLat, double? maxLat)
        {
            if (minLon == null || maxLon == null || minLat == null || maxLat == null)
                return StatusCode(400);
            if (!ModelState.IsValid)
                return BadRequest();
            var result = await _geoService.RangeFilterv2(minLon, maxLon, minLat, maxLat);
            return Ok(result);
        }

    }
}
