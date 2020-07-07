using MarsRover.Contracts.Attributes;

namespace MarsRover.Contracts.Enums
{
    public enum CommandType
    {
        [Command("M")]
        Forward = 1,

        [Command("L")]
        Left = 2,

        [Command("R")]
        Right = 3
    }
}