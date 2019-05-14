using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KitchenApp.Controllers
{
    public class Payments:Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
