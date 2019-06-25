using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace MongoDB.Schema
{
    public class User
    {
        public ObjectId Id { get; set; }

        [BsonElement("UserId")]
        public int UserId { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Lastname")]
        public string Lastname { get; set; }

        [BsonElement("Created")]
        public DateTime Created { get; set; }

        [BsonElement("Updated")]
        public DateTime Updated { get; set; }
    }

    public class UserAsDynamic
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public List<dynamic> Blogs { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}