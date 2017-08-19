using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyWorld.Models;
using MyWorld.ViewModels;
using System;
using System.Collections.Generic;

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
            try
            {
                var results = _repo.GetAllTrips();
                return Ok(Mapper.Map<IEnumerable<TripViewModel>>(results));
            }
            catch (Exception ex)
            {
                // TODO: Log the exception messgage.

                return BadRequest("> Error Occured.");
                
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] TripViewModel theTrip)
        {
            if (ModelState.IsValid)
            {
                // TODO: Save to the Database

                var newtrip = Mapper.Map<Trip>(theTrip);

                return Created($"api/trips/{newtrip.Id}", Mapper.Map<TripViewModel>(newtrip));
            }
            return BadRequest(ModelState);
        }
    }
}