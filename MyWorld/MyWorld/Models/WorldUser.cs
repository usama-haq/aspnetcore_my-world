using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;

namespace MyWorld.Models
{
    public class WorldUser : IdentityUser
    {
        public DateTime FirstTrip { get; set; }
    }
}