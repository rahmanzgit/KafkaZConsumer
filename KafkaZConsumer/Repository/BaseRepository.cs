using KafkaZConsumer.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using KafkaZConsumer.Repository.Contracts;

namespace KafkaZConsumer.Repository
{
    public class BaseRepository<T>
    {
        private readonly IDataBaseContext _databaseContext;
        //
        public BaseRepository(IDataBaseContext dataBaseContext)
        {
            _databaseContext = dataBaseContext;
        }
        public int UpdateViaSP(string sql, IList<SqlParameter> parameters)
        {
            var sqlCommand = _databaseContext.Connection.CreateCommand();
            sqlCommand.CommandText = sql;
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Clear();
            //sqlCommand.Parameters.Add(parameters);
            foreach (SqlParameter param in parameters)
            {
                sqlCommand.Parameters.Add(param);
            }
            sqlCommand.ExecuteNonQuery();
            return 1;            
        }
    }
}
