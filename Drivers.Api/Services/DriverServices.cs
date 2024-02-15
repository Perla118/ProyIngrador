using Drivers.Api.Configurations;
using Drivers.Api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

public class DriverServices
{
    private readonly IMongoCollection<Driver> _driversCollection;

    public DriverServices(
        IOptions<DatabaseSettings> databaseSettings)
    {
        //Inicializamos mi cliente de mongo
        var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
        //Conectamos a la BD de mongo
        var mongoDB = 
        mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
        _driversCollection = 
        mongoDB.GetCollection<Driver>
        (databaseSettings.Value.CollectionName);
    }

    public async Task<List<Driver>> GetAsync() =>
        await _driversCollection.Find(_ => true).ToListAsync();
}
