using System.Text.Json.Serialization;

namespace BulletinBoard.Mvc.Models
{
    public class Category
    {
        [JsonPropertyName("categoryId")]
        public int Id { get; set; }

        [JsonPropertyName("categoryName")]
        public string? Name { get; set; }
        public List<Subcategory>? Subcategories { get; set; }
    }
}