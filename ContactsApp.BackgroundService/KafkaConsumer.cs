using Confluent.Kafka;

namespace ContactsApp.BackgroundService
{

    public class KafkaConsumer
    {
        public static IConsumer<Null,string> RaiseConsumer()
        {
            var config = new ConsumerConfig()
            {
                BootstrapServers = "localhost:9092",
                GroupId = "report-consumer-group",
                AutoOffsetReset = AutoOffsetReset.Earliest,
            };

            var consumer = new ConsumerBuilder<Null, string>(config).Build();
            consumer.Subscribe("report-request");
            
            return consumer;
        }
        
        
        
        
    }

}