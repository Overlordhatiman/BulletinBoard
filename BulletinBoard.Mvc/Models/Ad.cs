namespace BulletinBoard.Mvc.Models
{
    public class Ad
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }
        public string? UserId { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public int SubcategoryId { get; set; }
        public Subcategory? Subcategory { get; set; }
    }
}