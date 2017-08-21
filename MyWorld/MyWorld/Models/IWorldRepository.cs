using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyWorld.Models
{
    public interface IWorldRepository
    {
        IEnumerable<Trip> GetAllTrips();

        IEnumerable<Trip> GetAllTripsWithStops();

        IEnumerable<Trip> GetAllUserTripsWithStops(string name);

        Trip GetTripByName(string name, string userName);

        void AddTrip(Trip trip);

        void AddStop(string tripName, string userName, Stop newStop);

        Task<bool> SaveChangesAsync();
    }
}