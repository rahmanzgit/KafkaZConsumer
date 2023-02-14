using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafkaZConsumer.Data
{
    public class DatabaseContext : IDataBaseContext
    {
        private readonly IDbConnection _connection;
        public DatabaseContext(string connectionString)
        { 
            _connection = new SqlConnection(connectionString);
            _connection.Open();
        }
        public IDbConnection Connection => _connection;
        public void Dispose()
        { 
            _connection.Close();
        }
    }
}
