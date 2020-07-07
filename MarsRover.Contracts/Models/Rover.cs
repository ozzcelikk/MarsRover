using MarsRover.Contracts.Enums;
using System.Collections.Generic;

namespace MarsRover.Contracts.Models
{
    public class Rover
    {
        public Location Location { get; set; }
        public Heading Heading { get; set; }
        public Plateau Plateau { get; }

        public CommandType[] CommandTypes { get; set; }
        public List<RoverHistory> RoverHistory { get; set; }

        public Rover(int x, int y, Heading heading, Plateau plateau)
        {
            Location = new Location(x, y);
            Heading = heading;
            Plateau = plateau;
            RoverHistory = new List<RoverHistory>();
        }
    }
}