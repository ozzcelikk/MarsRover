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
        public Rover CreateRover(string coordinateString, Plateau plateau)
        {
            if (plateau == null)
            {
                return null;
            }

            if (string.IsNullOrEmpty(coordinateString) || string.IsNullOrWhiteSpace(coordinateString))
            {
                return null;
            }

            var separatedValues = coordinateString.Split(' ');

            if (separatedValues.Length != 3)
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

            if (xCoordinate < 0 || xCoordinate > plateau.X)
            {
                return null;
            }

            if (yCoordinate < 0 || yCoordinate > plateau.Y)
            {
                return null;
            }

            var headings =
                Enum.GetValues(typeof(Heading))
                    .Cast<Heading>()
                    .ToArray();

            var heading = headings.Where(x => x.GetCode() == separatedValues[2]).ToArray();

            if (heading.Length != 1)
            {
                return null;
            }

            return CreateRover(xCoordinate, yCoordinate, heading[0], plateau);
        }

        public Rover CreateRover(int x, int y, Heading heading, Plateau plateau)
        {
            return new Rover(x, y, heading, plateau);
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

                        rover.Heading = heading;

                        break;
                    }

                case Command.Right:
                    {
                        var heading = headingService.TurnRight();

                        rover.Heading = heading;

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
            switch (rover.Heading)
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

        #endregion
    }
}