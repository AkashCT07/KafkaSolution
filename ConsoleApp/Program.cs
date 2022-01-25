using Confluent.Kafka;
using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConsumerConfig()
            {
                GroupId = "gid-consumers",
                BootstrapServers = "localhost:9092"
            };
            using (var consumer = new ConsumerBuilder<Null, string>(config).Build())
            {
                consumer.Subscribe("topic-temp");
                while (true)
                {
                    var cr = consumer.Consume();
                    Console.WriteLine(cr.Message.Value);
                }
            }
        }
    }
}
