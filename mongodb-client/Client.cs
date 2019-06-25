using System;
using System.Linq;
using System.Dynamic;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Schema;

namespace MongoDB.Client
{
    public class MongoDBClient : IMongoDBClient
    {
        private readonly IMongoDatabase _database;

        public MongoDBClient(string dbPath, string dbName)
        {
            var client = new MongoClient(dbPath);
            _database = client.GetDatabase(dbName);
        }

        public List<dynamic> GetUser()
        {
            return _database.GetCollection<dynamic>("users").Find(_ => true).ToListAsync().Result;
        }

        public void CreateUser(User user)
        {
            _database.GetCollection<User>("users").InsertOne(user);
        }

        public void DeleteUser(int id)
        {
            _database.GetCollection<User>("users").DeleteOne(s => s.UserId == id);
        }

        public dynamic GetUserById(int id, bool includeBlog)
        {
            var userCollection = _database.GetCollection<dynamic>("users");
            var blogCollection = _database.GetCollection<dynamic>("blogs");
            var query = Builders<dynamic>.Filter.Eq("UserId", id);
            if(includeBlog)
            {
                var result = from u in userCollection.Find(query).ToListAsync().Result
                join b in blogCollection.AsQueryable() on u.UserId equals b.UserId into blogs
                select new UserAsDynamic() {
                    UserId = u.UserId,
                    Name = u.Name,
                    Lastname = u.Lastname,
                    Blogs = blogs.ToList(),
                    Created = u.Created,
                    Updated = u.Updated
                };
                return result;
            }
            else
            {
                var result = userCollection.Find(query);
                return result;
            }
        }

        public void CreateBlog(Blog blog)
        {
            _database.GetCollection<Blog>("blogs").InsertOne(blog);
        }

        public List<dynamic> GetBlog()
        {
            return _database.GetCollection<dynamic>("blogs").Find(_ => true).ToListAsync().Result;
        }

        public dynamic GetBlog(int id)
        {
            var query = Builders<dynamic>.Filter.Eq("BlogId", id);
            var result = _database.GetCollection<dynamic>("blogs").Find(query);
            if(result.Any())
            {
                return result.SingleAsync().Result;
            }
            return null;
        }
    }

    public interface IMongoDBClient
    {
        List<dynamic> GetUser();
        void CreateUser(User user);
        dynamic GetUserById(int id, bool includeBlog);
        void DeleteUser(int id);
        List<dynamic> GetBlog();
        dynamic GetBlog(int id);
        void CreateBlog(Blog blog);
    }
}