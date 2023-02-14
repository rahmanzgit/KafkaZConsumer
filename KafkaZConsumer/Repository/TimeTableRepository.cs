using KafkaZConsumer.Data;
using KafkaZConsumer.Model;
using KafkaZConsumer.Repository.Contracts;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafkaZConsumer.Repository
{
    public class TimeTableRepository : BaseRepository<TimeTable>
    {
        public TimeTableRepository(IDataBaseContext dataBaseContext) : base(dataBaseContext)
        {
        }
        public int AddTimeTable(TimeTable timeTable)
        {
            var updateQuery = "dbo.AddTimeTable";
            IList<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(DataParameter.GetParameter("key", timeTable.key, DbType.String, ParameterDirection.Input));
            parameters.Add(DataParameter.GetParameter("time", timeTable.value.time, DbType.String, ParameterDirection.Input));
            parameters.Add(DataParameter.GetParameter("value", timeTable.value.value, DbType.Decimal, ParameterDirection.Input));
            parameters.Add(DataParameter.GetParameter("Id", DBNull.Value, DbType.Int32, ParameterDirection.Output));
            return UpdateViaSP(updateQuery, parameters);
        }
    }
}
