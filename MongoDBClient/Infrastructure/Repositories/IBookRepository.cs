using MongoDBClient.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MongoDBClient.Infrastructure.Repositories
{
    public interface IBookRepository
    {
        Task CreateBookAsync(Book book);
        Task UpdateBookById(string id, Book book);
        Task<Book> GetBookByIdAsync(string id);
        Task DeleteBookByIdAsync(string id);
        Task<IEnumerable<Book>> GetBookListAsync();
        Task<ResultSet<Book>> GetBookListWithPageAsync(int page, int rows);
    }
}
