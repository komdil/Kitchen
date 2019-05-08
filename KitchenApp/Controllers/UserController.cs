using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KitchenApp.DateProvider;
using KitchenApp.Models;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KitchenApp.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        KitchenAppContext db;

        public UserController(KitchenAppContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
