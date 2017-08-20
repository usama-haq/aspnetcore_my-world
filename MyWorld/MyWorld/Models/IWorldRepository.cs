using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyWorld.Models
{
    public interface IWorldRepository
    {
        IEnumerable<Trip> GetAllTrips();
        Trip GetTripByName(string name);

        void AddTrip(Trip trip);

        Task<bool> SaveChangesAsync();


    }
}