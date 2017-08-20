using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyWorld.Models;
using Microsoft.Extensions.Logging;
using MyWorld.ViewModels;
using AutoMapper;

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
                return Ok(Mapper.Map<IEnumerable<StopViewModel>>(trip.Stops.OrderBy(s=>s.Order).ToList()));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get stops: {ex}");
            }
            return BadRequest("Failed to get Stops.");
        }

        [HttpPost]
        public IActionResult Post()
        {
            return Ok();
        }
    }
}
