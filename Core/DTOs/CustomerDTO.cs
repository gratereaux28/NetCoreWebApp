﻿using System;

#nullable disable

namespace Core.DTOs
{
    public partial class CustomerDTO
    {
        public Guid Id { get; set; }
        public string Customer1 { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string ContactManager { get; set; }
        public string ContactNumber { get; set; }
        public bool Status { get; set; }
        public DateTime Date { get; set; }
    }
}