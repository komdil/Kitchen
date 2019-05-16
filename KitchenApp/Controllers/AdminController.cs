using KitchenApp.DateProvider;
using KitchenApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KitchenApp.Controllers
{
    [Authorize(Roles = Helper.ADMIN_ROLE)]
    public class AdminController : Controller
    {
        public KitchenAppContext appContext;
        public AdminController(KitchenAppContext appContext)
        {
            this.appContext = appContext;
        }

        public IActionResult Index()
        {

            var menus = appContext.Menus;

            return View(menus);
        }

        public IActionResult Users() => View();
        public IActionResult Orders() => View();
        public IActionResult Payments() => View();
        public IActionResult Menus() => View();
        public IActionResult CreateNewMenu() => View();
        public IActionResult SelectMenuForToday() => View();
        public IActionResult AddUser() => View();
        public IActionResult DeleteUser() => View();
        public IActionResult ChahgeUser() => View();

        public IActionResult CreateNewOrder() => View();
        public IActionResult ChahgeOrder() => View();

        public IActionResult DeleteOrder() => View();
        public IActionResult CreateOrder() => View();
    }
}