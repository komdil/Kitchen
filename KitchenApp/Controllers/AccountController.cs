using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KitchenApp.Models;
using KitchenApp.DateProvider;
using Microsoft.AspNetCore.Mvc;
using KitchenApp.ViewsModel;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace KitchenApp.Controllers
{
    public class AccountController : Controller
    {

        private KitchenAppContext appContext;
        public AccountController(KitchenAppContext context)
        {
            appContext = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (IsUserAuhorized)
                return RedirectToHomePage();
            return View();
        }

        bool IsUserAuhorized => this.User.Identity.IsAuthenticated;
        IActionResult RedirectToHomePage() => RedirectToAction("Index", "Home");

        public IActionResult AccessDenied(string url)
        {
            return View(model: url);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (IsUserAuhorized)
                return RedirectToHomePage();
            if (ModelState.IsValid)
            {
                var user = appContext.Users.FirstOrDefault(u => u.Login == model.Login && u.Password == model.Password);
                if (user != null)
                {
                    await Authenticate(user);
                    return RedirectToAction("Index", "Home");
                }
                else
                {

                    ModelState.AddModelError("", "Incorrect login or password");
                    return View(model);
                }
            }
            return View();
            
        }
        
        private async Task Authenticate(User user)
        {

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}