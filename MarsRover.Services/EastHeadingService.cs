using MarsRover.Contracts.Enums;
using MarsRover.Contracts.Models;
using MarsRover.Contracts.Services;

namespace MarsRover.Services
{
    public class EastHeadingService : IHeadingService
    {
        public Location Move(Location location)
        {
            return new Location(location.X + 1, location.Y, location.Heading);
        }

        public Heading TurnRight()
        {
            return Heading.South;
        }

        public Heading TurnLeft()
        {
            return Heading.North;
        }
    }
}
