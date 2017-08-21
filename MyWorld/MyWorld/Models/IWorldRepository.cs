using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyWorld.Models
{
    public interface IWorldRepository
    {
        IEnumerable<Trip> GetAllTrips();

        IEnumerable<Trip> GetAllTripsWithStops();

        IEnumerable<Trip> GetAllUserTripsWithStops(string name);

        Trip GetTripByName(string name);

        void AddTrip(Trip trip);

        void AddStop(string tripName, Stop newStop);

        Task<bool> SaveChangesAsync();
    }
}