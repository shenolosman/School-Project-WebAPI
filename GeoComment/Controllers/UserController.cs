using GeoComment.DTO.v2;
using GeoComment.Models;
using GeoComment.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GeoComment.Controllers
{
    [Route("api/user")]

    [ApiVersion("0.2")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly GeoService _geoService;
        private readonly UserManager<User> _userManager;

        public UserController(GeoService geoService, UserManager<User> userManager)
        {
            _geoService = geoService;
            _userManager = userManager;
        }
        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> SaveUser(UserDTO userDto)
        {
            var user = new User()
            {
                UserName = userDto.UserName
            };

            await _userManager.CreateAsync(user, userDto.Password);

            var success = await _userManager.CheckPasswordAsync(user,
                   userDto.Password);
            if (!success)
            {
                return BadRequest();
            }
            var createdUser =
                await _userManager.FindByNameAsync(user.UserName);
            return Created("",
                new
                {
                    username = createdUser.UserName,
                    id = createdUser.Id
                });
        }

        [Route("login")]
        [HttpPost]
        public IActionResult Login(UserDTO user)
        {
            return Ok(user);
        }
    }
}
