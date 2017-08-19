using Microsoft.AspNetCore.Mvc;
using MyWorld.Models;
using MyWorld.ViewModels;

namespace MyWorld.Controllers.API
{
    [Route("api/trips")]
    public class TripsController : Controller
    {
        private IWorldRepository _repo;

        public TripsController(IWorldRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repo.GetAllTrips());
        }

        [HttpPost]
        public IActionResult Post([FromBody] TripViewModel newTrip)
        {
            if (ModelState.IsValid)
            {

                // TODO: Save to the Database

                return Created($"api/trips/{newTrip.Name}", newTrip);
            }
            return BadRequest(ModelState.ToString());
        }
    }
}