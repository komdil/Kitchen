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

        public IActionResult Users()
        {
            var users = appContext.Users;

            return View(users);
        }
        
          public IActionResult Orders() => View();
        public IActionResult Payments() => View();
        public IActionResult Menus()
        {
            var menus = appContext.Menus;

            return View(menus);
        }
        public IActionResult CreateNewMenu() => View();
        public IActionResult UpdateMenu() => View();
        public IActionResult SelectMenuForToday() => View();
        public IActionResult AddUser() => View();
        public IActionResult DeleteUser() => View();
        public IActionResult ChahgeUser() => View();

        public IActionResult CreateNewOrder() => View();
        public IActionResult ChahgeOrder() => View();

        public IActionResult DeleteOrder() => View();
        public IActionResult CreateOrder() => View();
       
        [HttpPost]
        public int CreateNewMenu(string name,string description)
        {
          var menus= appContext.Menus.ToList();
            foreach (var item in menus)
            {
                if (item.Name==name)
                {

                }

            }
            Menu menu = new Menu() {



                Name =name,Description=description };
            appContext.Add(menu);
       return     appContext.SaveChanges();
         }

        
    }
}