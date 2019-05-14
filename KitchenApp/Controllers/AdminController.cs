using KitchenApp.DateProvider;
using KitchenApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
    }
}
