using Drivers.Api.Configurations;
using Drivers.Api.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Drivers.Api.Services
{
    public class DriverServices
    {
        private readonly IMongoCollection<Driver> _driversCollection;

        public DriverServices(IOptions<DatabaseSettings> databaseSettings)
        {
            var client = new MongoClient(databaseSettings.Value.ConnectionString);
            var database = client.GetDatabase(databaseSettings.Value.DatabaseName);
            _driversCollection = database.GetCollection<Driver>(databaseSettings.Value.CollectionName);
        }

        public async Task<List<Driver>> GetAsync()
        {
            return await _driversCollection.Find(c => true).ToListAsync();
        }
        public async Task<Driver> GetDriverById(string id)
        {
            return await _driversCollection.FindAsync(new BsonDocument { { "_id", new ObjectId(id) } }).Result.FirstAsync();

        }
        public async Task InsertDriver(Driver drive)
        {
            await _driversCollection.InsertOneAsync(drive);
        }
        public async Task DeleteDriver(string id)
        {
            var filter = Builders<Driver>.Filter.Eq(s => s.Id, id);
            await _driversCollection.DeleteOneAsync(filter);
        }
        public async Task UpdateDriver(Driver drive)
        {
            var filter = Builders<Driver>.Filter.Eq(s => s.Id, drive.Id);
            await _driversCollection.ReplaceOneAsync(filter, drive);
        }
    }
}
