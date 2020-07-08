using MarsRover.Contracts.Enums;
using System.Collections.Generic;

namespace MarsRover.Contracts.Models
{
    public class Rover
    {
        public Location Location { get; set; }
        public Plateau Plateau { get; }

        public Command[] Commands { get; set; }
        public List<RoverHistory> RoverHistory { get; set; }

        public Rover(int x, int y, Heading heading, Plateau plateau)
        {
            Location = new Location(x, y, heading);
            Plateau = plateau;
            RoverHistory = new List<RoverHistory>();
        }

        public Rover(Location location, Plateau plateau)
        {
            Location = location;
            Plateau = plateau;
            RoverHistory = new List<RoverHistory>();
        }
    }
}