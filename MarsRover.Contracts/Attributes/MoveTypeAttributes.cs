using System;

namespace MarsRover.Contracts.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class CommandAttribute : Attribute
    {
        public string Code { get; }

        public CommandAttribute(string code)
        {
            Code = code;
        }
    }
}