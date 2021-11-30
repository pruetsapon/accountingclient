using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDBClient.Models;

namespace MongoDBClient.Infrastructure
{
    public class BookContext
    {
        private readonly IMongoDatabase _database = null;

        public BookContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.MongoConnectionString);

            if (client != null)
            {
                _database = client.GetDatabase(settings.Value.MongoDatabase);
            }
        }

        public IMongoCollection<Book> Books
        {
            get
            {
                return _database.GetCollection<Book>("Books");
            }
        }
    }
}
