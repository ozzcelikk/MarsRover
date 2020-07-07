using MarsRover.Contracts.Models;
using MarsRover.Contracts.Services;
using MarsRover.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using MarsRover.Contracts.Extensions;

namespace MarsRover.Worker
{
    public class Program
    {
        #region Processor

        private static IPlateauService _plateauService;
        private static IRoverService _roverService;

        #endregion

        public static int RoverCount => 2;

        static void Main(string[] args)
        {
            #region DI

            var serviceProvider = new ServiceCollection()
                    .AddSingleton<IPlateauService, PlateauService>()
                    .AddSingleton<IRoverService, RoverService>()
                    .BuildServiceProvider();

            _plateauService = serviceProvider.GetService<IPlateauService>();
            _roverService = serviceProvider.GetService<IRoverService>(); 

            #endregion

            Console.WriteLine("Create a new plateau");

            Plateau plateau = null;

            var rovers = new List<Rover>();

            var work = true;

            while (work)
            {
                #region Plateau

                while (plateau == null)
                {
                    var plateauCoordinateString = Console.ReadLine();

                    plateau = _plateauService.CreatePlateau(plateauCoordinateString);

                    if (plateau == null)
                    {
                        Console.WriteLine("Please enter valid values like \"3 3\"");
                    }
                }

                #endregion

                #region Rover

                while (rovers.Count != RoverCount)
                {
                    var currentRoverIsValid = false;
                    var currentCommandsAreValid = false;

                    Console.WriteLine($"Enter {rovers.Count + 1}.Rover coordinate");

                    while (!currentRoverIsValid)
                    {
                        var roverCoordinateString = Console.ReadLine();

                        var currentRover = _roverService.CreateRover(roverCoordinateString, plateau);

                        if (currentRover == null)
                        {
                            Console.WriteLine($"Please enter {rovers.Count + 1}.Rover valid values like \"1 2 N\"");
                        }
                        else
                        {
                            currentRoverIsValid = true;

                            Console.WriteLine($"Enter {rovers.Count + 1}.Rover directives");

                            while (!currentCommandsAreValid)
                            {
                                var roverDirectiveString = Console.ReadLine();

                                var commandTypes = _roverService.GetCommandTypes(roverDirectiveString);

                                if (commandTypes.Length == 0)
                                {
                                    Console.WriteLine($"Please enter {rovers.Count + 1}.Rover valid values like \"MLRRMLMM\"");
                                }
                                else
                                {
                                    currentCommandsAreValid = true;

                                    currentRover.CommandTypes = commandTypes;

                                    rovers.Add(currentRover);
                                }
                            }
                        }
                    }
                }

                #endregion

                var count = 0;

                foreach (var rover in rovers)
                {
                    count++;

                    _roverService.ProcessCommands(rover);

                    Console.WriteLine($"{count}. Rover => {rover.Location.X} {rover.Location.Y} {rover.Heading.GetCode()}");
                }

                Console.WriteLine("Press any key for exit");

                Console.ReadLine();

                work = false;
            }
        }
    }
}