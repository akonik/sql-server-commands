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

        public int ExecuteNonQuery(ISqlCommandDefinition command)
        {
            using (SqlCommand sqlCommand = command.GetCommand())
            {
                sqlCommand.Connection = Connection;
                return sqlCommand.ExecuteNonQuery();
            }

        }

        public async Task<int> ExecuteNonQueryAsync(ISqlCommandDefinition command)
        {
            using (SqlCommand sqlCommand = command.GetCommand())
            {
                sqlCommand.Connection = Connection;
                OpenConnection();
                return await sqlCommand.ExecuteNonQueryAsync();
            }
        }

        public object ExecuteScalar(ISqlCommandDefinition command)
        {
            using (SqlCommand sqlCommand = command.GetCommand())
            {
                sqlCommand.Connection = Connection;
                return sqlCommand.ExecuteScalar();
            }
        }

        public async Task<object> ExecuteScalarAsync(ISqlCommandDefinition command)
        {
            using (SqlCommand sqlCommand = command.GetCommand())
            {
                sqlCommand.Connection = Connection;
                return await sqlCommand.ExecuteScalarAsync();
            }
        }

        public void Dispose()
        {
            if(_connection != null)
            {
                _connection.Dispose();
            }
        }

        private SqlConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    _connection = new SqlConnection(_options.ConnectionString);
                }
                OpenConnection();
                return _connection;
            }
        }

        private void OpenConnection()
        {
            if(_connection != null)
            {
                if(_connection.State != System.Data.ConnectionState.Open)
                {
                    _connection.Open();
                }
            }
        }
    }
}
