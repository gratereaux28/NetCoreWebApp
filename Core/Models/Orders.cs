using System;
using System.Collections.Generic;

#nullable disable

namespace Core.Models
{
    public partial class Orders
    {
        public Orders()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }

        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipAddress2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public DateTime Date { get; set; }
        public bool Shipped { get; set; }

        public virtual Customers Customer { get; set; }
        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
