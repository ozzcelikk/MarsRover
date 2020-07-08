using MarsRover.Contracts.Enums;
using MarsRover.Contracts.Models;
using MarsRover.Contracts.Services;

namespace MarsRover.Services
{
    public class WestHeadingService : IHeadingService
    {
        public Location Move(Location location)
        {
            return new Location(location.X - 1, location.Y);
        }

        public Heading TurnRight()
        {
            return Heading.North;
        }

        public Heading TurnLeft()
        {
            return Heading.South;
        }
    }
}
