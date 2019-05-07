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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = appContext.Users.FirstOrDefault(u => u.Login == model.Login && u.Password == model.Password);
                if (user != null)
                {
                    await Authenticate(model.Login); 

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        private async Task Authenticate(string userName)
        {
           
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
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