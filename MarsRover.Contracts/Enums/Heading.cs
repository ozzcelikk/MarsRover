using MarsRover.Contracts.Attributes;

namespace MarsRover.Contracts.Enums
{
    public enum Heading
    {
        [Code("N/A")]
        NA = 0,

        [Code("N")]
        North = 1,

        [Code("S")]
        South = 2,

        [Code("E")]
        East = 3,

        [Code("W")]
        West = 4
    }
}