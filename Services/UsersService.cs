using Microsoft.Extensions.Options;
using MongoDB.Driver;
using qwerty_chat_api.Models;

namespace qwerty_chat_api.Services
{
    public class UsersService
    {
        private readonly IMongoCollection<User> _UsersCollection;

        public UsersService(
            IOptions<ChatDatabaseSettings> ChatDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                ChatDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                ChatDatabaseSettings.Value.DatabaseName);

            _UsersCollection = mongoDatabase.GetCollection<User>(
                ChatDatabaseSettings.Value.UsersCollectionName);
        }

        public async Task<List<User>> GetAsync() =>
            await _UsersCollection.Find(_ => true).ToListAsync();

        public async Task<User?> GetAsync(string id) =>
            await _UsersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<User?> GetAsync(string username, string password) =>
            await _UsersCollection.Find(x => x.Username == username && x.Password == password).FirstOrDefaultAsync();

        public async Task CreateAsync(User newUser) =>
            await _UsersCollection.InsertOneAsync(newUser);

        public async Task UpdateAsync(string id, User updatedUser) =>
            await _UsersCollection.ReplaceOneAsync(x => x.Id == id, updatedUser);

        public async Task RemoveAsync(string id) =>
            await _UsersCollection.DeleteOneAsync(x => x.Id == id);
    }
}
