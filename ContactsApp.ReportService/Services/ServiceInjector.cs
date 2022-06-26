using ContactsApp.ReportService.Entities;
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

            serviceCollection.AddSingleton(ServiceProvider =>
            {
                var configuration = ServiceProvider.GetService<IConfiguration>();
                var connectionString = configuration.GetConnectionString("MongoDB");
                var databaseName = configuration.GetConnectionString("MongoDbDatabaseName");
                var mongoClient = new MongoClient($"{connectionString}/{databaseName}");
                return mongoClient.GetDatabase(databaseName);
            });
            return serviceCollection;
        }
    }
}