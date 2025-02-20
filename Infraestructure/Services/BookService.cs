using Application.DTOs.Book;
using Application.Interfaces;
using Domain.Models;
using System.Text;
using System.Text.Json;

namespace Infraestructure.Services
{
    public class BookService : IBookService
    {
        private readonly HttpClient _httpClient;
        private string _url = "https://fakerestapi.azurewebsites.net/api/v1/Books";

        public BookService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            var response = await _httpClient.GetAsync(_url);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<Book>>(json) ?? new List<Book>();
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_url}/{id}");
            if (!response.IsSuccessStatusCode) return null;
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Book>(json);
        }

        public async Task<Book?> CreateBookAsync(BookDto book)
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(book), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_url, jsonContent);

            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Book>(json);
        }

        public async Task<Book?> UpdateBookAsync(int id, BookDto book)
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(book), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_url}/{id}", jsonContent);

            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Book>(json);
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_url}/{id}");

            return response.IsSuccessStatusCode;
        }


    }
}
