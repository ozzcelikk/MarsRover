﻿using MarsRover.Contracts.Enums;
using MarsRover.Contracts.Models;
using MarsRover.Contracts.Services;

namespace MarsRover.Services
{
    public class NorthHeadingService : IHeadingService
    {
        public Location Move(Location location)
        {
            return new Location(location.X, location.Y + 1);
        }

        public Heading TurnRight()
        {
            return Heading.West;
        }

        public Heading TurnLeft()
        {
            return Heading.East;
        }
    }
}