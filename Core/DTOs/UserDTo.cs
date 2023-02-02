using System;

#nullable disable

namespace Core.DTOs
{
    public partial class UserDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; }
        public DateTime Date { get; set; }
    }
}
