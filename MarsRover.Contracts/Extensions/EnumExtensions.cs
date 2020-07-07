using MarsRover.Contracts.Attributes;
using System;
using System.Linq;
using System.Reflection;

namespace MarsRover.Contracts.Extensions
{
    public static class EnumExtensions
    {
        public static string GetCode(this Enum @enum)
        {
            var attribute = @enum.GetType()
                .GetMember(@enum.ToString())
                .First()
                .GetCustomAttribute<CommandAttribute>();

            return attribute.Code;
        }
    }
}
