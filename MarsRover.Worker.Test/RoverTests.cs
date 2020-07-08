using MarsRover.Contracts.Models;
using MarsRover.Contracts.Services;
using MarsRover.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using MarsRover.Contracts.Enums;
using Xunit;

namespace MarsRover.Worker.Test
{
    public class RoverTests
    {
        private static IRoverService _roverService;

        public RoverTests()
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IRoverService, RoverService>()
                .BuildServiceProvider();

            _roverService = serviceProvider.GetService<IRoverService>();
        }

        [Fact]
        public void ShouldCreateRover()
        {
            var plateau = new Plateau(10, 10);

            var coordinateString = "1 2 E";

            var rover = _roverService.CreateRover(coordinateString, plateau);

            Assert.NotNull(rover);
        }

        [Fact]
        public void ShouldNotCreateRover()
        {
            var plateau = new Plateau(10, 10);

            var coordinateString = "12 E";

            var rover = _roverService.CreateRover(coordinateString, plateau);

            Assert.Null(rover);
        }

        [Fact]
        public void ShouldCreateCommandList()
        {
            var commandString = "MMRMLMLM";

            var commandList = _roverService.GetCommands(commandString);

            Assert.NotEmpty(commandList);
        }

        [Fact]
        public void ShouldNotCreateCommandList()
        {
            var commandString = "RRMLLRMKT";

            var commandList = _roverService.GetCommands(commandString);

            Assert.Empty(commandList);
        }

        [Fact]
        public void ShouldExecuteCommandList()
        {
            var plateau = new Plateau(10, 10);

            var coordinateString = "1 2 E";

            var rover = _roverService.CreateRover(coordinateString, plateau);

            var commandString = "MMRMLMLM";

            rover.Commands = _roverService.GetCommands(commandString);

            var executedRover = _roverService.ExecuteCommands(rover);

            Assert.Same(rover, executedRover);
        }

        [Fact]
        public void ShouldNotExecuteCommandList()
        {
            var plateau = new Plateau(10, 10);

            var coordinateString = "1 2 E";

            var rover = _roverService.CreateRover(coordinateString, plateau);

            rover.Commands = null;

            Assert.Throws<NullReferenceException>(() => _roverService.ExecuteCommands(rover));
        }

        [Fact]
        public void ShouldBeNorthRoverHeading()
        {
            var plateau = new Plateau(10, 10);

            var coordinateString = "3 1 E";

            var rover = _roverService.CreateRover(coordinateString, plateau);

            var commandString = "L";

            rover.Commands = _roverService.GetCommands(commandString);

            _roverService.ExecuteCommands(rover);

            Assert.Equal(Heading.North,rover.Heading);
        }

        [Fact]
        public void ShouldBeRoverLocation()
        {
            var plateau = new Plateau(10, 10);

            var coordinateString = "1 2 N";

            var rover = _roverService.CreateRover(coordinateString, plateau);

            var commandString = "LMLMLMLMM";

            rover.Commands = _roverService.GetCommands(commandString);

            _roverService.ExecuteCommands(rover);

            var location = new Location(1, 3);

            Assert.Equal(location.X, rover.Location.X);
            Assert.Equal(location.Y, rover.Location.Y);
        }
    }
}
