using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafkaZConsumer.Data
{
    public interface IDataBaseContext
    {
        IDbConnection Connection { get; }
    }
}
