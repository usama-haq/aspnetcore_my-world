using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyWorld.Models;
using MyWorld.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyWorld.Controllers.API
{
    [Route("api/trips")]
    [Authorize]
    public class TripsController : Controller
    {
        private IWorldRepository _repo;
        private ILogger<TripsController> _logger;

        public TripsController(IWorldRepository repo, ILogger<TripsController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<Trip> results = _repo.GetAllUserTrips(User.Identity.Name);
                return Ok(Mapper.Map<IEnumerable<TripViewModel>>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"-- Exception in Get() of TripsController: {ex}");
                return BadRequest("> Error Occured.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TripViewModel theTrip)
        {
            if (ModelState.IsValid)
            {
                var newtrip = Mapper.Map<Trip>(theTrip);
                newtrip.UserName = User.Identity.Name;
                _repo.AddTrip(newtrip);

                if (await _repo.SaveChangesAsync())
                {
                    return Created($"api/trips/{theTrip.Name}", Mapper.Map<TripViewModel>(newtrip));
                }
            }
            return BadRequest("Failed to Save the trip");
        }
    }
}