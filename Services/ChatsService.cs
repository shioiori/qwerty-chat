using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using qwerty_chat_api.Models;

namespace qwerty_chat_api.Services
{
    public class ChatsService
    {
        private readonly IMongoCollection<Chat> _ChatsCollection;

        public ChatsService(
            IOptions<ChatDatabaseSettings> ChatDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                ChatDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                ChatDatabaseSettings.Value.DatabaseName);

            _ChatsCollection = mongoDatabase.GetCollection<Chat>(
                ChatDatabaseSettings.Value.ChatsCollectionName);
        }

        public async Task<List<Chat>> GetAsync() =>
            await _ChatsCollection.Find(_ => true).ToListAsync();

        public async Task<Chat?> GetAsync(string id) =>
            await _ChatsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<List<Chat>> GetByUserIdAsync(string id) =>
            await _ChatsCollection.Find(x => x.Id == id).ToListAsync();

        public async Task CreateAsync(Chat newChat) =>
            await _ChatsCollection.InsertOneAsync(newChat);

        public async Task UpdateAsync(string id, Chat updatedChat) =>
            await _ChatsCollection.ReplaceOneAsync(x => x.Id == id, updatedChat);

        public async Task RemoveAsync(string id) =>
            await _ChatsCollection.DeleteOneAsync(x => x.Id == id);

    }
}
