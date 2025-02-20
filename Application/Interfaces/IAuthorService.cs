using Application.DTOs.Author;
using Domain.Models;

namespace Application.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> GetAllAuthorsAsync();
        Task<Author?> GetAuthorById(int id);
        Task<IEnumerable<Author>> GetAuthorsByBookId(int bookId);
        Task<Author?> CreateAuthorAsync(AuthorDto book);
        Task<Author?> UpdateAuthorAsync(int id, AuthorDto book);
        Task<bool> DeleteAuthorAsync(int id);
    }
}
