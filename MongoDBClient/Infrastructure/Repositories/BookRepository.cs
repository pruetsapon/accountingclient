using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDBClient.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoDBClient.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookContext _context;

        public BookRepository(IOptions<Settings> settings)
        {
            _context = new BookContext(settings);
        }

        public async Task CreateBookAsync(Book book)
        {
            await _context.Books.InsertOneAsync(book);
        }

        public async Task UpdateBookById(string id, Book job)
        {
            var filterBuilder = Builders<Book>.Filter;
            var filter = filterBuilder.Eq("BookId", id);
            await _context.Books.ReplaceOneAsync(filter, job);
        }

        public async Task<Book> GetBookByIdAsync(string id)
        {
            var filterBuilder = Builders<Book>.Filter;
            var filter = filterBuilder.Eq("BookId", id);
            return await _context.Books.Find(filter).FirstOrDefaultAsync();
        }

        public async Task DeleteBookByIdAsync(string id)
        {
            var filterBuilder = Builders<Book>.Filter;
            var filter = filterBuilder.Eq("BookId", id);
            await _context.Books.DeleteOneAsync(filter);
        }

        public async Task<IEnumerable<Book>> GetBookListAsync()
        {
            return await _context.Books.Find(_ => true).ToListAsync();
        }

        public async Task<ResultSet<Book>> GetBookListWithPageAsync(int page, int rows)
        {
            var filterBuilder = Builders<Book>.Filter;
            var filter = filterBuilder.Empty;
            var jobs = await _context.Books
                                    .Find(filter)
                                    .Skip((page - 1) * rows)
                                    .Limit(rows)
                                    .ToListAsync();
            var jobsCount = await _context.Books
                                     .Find(filter)
                                     .CountDocumentsAsync();
            var resultset = new ResultSet<Book>(page, rows, (int)jobsCount, jobs);
            return resultset;
        }
    }
}
