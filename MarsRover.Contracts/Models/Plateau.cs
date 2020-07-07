namespace MarsRover.Contracts.Models
{
    public class Plateau
    {
        public Plateau(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get; set; }
        public double Y { get; set; }
    }
}