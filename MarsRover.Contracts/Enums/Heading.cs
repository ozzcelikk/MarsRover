using MarsRover.Contracts.Attributes;

namespace MarsRover.Contracts.Enums
{
    public enum Heading
    {
        [Command("N")]
        North = 1,

        [Command("S")]
        South = 2,

        [Command("E")]
        East = 3,

        [Command("W")]
        West = 4
    }
}