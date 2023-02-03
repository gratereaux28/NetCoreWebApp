using System;

#nullable disable

namespace Core.Models
{
    public partial class Users
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; }
        public DateTime Date { get; set; }
    }
}
