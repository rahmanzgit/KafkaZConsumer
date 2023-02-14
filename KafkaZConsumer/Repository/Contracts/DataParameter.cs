using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafkaZConsumer.Repository.Contracts
{
    public class DataParameter { 
 /*       public DbType DbType { get; set; }
        public ParameterDirection Direction { get; set; }

        public bool IsNullable { get; set; }

        public string ParameterName { get; set; }
        public string SourceColumn { get; set; }
        public DataRowVersion SourceVersion { get; set; }
        public object? Value { get; set; }*/

        public static SqlParameter GetParameter(string name, object value, DbType dbType, ParameterDirection parameterDirection)
        {
            var result = new SqlParameter();
            result.ParameterName = name;
            result.Value = value;
            result.DbType = dbType;
            result.Direction = parameterDirection;  
            return result;
        }

    }
}
