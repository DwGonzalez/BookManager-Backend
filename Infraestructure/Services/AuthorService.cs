using Application.DTOs.Author;
using Application.Interfaces;
using Domain.Models;
using System.Text;
using System.Text.Json;

namespace Infraestructure.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly HttpClient _httpClient;
        private string _url = "https://fakerestapi.azurewebsites.net/api/v1/Authors";

        public AuthorService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
        {
            var response = await _httpClient.GetAsync(_url);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<Author>>(json) ?? new List<Author>();
        }

        public async Task<Author?> GetAuthorById(int id)
        {
            var response = await _httpClient.GetAsync($"{_url}/{id}");
            if (!response.IsSuccessStatusCode) return null;
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Author>(json);
        }

        public async Task<IEnumerable<Author>> GetAuthorsByBookId(int bookId)
        {
            var response = await _httpClient.GetAsync($"{_url}/authors/books/{bookId}");
            if (!response.IsSuccessStatusCode) return null;
            var json = await response.Content.ReadAsStringAsync();
            //return JsonSerializer.Deserialize<IEnumerable<Author>>(json);
            return JsonSerializer.Deserialize<IEnumerable<Author>>(json) ?? new List<Author>();
        }

        public async Task<Author?> CreateAuthorAsync(AuthorDto author)
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(author), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_url, jsonContent);

            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Author>(json);
        }

        public async Task<Author?> UpdateAuthorAsync(int id, AuthorDto book)
        {
            var jsonContent = new StringContent(JsonSerializer.Serialize(book), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_url}/{id}", jsonContent);

            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Author>(json);
        }

        public async Task<bool> DeleteAuthorAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_url}/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}
