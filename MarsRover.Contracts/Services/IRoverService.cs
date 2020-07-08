using MarsRover.Contracts.Enums;
using MarsRover.Contracts.Models;

namespace MarsRover.Contracts.Services
{
    public interface IRoverService
    {
        RoverCreateResult CreateRover(string coordinateString, Plateau plateau);
        Rover CreateRover(Location location, Plateau plateau);
        Rover ExecuteCommands(Rover rover);
        Command[] GetCommands(string commandString);
    }
}