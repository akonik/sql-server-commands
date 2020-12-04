using System;
using System.Data;

namespace SqlServer.Commands.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SqlCommandAttribute : Attribute
    {
        public string CommandText { get; set; }

        public CommandType Type { get; set; } = CommandType.Text;

        public SqlCommandAttribute(string command)
        {
            CommandText = command;
        }

        public SqlCommandAttribute(string command, CommandType commandType)
        {
            CommandText = command;
            Type = commandType;
        }
    }
}
