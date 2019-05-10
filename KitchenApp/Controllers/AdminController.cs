using KitchenApp.DateProvider;
using KitchenApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KitchenApp.Controllers
{
    [Authorize(Roles = Helper.ADMIN_ROLE)]
    public class AdminController : Controller
    {
        KitchenAppContext appContext;
        public AdminController(KitchenAppContext appContext)
        {
            this.appContext = appContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Users()
        {
            return View();
        }
        public IActionResult Payments()
        {
            return View();
        }

        public IActionResult Orders()
        {
            return View();
        }
        public IActionResult Menus()
        {
            return View();
        }

        public IActionResult Info()
        {
            return View();
        }









    }
}
