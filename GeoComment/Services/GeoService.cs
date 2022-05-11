using GeoComment.Data;
using GeoComment.DTO.v1;
using GeoComment.DTO.v2;
using GeoComment.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GeoComment.Services
{
    public partial class GeoService
    {
        private readonly GeoDbContext _dbContext;
        private readonly UserManager<User> _userManager;

        public GeoService(GeoDbContext dbContext, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<MyGeoComment> Add(InputDto geoComment)
        {
            var geo = new MyGeoComment
            {
                Author = geoComment.author,
                Message = geoComment.message,
                Latitude = (int)geoComment.latitude,
                Longitude = (int)geoComment.longitude
            };
            await _dbContext.AddAsync(geo);
            await _dbContext.SaveChangesAsync();
            return geo;
        }

        public async Task<ResultDto> GetId(int id)
        {
            var result = await _dbContext.GeoComments.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
            {
                return null;
            }

            var myNewDto = new ResultDto()
            {
                id = result.Id,
                author = result.Author,
                message = result.Message,
                longitude = result.Longitude,
                latitude = result.Latitude
            };
            return myNewDto;
        }

        public async Task<List<ResultDto>> RangeFilter(double? minLon, double? maxLon, double? minLat, double? maxLat)
        {
            var filter = await _dbContext.GeoComments.Where(x =>
                x.Longitude >= minLon &&
                x.Longitude <= maxLon &&
                x.Latitude >= minLat &&
                x.Latitude <= maxLat).Select(x => new ResultDto
                {
                    author = x.Author,
                    message = x.Message,
                    latitude = x.Latitude,
                    longitude = x.Longitude,
                    id = x.Id
                }).ToListAsync();
            return filter;
        }

        public async Task<bool> ResetDb()
        {
            await _dbContext.Database.EnsureDeletedAsync();
            return await _dbContext.Database.EnsureCreatedAsync();
        }


        public async Task<MyGeoComment> Addv2(MyGeoComment geoComment)
        {
            //var geo = new MyGeoComment()
            //{
            //    Author = geoComment.body.author,
            //    Message = geoComment.body.message,
            //    Title = geoComment.body.title,
            //    Latitude = geoComment.latitude,
            //    Longitude = geoComment.longitude
            //};
            var newcomment = await _dbContext.AddAsync(geoComment);
            await _dbContext.SaveChangesAsync();

            //var savedComment = await _dbContext.GeoComments.FirstOrDefaultAsync(x =>
            //    x.Author == geo.Author &&
            //    x.Message == geo.Message &&
            //    x.Title == geo.Title &&
            //    x.Latitude == geo.Latitude &&
            //    x.Longitude == geo.Longitude);
            //var newcomment = new ResultDtoV2()
            //{
            //    id = savedComment.Id,
            //    longitude = savedComment.Longitude,
            //    latitude = savedComment.Latitude,
            //    body =
            //    {
            //        author = savedComment.Author,
            //        message = savedComment.Message,
            //        title = savedComment.Title
            //    },

            //};
            return newcomment.Entity;
        }
        public async Task<MyGeoComment?> AssignUser(User user)
        {
            return await _dbContext.GeoComments
                .Include(u => u.User)
                .FirstOrDefaultAsync(u => u.User.Id == user.Id);
        }
        public async Task<ResultDtoV2> GetIdv2(int id)
        {
            var result = await _dbContext.GeoComments.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
            {
                return null;
            }

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
            return myNewDto;
        }
        //Made Array for request (Not List<>)
        public async Task<ResultDtoV2[]> RangeFilterv2(double? minLon, double? maxLon, double? minLat, double? maxLat)
        {
            var filter = await _dbContext.GeoComments.Where(x =>
                x.Longitude >= minLon &&
                x.Longitude <= maxLon &&
                x.Latitude >= minLat &&
                x.Latitude <= maxLat).Select(x => new ResultDtoV2
                {
                    body = new ResultBody
                    {
                        author = x.Author,
                        message = x.Message,
                        title = x.Title
                    },
                    latitude = x.Latitude,
                    longitude = x.Longitude,
                    id = x.Id
                }).ToArrayAsync();
            return filter;
        }

        public async Task<User> CreateUser(UserDTO userRegistration)
        {
            var user = new User()
            {
                UserName = userRegistration.UserName
            };

            await _userManager.CreateAsync(user, userRegistration.Password);

            await _userManager.CheckPasswordAsync(user,
                userRegistration.Password);

            await _userManager.FindByNameAsync(user.UserName);

            return user;
        }

        public async Task<MyGeoComment[]> FindByName(string username)
        {
            var result = _dbContext.GeoComments.Include(x => x.User)
                .Where(x => x.User.UserName == username || x.Author == username);
            return await result.ToArrayAsync();
        }

    }
}