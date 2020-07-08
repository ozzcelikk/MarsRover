using MarsRover.Contracts.Enums;
using MarsRover.Contracts.Extensions;
using MarsRover.Contracts.Models;
using MarsRover.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarsRover.Services
{
    public class RoverService : IRoverService
    {
        public RoverCreateResult CreateRover(string coordinateString, Plateau plateau)
        {
            var roverResult = new RoverCreateResult();

            if (plateau == null)
            {
                roverResult.Message = "Plateau is empty";

                return roverResult;
            }

            var locationCreateResult = CreateRoverLocation(plateau, coordinateString);

            if (locationCreateResult.Location == null)
            {
                roverResult.Message = locationCreateResult.Message;

                return roverResult;
            }

            roverResult.Rover = CreateRover(locationCreateResult.Location, plateau);

            return roverResult;
        }

        public Rover CreateRover(Location location, Plateau plateau)
        {
            return new Rover(location, plateau);
        }

        public Rover ExecuteCommands(Rover rover)
        {
            var count = 0;

            foreach (var command in rover.Commands)
            {
                count++;

                var roverIsInPlateau = CheckRoverIsInPlateau(rover);

                if (!roverIsInPlateau)
                {
                    break;
                }

                Execute(rover, command);

                rover.RoverHistory.Add(new RoverHistory
                {
                    Step = count,
                    Location = rover.Location
                });
            }

            return rover;
        }

        public Command[] GetCommands(string commandString)
        {
            var validCommands =
                Enum.GetValues(typeof(Command))
                    .Cast<Command>()
                    .ToArray();

            var commandCodes = validCommands.Select(x => x.GetCode()).ToArray();

            var separatedCommands = commandString.ToCharArray().Select(x => x.ToString()).ToArray();

            var unsupportedCommands = separatedCommands.Distinct().Except(commandCodes).ToArray();

            if (unsupportedCommands.Length > 0)
            {
                return new Command[0];
            }

            var commands = new List<Command>();

            foreach (var command in separatedCommands)
            {
                var commandEnum = validCommands.First(x => x.GetCode() == command);

                commands.Add(commandEnum);
            }

            return commands.ToArray();
        }

        #region Helper Methods

        private void Execute(Rover rover, Command commandType)
        {
            var headingService = GetHeadingService(rover);

            switch (commandType)
            {
                case Command.Forward:
                    {
                        var location = headingService.Move(rover.Location);

                        rover.Location = location;

                        break;
                    }

                case Command.Left:
                    {
                        var heading = headingService.TurnLeft();

                        rover.Location.Heading = heading;

                        break;
                    }

                case Command.Right:
                    {
                        var heading = headingService.TurnRight();

                        rover.Location.Heading = heading;

                        break;
                    }
            }
        }

        private bool CheckRoverIsInPlateau(Rover rover)
        {
            var maxXCoordinateValue = rover.Plateau.X;

            var maxYCoordinateValue = rover.Plateau.Y;

            var roverIsInPlateau = true;

            if (rover.Location.X > maxXCoordinateValue || rover.Location.Y > maxYCoordinateValue)
            {
                roverIsInPlateau = false;
            }
            else if (rover.Location.X < 0 || rover.Location.Y < 0)
            {
                roverIsInPlateau = false;
            }

            return roverIsInPlateau;
        }

        private IHeadingService GetHeadingService(Rover rover)
        {
            switch (rover.Location.Heading)
            {
                case Heading.North:
                    {
                        return new NorthHeadingService();
                    }

                case Heading.South:
                    {
                        return new SouthHeadingService();
                    }

                case Heading.East:
                    {
                        return new EastHeadingService();
                    }

                case Heading.West:
                    {
                        return new WestHeadingService();
                    }
            }

            return null;
        }

        private LocationCreateResult CreateRoverLocation(Plateau plateau, string coordinateString)
        {
            var locationCreateResult = new LocationCreateResult();

            if (string.IsNullOrEmpty(coordinateString) || string.IsNullOrWhiteSpace(coordinateString))
            {
                locationCreateResult.Message = "Coordinate string must be this format: 1 2 N";

                return locationCreateResult;
            }

            var separatedValues = coordinateString.Split(' ');

            if (separatedValues.Length != 3)
            {
                locationCreateResult.Message = "Coordinate string must be includes x,y and heading value";

                return locationCreateResult;
            }

            if (!int.TryParse(separatedValues[0], out var xCoordinate))
            {
                locationCreateResult.Message = "Coordinate x is not valid";

                return locationCreateResult;
            }

            if (!int.TryParse(separatedValues[1], out var yCoordinate))
            {
                locationCreateResult.Message = "Coordinate y is not valid";

                return locationCreateResult;
            }

            if (xCoordinate < 0 || xCoordinate > plateau.X)
            {
                locationCreateResult.Message = "Coordinate x is out plateau";

                return locationCreateResult;
            }

            if (yCoordinate < 0 || yCoordinate > plateau.Y)
            {
                locationCreateResult.Message = "Coordinate y is out plateau";

                return locationCreateResult;
            }

            var heading = CreateHeading(separatedValues[2]);

            if (heading == null)
            {
                locationCreateResult.Message = "Heading is not valid (N-S-E-W)";

                return locationCreateResult;
            }

            locationCreateResult.Location = new Location(xCoordinate, yCoordinate, heading.Value);

            return locationCreateResult;
        }

        private Heading? CreateHeading(string headingCode)
        {
            var headings =
                Enum.GetValues(typeof(Heading))
                    .Cast<Heading>()
                    .ToArray();

            var heading = headings.FirstOrDefault(x => x.GetCode() == headingCode);

            return heading == Heading.NA ? null : (Heading?)heading;
        }

        #endregion

        private class LocationCreateResult
        {
            public Location Location { get; set; }
            public string Message { get; set; }
        }
    }
}