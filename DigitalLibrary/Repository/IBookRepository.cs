using DigitalLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DigitalLibrary.Repository
{
    public interface IBookRepository
    {
        Task<int> AddNewBook(BookModel bookModel);
        Task<List<BookModel>> GetAllBooks();
        Task<BookModel> GetBookById(int id);
        Task<List<BookModel>> GetTopBooks(int count);
        List<BookModel> SearchBook(string title, string author);
        string GetAppName();
    }
}