using System.Diagnostics;
using KitchenApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KitchenApp.Controllers
{
    public class HomeController : Controller
    {
        KitchenAppContext appContext;
        public HomeController(KitchenAppContext context)
        {
            appContext = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            if (User.IsInRole(Helper.ADMIN_ROLE))
            {
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                return RedirectToAction("Index", "User");
            }
        }
        //public IActionResult Index() => View();

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}