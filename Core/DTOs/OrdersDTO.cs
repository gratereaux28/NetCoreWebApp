using Core.Models;
using System;
using System.Collections.Generic;

#nullable disable

namespace Core.DTOs
{
    public partial class OrdersDTO
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string ShipAddress { get; set; }
        public string ShipAddress2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public DateTime Date { get; set; }
        public bool Shipped { get; set; }

        public string stringDetalle { get; set; }
        public virtual ICollection<OrderDetailsDTO> OrderDetails { get; set; }
    }
}
