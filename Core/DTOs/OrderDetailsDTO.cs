using System;
using System.Collections.Generic;

#nullable disable

namespace Core.DTOs
{
    public partial class OrderDetailsDTO
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
