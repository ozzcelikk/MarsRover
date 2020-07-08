using MarsRover.Contracts.Enums;
using MarsRover.Contracts.Models;

namespace MarsRover.Contracts.Services
{
    public interface IRoverService
    {
        Rover CreateRover(string coordinateString, Plateau plateau);
        Rover CreateRover(int x, int y, Heading heading, Plateau plateau);
        Rover ExecuteCommands(Rover rover);
        Command[] GetCommands(string commandString);
    }
}