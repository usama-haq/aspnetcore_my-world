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
        [HttpGet("api/trips")]
        public IActionResult Get()
        {
            return Ok(new Trip() { Name = "My Trip" });
        }
    }
}
