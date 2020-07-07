using MarsRover.Contracts.Services;

namespace MarsRover.Services
{
    public class PlateauService : IPlateauService
    {
        public Contracts.Models.Plateau CreatePlateau(string coordinateString)
        {
            if (string.IsNullOrEmpty(coordinateString) || string.IsNullOrWhiteSpace(coordinateString))
            {
                return null;
            }

            var separatedValues = coordinateString.Split(' ');

            if (separatedValues.Length != 2)
            {
                return null;
            }

            if (!int.TryParse(separatedValues[0], out var xCoordinate))
            {
                return null;
            }

            if (!int.TryParse(separatedValues[1], out var yCoordinate))
            {
                return null;
            }

            return CreatePlateau(xCoordinate, yCoordinate);
        }

        public Contracts.Models.Plateau CreatePlateau(int x, int y)
        {
            return new Contracts.Models.Plateau(x, y);
        }
    }
}