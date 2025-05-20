using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulletinBoard.Core.Entities
{
    public class Subcategory
    {
        public int SubcategoryId { get; set; }
        public string? SubcategoryName { get; set; }
        public int CategoryId { get; set; }
    }
}
