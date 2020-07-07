using MarsRover.Contracts.Enums;
using MarsRover.Contracts.Models;

namespace MarsRover.Contracts.Services
{
    public interface IRoverService
    {
        Rover CreateRover(string coordinateString, Plateau plateau);
        Rover CreateRover(int x, int y, Heading heading, Plateau plateau);
        Rover ProcessCommands(Rover rover);
        CommandType[] GetCommandTypes(string commandString);
    }
}