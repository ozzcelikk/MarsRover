using MarsRover.Contracts.Enums;
using MarsRover.Contracts.Models;

namespace MarsRover.Contracts.Services
{
    public interface IHeadingService
    {
        Location Move(Location location);
        Heading TurnRight();
        Heading TurnLeft();
    }
}