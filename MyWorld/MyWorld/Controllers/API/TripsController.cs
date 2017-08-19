using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyWorld.Models;

namespace MyWorld.Controllers.API
{
    public class TripsController : Controller
    {
        private IWorldRepository _repo;

        public TripsController(IWorldRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("api/trips")]
        public IActionResult Get()
        {
            return Ok(_repo.GetAllTrips());
        }
    }
}
