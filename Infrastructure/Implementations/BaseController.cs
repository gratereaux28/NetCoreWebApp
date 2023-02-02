using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace Infrastructure.Implementations
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class BaseController : Controller
    {
        public BaseController()
        {

        }

        /// <summary>
        /// Obtiene el identificador del usuario logueado.
        /// </summary>
        public string LoggedUser => GetLoggedUser();

        /// <summary>
        /// Obtiene el codigo del usuario logueado
        /// </summary>
        public string CodigoUsuario => GetUserCodigo();
        public int CodigoEmpleado => GetCodigoEmpleado();
        public int CodigoPuesto => GetCodigoPuesto();
        public int CodigoDivision => GetCodigoDivision();
        public string TipoEmpleado => GetTipoEmpleado();
        public int CodigoRol => GetCodigoRol();

        private string GetLoggedUser()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claims = identity.Claims;

            return claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
        }

        private string GetUserCodigo()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claims = identity.Claims;

            return claims.FirstOrDefault(x => x.Type == "CodigoUsuario").Value;
        }

        private int GetCodigoEmpleado()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claims = identity.Claims;

            return int.Parse(claims.FirstOrDefault(x => x.Type == "CodigoEmpleado").Value);
        }

        private int GetCodigoPuesto()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claims = identity.Claims;

            return int.Parse(claims.FirstOrDefault(x => x.Type == "CodigoPuesto").Value);
        }

        private int GetCodigoDivision()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claims = identity.Claims;

            return int.Parse(claims.FirstOrDefault(x => x.Type == "CodigoDivision").Value);
        }

        private string GetTipoEmpleado()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claims = identity.Claims;

            return claims.FirstOrDefault(x => x.Type == "TipoEmpleado").Value;
        }

        private int GetCodigoRol()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claims = identity.Claims;
            var claim = claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);

            if (claim != null)
                return int.Parse(claim.Value);

            return 0;
        }
    }
}