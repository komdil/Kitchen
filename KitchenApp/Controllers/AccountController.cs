using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KitchenApp.Models;
using Microsoft.AspNetCore.Mvc;
using KitchenApp.ViewsModel;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace KitchenApp.Controllers
{
    public class AccountController : Controller
    {
        #region Helpers

        bool IsUserAuhorized => User.Identity.IsAuthenticated;
        IActionResult RedirectToHomePage() => RedirectToAction("Index", "Home");

        #endregion

        KitchenAppContext appContext;
        public AccountController(KitchenAppContext context)
        {
            appContext = context;
        }

        #region Actions

        [HttpGet]
        public IActionResult Login()
        {
            if (IsUserAuhorized)
                return RedirectToHomePage();
            else
            {
                ModelState.AddModelError("", "Incorrect Password");
            }
            return View();
        }

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

                var user = appContext.GetEntities<User>().FirstOrDefault(u => u.Login == model.Login && u.Password == Helper.HashPassword(model.Password, u.Salt));
                if (user != null)
                {
                    await Authenticate(user);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    model.Login = "Incorrect login or password";
                    return View("Login", model);
                }


            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        #endregion

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
    }
}