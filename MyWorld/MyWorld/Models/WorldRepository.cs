using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWorld.Models
{
    public class WorldRepository : IWorldRepository
    {
        private WorldContext _context;
        private ILogger<WorldRepository> _logger;

        public WorldRepository(WorldContext context, ILogger<WorldRepository> logger)
        {
            _context = context;
            _logger = logger;
        }


        public IEnumerable<Trip> GetAllTrips()
        {
            _logger.LogInformation("> World Respository Information: Getting All Trips from database.");
            return _context.Trips.ToList();
        }
    }
}
