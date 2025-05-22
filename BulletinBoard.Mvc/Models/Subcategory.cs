using System.Text.Json.Serialization;

namespace BulletinBoard.Mvc.Models
{
    public class Subcategory
    {
        [JsonPropertyName("subcategoryId")]
        public int Id { get; set; }

        [JsonPropertyName("subcategoryName")]
        public string? Name { get; set; }
        public int CategoryId { get; set; }
    }
}