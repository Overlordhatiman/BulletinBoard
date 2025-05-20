using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Core.Entities
{
    public class Ad
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public bool Status { get; set; } = true; // Active by default
        public string? UserId { get; set; } // Google Auth ID

        // Category relationships
        public int CategoryId { get; set; }
        public int SubcategoryId { get; set; }

        // Navigation properties (optional)
        public Category? Category { get; set; }
        public Subcategory? Subcategory { get; set; }
    }
}
