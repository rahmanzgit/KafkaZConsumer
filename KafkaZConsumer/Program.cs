using Confluent.Kafka;
using KafkaZConsumer.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using Microsoft.Extensions.Configuration;
using KafkaZConsumer.Data;
using Newtonsoft.Json;
using KafkaZConsumer.Model;
using Microsoft.Identity.Client;

namespace KafkaZConsumer
{
    public class Program
    {
        public static void Run_Consume(string brokerList, string topic, CancellationToken cancellationToken, string connectionString)
        {
            var dbContext = new DatabaseContext(connectionString);
            TimeTableRepository timeTableRepository = new TimeTableRepository(dbContext);
            //
            var config = new ConsumerConfig
            {
                BootstrapServers = brokerList,
                GroupId = new Guid().ToString(),
                EnableAutoOffsetStore = false,
                EnableAutoCommit = true,
                StatisticsIntervalMs = 5000,
                SessionTimeoutMs = 6000,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnablePartitionEof = true,                
            };

            using (var consumer = new ConsumerBuilder<Ignore, string>(config)                
                .Build())
            {
                consumer.Subscribe(topic);

                try
                {
                    while (true)
                    {
                        try
                        {
                            var consumeResult = consumer.Consume(cancellationToken);

                            if (consumeResult.IsPartitionEOF)
                            {                               
                                continue;
                            }

                            Console.WriteLine($"Received message at {consumeResult.TopicPartitionOffset}: {consumeResult.Message.Value}");
                            var timeTable =  JsonConvert.DeserializeObject<TimeTable>(consumeResult.Message.Value);
                            if (timeTable != null)
                            { 
                                timeTableRepository.AddTimeTable(timeTable);
                            }
                        }
                        catch (ConsumeException e)
                        {
                            Console.WriteLine($"Consume error: {e.Error.Reason}");
                        }
                    }
                }
                catch (OperationCanceledException ocx)
                {
                    Console.WriteLine("Closing consumer. Error: " + ocx.ToString());
                    consumer.Close();
                }
            }
        }
        

        public static void Main(string[] args)
        {
            /*            var brokerList = "localhost:9092";                                              
                        var topic = "ztesttopic2";
            */
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false);

            IConfiguration appConfig = builder.Build();

            string brokerList = appConfig.GetConnectionString("Broker");//"localhost:9092"; 
            string topic = appConfig.GetConnectionString("Topic");//"ztesttopic2";
            string connectionString = appConfig.GetConnectionString("TTConnection");

            Console.WriteLine($"Started consumer, Ctrl-C to stop consuming");

            CancellationTokenSource cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true; 
                cts.Cancel();
            };
            Run_Consume(brokerList, topic, cts.Token, connectionString);
 
        }
    }
}
