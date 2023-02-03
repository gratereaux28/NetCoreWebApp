using System;

#nullable disable

namespace Core.DTOs
{
    public partial class ProductCategoriesDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
