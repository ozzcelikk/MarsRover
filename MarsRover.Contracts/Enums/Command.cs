using MarsRover.Contracts.Attributes;

namespace MarsRover.Contracts.Enums
{
    public enum Command
    {
        [Code("M")]
        Forward = 1,

        [Code("L")]
        Left = 2,

        [Code("R")]
        Right = 3
    }
}