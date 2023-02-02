using Microsoft.AspNet.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace NetCoreWebApp.Web.Models
{
    public class LoginViewModel : IUser
    {
        public LoginViewModel()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; }

        [Required(ErrorMessage = "El campo Usuario es Requerido")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "El campo Contraseña es Requerido")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
