using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Implementations;
using NetCoreWebApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Models;

namespace NetCoreWebApp.Web.Controllers
{
    [AllowAnonymous]
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService) : base()
        {
            _userService = userService;
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

                Users user = await _userService.GetUser(model.UserName);

                if (user != null && user.Password == model.Password)
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
                    ModelState.AddModelError(string.Empty, "Usuario o Contraseña inválido");
                    return View("Login", model);
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Usuario o Contraseña inválido");
                return View("Login", model);
            }
        }

        /// <summary>
        /// Cierra la sesión actual del usuario.
        /// </summary>
        /// <returns>Vista principal.</returns>
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
