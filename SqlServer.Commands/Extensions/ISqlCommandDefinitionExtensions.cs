using SqlServer.Commands.Attributes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace SqlServer.Commands.Extensions
{
    public static class ISqlCommandDefinitionExtensions
    {
        public static IEnumerable<SqlParameter> GetParameters(this ISqlCommandDefinition definition)
        {
            var propertiesAndAttibutes = definition.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(x => Attribute.IsDefined(x, typeof(SqlParameterAttribute)))
                .Select(x => new
                {
                    property = x,
                    attribure = x.GetCustomAttributes(typeof(SqlParameterAttribute), false)
                                            .FirstOrDefault() as SqlParameterAttribute
                });

            //List<SqlParameter> resultParameters = new List<SqlParameter>();

            foreach (var attr in propertiesAndAttibutes)
            {
                var propertyValue = attr.property.GetValue(definition);

                if (attr.attribure.IsRequired)
                {
                    if (propertyValue is string)
                    {
                        if (string.IsNullOrEmpty(propertyValue as string))
                        {
                            ThrowException(attr.property.Name);
                        }
                    }

                    if (propertyValue == null)
                    {
                        ThrowException(attr.property.Name);
                    }
                }

                SqlParameter sqlParameter = null;

                yield return sqlParameter;

                //resultParameters.Add(sqlParameter);
            }

            //return resultParameters;

        }

        public static SqlCommand GetCommand(this ISqlCommandDefinition definition)
        {
            var attribute = definition.GetType()
                .GetCustomAttributes(typeof(SqlCommandAttribute), false).FirstOrDefault() as SqlCommandAttribute;

            
            if (attribute != null)
            {
                SqlCommand command = new SqlCommand();
                command.CommandType = attribute.Type;
                command.CommandText = attribute.CommandText;

                command.Parameters.AddRange(definition.GetParameters().ToArray());

                return command;
            }

            throw new ArgumentNullException("ISqlCommandDefinitionExtensions. Name of stored procedure cannot be null.");

        }

        private static void ThrowException(string parameterName)
        {

            string error = $"ISqlCommandDefinitionExtensions. Required cannot be null or empty. Parameter Name = {parameterName}";

            throw new ArgumentNullException(error);
        }
    }
}
