using KafkaZConsumer.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KafkaZConsumer.Model
{
    public class TimeTable
    {
        public string key { get; set; }
        public Time value { get; set; }
        public void Populate(string objvalue) {
            var objTc =  JsonConvert.DeserializeObject<TimeTable>(objvalue);
            this.key = objTc.key;
            this.value = objTc.value;
        }
    }
}
