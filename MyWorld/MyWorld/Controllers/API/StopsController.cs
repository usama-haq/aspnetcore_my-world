using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyWorld.Models;
using MyWorld.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWorld.Controllers.API
{
    [Route("/api/trips/{tripName}/Stops")]
    public class StopsController : Controller
    {
        private IWorldRepository _repository;
        private ILogger<StopsController> _logger;

        public StopsController(IWorldRepository repository, ILogger<StopsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get(string tripName)
        {
            try
            {
                var trip = _repository.GetTripByName(tripName);
                return Ok(Mapper.Map<IEnumerable<StopViewModel>>(trip.Stops.OrderBy(s => s.Order).ToList()));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get stops: {ex}");
            }
            return BadRequest("Failed to get Stops.");
        }

        [HttpPost]
        public async Task<IActionResult> Post(string tripName, [FromBody] StopViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newStop = Mapper.Map<Stop>(vm);

                    // TODO: Lookup the Geocodes

                    // Save to the Database
                    _repository.AddStop(tripName, newStop);

                    if (await _repository.SaveChangesAsync())
                    {
                        return Created($"/api/trips/{tripName}/stops/{newStop.Name}", Mapper.Map<StopViewModel>(newStop));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to save stops: {ex}");
            }
            return BadRequest("Failed to save Stops.");
        }
    }
}