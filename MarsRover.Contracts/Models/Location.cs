using MarsRover.Contracts.Enums;

namespace MarsRover.Contracts.Models
{
    public class Location
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Heading Heading { get; set; }

        public Location(int x, int y, Heading heading)
        {
            X = x;
            Y = y;
            Heading = heading;
        }
    }
}