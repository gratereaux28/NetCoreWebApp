using System;
using System.Collections.Generic;

#nullable disable

namespace Core.Models
{
    public partial class Products
    {
        public Guid Id { get; set; }
        public string Sku { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string ShortDesc { get; set; }
        public string LongDesc { get; set; }
        public string Image { get; set; }
        public Guid? CategoryId { get; set; }
        public double? Stock { get; set; }
        public string Location { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public virtual ProductCategories Category { get; set; }
    }
}
