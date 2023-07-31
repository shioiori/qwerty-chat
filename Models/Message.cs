using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace qwerty_chat_api.Models
{
    public class Message
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("text")]
        public string Text { get; set; }
        [BsonElement("file")]
        public string File { get; set; }
        [BsonElement("created_date")]
        public DateTime CreatedDate { get; set; }
        [BsonElement("user")]
        public User User { get; set; }
        [BsonElement("chat")]
        public Chat Chat { get; set; }
    }
}
