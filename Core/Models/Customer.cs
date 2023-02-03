using System;

#nullable disable

namespace Core.Models
{
    public partial class Customer
    {
        public Guid Id { get; set; }
        public string customer { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string ContactManager { get; set; }
        public string ContactNumber { get; set; }
        public bool Status { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
    }
}
