namespace MarsRover.Contracts.Services
{
    public interface IPlateauService
    {
        Models.Plateau CreatePlateau(string coordinateString);
        Models.Plateau CreatePlateau(int x, int y);
    }
}