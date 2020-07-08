using System;

namespace MarsRover.Contracts.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class CodeAttribute : Attribute
    {
        public string Code { get; }

        public CodeAttribute(string code)
        {
            Code = code;
        }
    }
}