using System.ComponentModel.DataAnnotations;

namespace BulletinBoard.Mvc.Models
{
    public class AdViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Title { get; set; }

        [Required]
        public string? Description { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Display(Name = "Subcategory")]
        public int SubcategoryId { get; set; }

        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }
    }
}