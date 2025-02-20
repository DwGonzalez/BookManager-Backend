using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class Author
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("idBook")]
        public int BookId { get; set; }

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; } = string.Empty;

        [JsonPropertyName("lastName")]
        public string LastName { get; set; } = string.Empty;
    }
}
