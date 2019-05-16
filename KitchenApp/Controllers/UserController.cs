using Microsoft.AspNetCore.Mvc;
using KitchenApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace KitchenApp.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        KitchenAppContext appContext;
        public UserController(KitchenAppContext context)
        {
            appContext = context;
        }

        public IActionResult Index() => View();


        public IActionResult LookPayments() => View();





    }
}
