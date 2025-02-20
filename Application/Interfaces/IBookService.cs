using Application.DTOs.Book;
using Domain.Models;

namespace Application.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetBooksAsync();
        Task<Book> GetBookByIdAsync(int id);
        Task<Book> CreateBookAsync(BookDto book);
        Task<Book> UpdateBookAsync(int id, BookDto book);
        Task<bool> DeleteBookAsync(int id);
    }
}
