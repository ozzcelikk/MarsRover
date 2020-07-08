using MarsRover.Contracts.Models;

namespace MarsRover.Contracts.Services
{
    public interface IPlateauService
    {
        Plateau CreatePlateau(string coordinateString);
        Plateau CreatePlateau(int x, int y);
    }
}