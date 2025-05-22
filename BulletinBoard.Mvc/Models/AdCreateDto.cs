namespace BulletinBoard.Mvc.Models
{
    public class AdCreateDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public int SubcategoryId { get; set; }
    }
}
