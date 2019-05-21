using KitchenApp.DateProvider;
using KitchenApp.Models;
using KitchenApp.ViewsModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        
          public IActionResult Orders()
        {
            var orders = appContext.Orders;

            return View(orders);
        }
        public IActionResult Payments() => View();
        public IActionResult Menus()
        {
            var menus = appContext.Menus;

            return View(menus);
        }
        public IActionResult CreateNewMenu() => View();
        public IActionResult UpdateMenu(Guid Id)
        {
            var menu = appContext.Menus.FirstOrDefault(m => m.Id == Id);
            Helper.IdMenu = Id;
            return View(menu);
            
        }
        [HttpPost]
        public IActionResult UpdateMenu(Menu menuModel)
        {
            var menu = appContext.Menus.FirstOrDefault(m => m.Id == Helper.IdMenu);
            menu.Name = menuModel.Name;
            menu.Description = menuModel.Description;
            appContext.Update(menu);
            appContext.SaveChanges();
            return RedirectToAction("Menus", "Admin");

        }

        public IActionResult CreateNewUser()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateNewUser(UserModel userModel)
        {
            return View();
        }
        public IActionResult UpdateUser(Guid Id)
        {
            var user = appContext.Users.FirstOrDefault(u => u.Id == Id);
            return View(user);

            
        }
        public IActionResult SelectMenuForToday() => View();
        public IActionResult AddUser() => View();
        public IActionResult DeleteUser() => View();
        public IActionResult ChahgeUser() => View();

        public IActionResult CreateNewOrder() => View();
        public IActionResult ChahgeOrder() => View();

        public IActionResult DeleteOrder() => View();
        public IActionResult CreateOrder() => View();
       
        [HttpPost]
        public IActionResult CreateNewMenu(string name,string description)
        {
            Menu menu = new Menu()
            {
                Name = name,
                Description = description
            };
            appContext.Add(menu);
            appContext.SaveChanges();

            return RedirectToAction("Menus", "Admin");
       

        }
        
       public IActionResult DeleteMenu(Guid Id)
        {
            return View();
        }
        [HttpPost]
        public IActionResult DeleteMenu(string id)
        {
            var menu = appContext.Menus.FirstOrDefault(m => m.Id.ToString() == id);
            appContext.Remove(menu);
            appContext.SaveChanges();
            return RedirectToAction("Menus", "Admin");  
                        
        }

        [HttpPost]
        public IActionResult DeleteUser(string id)
        {


            return RedirectToAction("Users", "Admin");
        }


    }
}