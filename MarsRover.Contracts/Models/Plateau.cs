namespace MarsRover.Contracts.Models
{
    public class Plateau
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Plateau(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}