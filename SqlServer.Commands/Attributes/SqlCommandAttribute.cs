using System;
using System.Data;

namespace SqlServer.Commands.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SqlCommandAttribute : Attribute
    {
        public string Command { get; set; }
        public CommandType Type { get; set; } = CommandType.Text;

        public SqlCommandAttribute(string command)
        {
            Command = command;
        }

        public SqlCommandAttribute(string command,CommandType commandType)
        {
            Command = command;
            Type = commandType;
        }
    }
}
