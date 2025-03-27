using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;

namespace SubscriptionManager.IntegrationTests.Fixtures;

public class MongoDbFixture : IDisposable
{
    public IMongoDatabase Database { get; }
    private readonly MongoClient _client;
    private readonly string? _databaseName;

    public MongoDbFixture()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true)
            .AddUserSecrets<MongoDbFixture>(optional: true)
            .Build();

        var connectionString = "mongodb://localhost:27017";
        _databaseName = "SubscriptionManagerTest";
        //_databaseName = $"TestDB_{Guid.NewGuid()}";


        var clientSettings = MongoClientSettings.FromConnectionString(connectionString);
        clientSettings.ServerApi = new ServerApi(ServerApiVersion.V1);

        _client = new MongoClient(clientSettings);
        Database = _client.GetDatabase(_databaseName);


        Console.WriteLine($"Database used in test: {Database.DatabaseNamespace.DatabaseName}");
    }

    public void Dispose()
    {
        try
        {
          //_client.DropDatabase(_databaseName);
        }
        catch
        {
            // Ignorer les erreurs de nettoyage
        }
    }
}