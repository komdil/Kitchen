using KitchenApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KitchenApp.Controllers
{
    [Authorize(Roles = Helper.ADMIN_ROLE)]
    public class AdminController : Controller
    {
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
