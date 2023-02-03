using System;
using System.Collections.Generic;

#nullable disable

namespace Core.Models
{
    public partial class ProductCategories
    {
        public ProductCategories()
        {
            Products = new HashSet<Products>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? ModifiedAt { get; set; }

        public virtual ICollection<Products> Products { get; set; }
    }
}
