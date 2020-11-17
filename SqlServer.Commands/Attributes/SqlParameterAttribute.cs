using System;
using System.Data;

namespace SqlServer.Commands.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class SqlParameterAttribute : Attribute
    {
        public string Name { get; set; }

        public SqlDbType Type { get; set; }

        public ParameterDirection Direction { get; set; }
        
        public bool IsRequired { get; set; }

        public SqlParameterAttribute(string name)
        {
            Name = name;
        }
    }
}
