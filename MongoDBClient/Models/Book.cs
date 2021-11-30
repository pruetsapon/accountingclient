using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MongoDBClient.Models
{
    public class Book
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("BookId")]
        public string BookId { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Created")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime Created { get; set; }
    }
}
