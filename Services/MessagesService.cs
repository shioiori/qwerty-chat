﻿using Microsoft.Extensions.Options;
using MongoDB.Driver;
using qwerty_chat_api.Models;

namespace qwerty_chat_api.Services
{
    public class MessagesService
    {
        private readonly IMongoCollection<Message> _MessagesCollection;

        public MessagesService(
            IOptions<ChatDatabaseSettings> ChatDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                ChatDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                ChatDatabaseSettings.Value.DatabaseName);

            _MessagesCollection = mongoDatabase.GetCollection<Message>(
                ChatDatabaseSettings.Value.MessagesCollectionName);
        }

        public async Task<List<Message>> GetAsync() =>
            await _MessagesCollection.Find(_ => true).ToListAsync();

        public async Task<Message?> GetAsync(string id) =>
            await _MessagesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Message newMessage) =>
            await _MessagesCollection.InsertOneAsync(newMessage);

        public async Task UpdateAsync(string id, Message updatedMessage) =>
            await _MessagesCollection.ReplaceOneAsync(x => x.Id == id, updatedMessage);

        public async Task RemoveAsync(string id) =>
            await _MessagesCollection.DeleteOneAsync(x => x.Id == id);
    }
}
