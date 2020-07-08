using MarsRover.Contracts.Extensions;
using MarsRover.Contracts.Models;
using MarsRover.Contracts.Services;
using MarsRover.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace MarsRover.Worker
{
    public class Program
    {
        #region Services

        private static IPlateauService _plateauService;
        private static IRoverService _roverService;

        #endregion

        public static int MaxRoverCount => 4;

        public static Plateau CurrentPlateau { get; set; }

        public static List<Rover> RoverList { get; set; }

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

            RoverList = new List<Rover>();

            Console.WriteLine("Create a new plateau");

            var work = true;

            while (work)
            {
                PlateauWorker();

                RoverWorker();

                ExecuteWorker();

                Console.WriteLine("Press any key for exit");

                Console.ReadLine();

                work = false;
            }
        }

        public static void PlateauWorker()
        {
            while (CurrentPlateau == null)
            {
                var plateauCoordinateString = Console.ReadLine();

                CurrentPlateau = _plateauService.CreatePlateau(plateauCoordinateString);

                if (CurrentPlateau == null)
                {
                    Console.WriteLine("Please enter valid coordinate values like this template: \"3 3\"");
                }
            }
        }

        public static void RoverWorker()
        {
            while (RoverList.Count != MaxRoverCount)
            {
                var roverIsReady = false;
                var roverCommandsAreReady = false;

                Console.WriteLine($"Enter {RoverList.Count + 1}.Rover coordinate");

                while (!roverIsReady)
                {
                    var roverCoordinateString = Console.ReadLine();

                    var roverCreateResult = _roverService.CreateRover(roverCoordinateString, CurrentPlateau);

                    var currentRover = roverCreateResult.Rover;

                    if (currentRover == null)
                    {
                        Console.WriteLine(roverCreateResult.Message);
                    }
                    else
                    {
                        roverIsReady = true;

                        Console.WriteLine($"Enter {RoverList.Count + 1}.Rover directives");

                        while (!roverCommandsAreReady)
                        {
                            var roverDirectiveString = Console.ReadLine();

                            var roverCommands = _roverService.GetCommands(roverDirectiveString);

                            if (roverCommands.Length == 0)
                            {
                                Console.WriteLine($"Please enter {RoverList.Count + 1}.Rover valid command values like this template: \"MLRRMLMM\"");
                            }
                            else
                            {
                                roverCommandsAreReady = true;

                                currentRover.Commands = roverCommands;

                                RoverList.Add(currentRover);
                            }
                        }
                    }
                }
            }
        }

        public static void ExecuteWorker()
        {
            var count = 0;

            foreach (var rover in RoverList)
            {
                count++;

                _roverService.ExecuteCommands(rover);

                Console.WriteLine($"{count}. Rover => {rover.Location.X} {rover.Location.Y} {rover.Location.Heading.GetCode()}");
            }
        }
    }
}