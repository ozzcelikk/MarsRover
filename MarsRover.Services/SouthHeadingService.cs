using MarsRover.Contracts.Enums;
using MarsRover.Contracts.Models;
using MarsRover.Contracts.Services;

namespace MarsRover.Services
{
    public class SouthHeadingService : IHeadingService
    {
        public Location Move(Location location)
        {
            return new Location(location.X, location.Y - 1);
        }

        public Heading TurnRight()
        {
            return Heading.East;
        }

        public Heading TurnLeft()
        {
            return Heading.West;
        }
    }
}
