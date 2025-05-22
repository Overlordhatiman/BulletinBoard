namespace BulletinBoard.Mvc.Models
{
    public class AdUpdateDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int CategoryId { get; set; }
        public int SubcategoryId { get; set; }
    }
}