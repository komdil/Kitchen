using KitchenApp.DateProvider;
using KitchenApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public IActionResult Index() => View();
        public IActionResult Users() => View();
        public IActionResult Orders() => View();
        public IActionResult Payments() => View();

        public IActionResult Menus() => View();

        public IActionResult CreateNewMenu() => View();
        public IActionResult DeleteMenu() => View();

        public IActionResult SelectMenuForToday() => View();

        public IActionResult CreateNewUser() => View();

        public IActionResult DeleteUser() => View();
        public IActionResult ChahgeUser() => View();

        public IActionResult CreateNewOrder() => View();
        public IActionResult ChahgeOrder() => View();
        
        public IActionResult DeleteOrder() => View();










    }
}