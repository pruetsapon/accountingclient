using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDB.Schema
{
    public class Blog
    {
        public ObjectId Id { get; set; }

        [BsonElement("BlogId")]
        public int BlogId { get; set; }

        [BsonElement("UserId")]
        public int UserId { get; set; }

        [BsonElement("Title")]
        public string Title { get; set; }

        [BsonElement("Detail")]
        public string Detail { get; set; }

        [BsonElement("Created")]
        public DateTime Created { get; set; }

        [BsonElement("Updated")]
        public DateTime Updated { get; set; }
    }
}