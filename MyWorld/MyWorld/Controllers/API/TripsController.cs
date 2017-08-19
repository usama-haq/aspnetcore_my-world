using Microsoft.AspNetCore.Mvc;
using MyWorld.Models;

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

        [HttpGet("")]
        public IActionResult Get()
        {
            return Ok(_repo.GetAllTrips());
        }

        [HttpPost("")]
        public IActionResult Post([FromBody] Trip newTrip)
        {
            return Ok(newTrip);
        }
    }
}