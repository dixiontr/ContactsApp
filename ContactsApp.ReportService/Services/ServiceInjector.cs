using Confluent.Kafka;
using ContactsApp.ReportService.Entities;
using ContactsApp.ReportService.Settings;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace ContactsApp.ReportService.Services
{
    public static class ServiceInjector
    {
        public static IServiceCollection AddMongoDB(this IServiceCollection serviceCollection)
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new DateTimeSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new EnumSerializer<ReportStatus>(BsonType.String));

            serviceCollection.AddSingleton(serviceProvider =>
            {
                var configuration = serviceProvider.GetService<IConfiguration>();
                var connectionString = configuration.GetConnectionString("MongoDB");
                var databaseName = configuration.GetConnectionString("MongoDbDatabaseName");
                var mongoClient = new MongoClient($"{connectionString}/{databaseName}");
                return mongoClient.GetDatabase(databaseName);
            });
            return serviceCollection;
        }

        public static IServiceCollection AddKafkaServices(this IServiceCollection serviceCollection, KafkaSetting kafkaSettings)
        {
            serviceCollection.AddTransient<IProducer<Null, string>>(serviceProvider =>
            {
                kafkaSettings = serviceProvider.GetService<IConfiguration>().GetSection(nameof(KafkaSetting)).Get<KafkaSetting>();
                
                var config = new ProducerConfig()
                {
                    BootstrapServers = kafkaSettings.BoostrapServers,
                };
                return new ProducerBuilder<Null, string>(config).Build();
            });
            serviceCollection.AddTransient<IConsumer<Null, string>>(serviceProvider =>
            {
                var config = new ConsumerConfig()
                {
                    BootstrapServers = kafkaSettings.BoostrapServers,
                    GroupId = kafkaSettings.GroupId,
                    AutoOffsetReset = kafkaSettings.AutoOffsetReset
                };
                var consumer = new ConsumerBuilder<Null, string>(config).Build();
                consumer.Subscribe(kafkaSettings.Topic);
                return consumer;
            });

            KafkaTopic.Topic = kafkaSettings.Topic;

            return serviceCollection;
        }
    }
}