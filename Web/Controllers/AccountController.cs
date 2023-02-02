using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetCoreWebApp.Infrastructure.Implementations;
using NetCoreWebApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NetCoreWebApp.Web.Controllers
{
    [AllowAnonymous]
    public class AccountController : BaseController
    {
        public AccountController() : base()
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Login(string ReturnUrl)
        {
            await HttpContext.SignOutAsync();
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        /// <summary>
        /// Loguea un usuario en el sistema.
        /// </summary>
        /// <param name="model">Información del usuario.</param>
        /// <returns>Vista principal.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string? ReturnUrl)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Login", model);
                }

                if (model.UserName == "martin" && model.Password == "123")
                {
                    var ident = new List<Claim>
                    {
                        // adding following 2 claim just for supporting default antiforgery provider
                        new Claim(ClaimTypes.NameIdentifier, "martin"),
                        new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"),
                    };

                    var claimsIdentity = new ClaimsIdentity(ident, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(2440)
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                          new ClaimsPrincipal(claimsIdentity),
                          authProperties);

                    if (string.IsNullOrEmpty(ReturnUrl))
                        return RedirectToAction("Index", "Home");
                    else
                        return Redirect(ReturnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Se ha presentado un error al cargar el Acuerdo de Usuario.");
                    return View("Login", model);
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Usuario o Contraseña inválido");
                return View("Login", model);
            }
        }
    }
}
