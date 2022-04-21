using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeoComment.Data;

namespace GeoComment.Services
{
    public class GeoService
    {
        private readonly GeoDbContext _dbContex;

        public GeoService(GeoDbContext dbContex)
        {
            _dbContex = dbContex;
        }
    }
}
