using Microsoft.EntityFrameworkCore;
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

        public void AddStop(string tripName, string userName, Stop newStop)
        {
            var trip = GetTripByName(tripName, userName);
            if (trip != null)
            {
                trip.Stops.Add(newStop);
                _context.Stops.Add(newStop);
            }
        }

        public void AddTrip(Trip trip)
        {
            _context.Add(trip);
        }

        public IEnumerable<Trip> GetAllTrips()
        {
            try
            {
                return _context.Trips.OrderBy(t => t.Name).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("-- Exception: Couldn't get trips from the database.", ex);
                return null;
            }
        }

        public IEnumerable<Trip> GetAllTripsWithStops()
        {
            try
            {
                return _context.Trips.Include(t => t.Stops).OrderBy(t => t.Name).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("-- Exception: Couldn't get trips with stops from the database.", ex);
                return null;
            }
        }

        public Trip GetTripByName(string tripName, string userName)
        {
            return _context.Trips.Include(e => e.Stops)
                .Where(t => t.Name.Equals(tripName) && t.UserName.Equals(userName))
                .FirstOrDefault();
        }

        public IEnumerable<Trip> GetAllUserTripsWithStops(string name)
        {
            try
            {
                return _context.Trips.Include(t => t.Stops)
                    .OrderBy(t => t.Name)
                    .Where(t => t.UserName.Equals(name))
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("-- Exception: Couldn't get User trips with stops from database.", ex);
                return null;
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}