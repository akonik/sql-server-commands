using SqlServer.Commands.Extensions;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace SqlServer.Commands.Handlers
{
    public class SqlCommandHandler : IDisposable
    {
        private SqlConnection _connection;
        private readonly SqlCommandHandlerOptions _options;

        public SqlCommandHandler(SqlCommandHandlerOptions options)
        {
            _options = options;
        }

        public void Execute(ISqlCommandDefinition command)
        {
            using (SqlCommand sqlCommand = command.GetCommand())
            {
                sqlCommand.Connection = Connection;
                sqlCommand.ExecuteNonQuery();
            }

        }

        public async Task ExecuteAsync(ISqlCommandDefinition command)
        {
            using (SqlCommand sqlCommand = command.GetCommand())
            {
                sqlCommand.Connection = Connection;
                await sqlCommand.ExecuteNonQueryAsync();
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        private SqlConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    _connection = new SqlConnection(_options.ConnectionString);
                }

                return _connection;
            }
        }
    }
}
