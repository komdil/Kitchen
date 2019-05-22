using KitchenApp.Models;
using KitchenApp.ViewsModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SignalRPushNotification.Server;
using SignalRPushNotification.Server.Models;
using System.Threading.Tasks;
using KitchenApp.Models.Exceptions;
using System.Data.SqlClient;

namespace KitchenApp.Controllers
{
    [Authorize(Roles = Helper.ADMIN_ROLE)]
    public class AdminController : Controller
    {
        private readonly IPushNotificationService _pushNotificationService;
        public Admin Admin
        {
            get
            {
                var admin = appContext.GetEntities<Admin>().Single(a => a.Login == User.Identity.Name);
                admin.Context = appContext;
                return admin;

            }
        }
        KitchenAppContext appContext;
        public AdminController(KitchenAppContext appContext, IPushNotificationService pushNotificationService)
        {
            this.appContext = appContext;
            _pushNotificationService = pushNotificationService;
        }

        public IActionResult Index()
        {
            var menus = appContext.GetEntities<Menu>().ToList();
            
            return View(menus);
        }

        [HttpPost]
        public IActionResult Index(string SelectedMenu)
        {
            Menu menu = appContext.GetEntities<Menu>().Single(m => m.Id.ToString() == SelectedMenu);
            Admin.SelectMenuForToday(menu);
            return View();
        }

        public IActionResult Users()
        {
            var users = appContext.GetEntities<User>();
            return View(users);
        }

        public IActionResult Orders()
        {
            

            var order = appContext.GetEntities<Order>();
         
            ViewBag.order = order;
            return View(order);

        }
        public IActionResult Payments() => View();
        public IActionResult Menus()
        {
            var menus = appContext.GetEntities<Menu>();
            return View(menus);
        }
        public IActionResult CreateNewMenu() => View();
        [HttpPost]
        public IActionResult CreateNewMenu(MenuModel menuModel)
        {
            try
            {
                Admin.CreateNewMenu(menuModel.Name, menuModel.Description);
                return RedirectToAction("Menus", "Admin");
            }
            catch (MenuAleadyIsExsistException ex)
            {
                menuModel.ErrorMessage = ex.Message;
            }
            return View(menuModel);

        }

        [HttpGet]
        public IActionResult UpdateMenu(Guid Id)
        {
            var menu = appContext.GetEntities<Menu>().FirstOrDefault(m => m.Id == Id);
            var menuModel = new MenuModel() { Name = menu.Name, Description = menu.Description, Id = menu.Id };
            return View(menuModel);
        }

        [HttpPost]
        [ActionName("UpdateMenu")]
        public IActionResult ShowUpdateMenu(MenuModel menuModel)
        {
            try
            {
                Admin.UpdateMenu(menuModel.Id, menuModel.Name, menuModel.Description);
                return RedirectToAction("Menus", "Admin");
            }
            catch (Exception ex)
            {
                if (ex is MenuAleadyIsExsistException || ex is MenuWasNotFoundException)
                {
                    menuModel.ErrorMessage = ex.Message;
                }
                else
                {
                    throw;
                }
            }
            return View(menuModel);
        }

        public IActionResult CreateNewUser()
        {
            return View();
        }
        [HttpPost]

        public IActionResult CreateNewUser(UserModel userModel)
        {
            try
            {
                Admin.CreateNewUser(userModel.FirstName, userModel.LastName, userModel.Login, userModel.Password);
            }
            catch (UserIsAlreadyExsist ex)
            {
                userModel.Errormessage = ex.Message;
            }
            return View(userModel);

        }

        public IActionResult UpdateUser(Guid Id)
        {
            var user = appContext.GetEntities<User>().FirstOrDefault(u => u.Id == Id);
            var userModel = new UserModel{FirstName=user.FirstName,LastName=user.LastName,Login=user.Login,Password=user.Password,Id=user.Id};

            return View(userModel);
        }

        [HttpPost]
        public IActionResult UpdateUser(UserModel userModel)
        {
            try
            {
                Admin.UpdateUser(userModel.Id,userModel.FirstName,userModel.LastName,userModel.Login,userModel.Password);
                return RedirectToAction("Users", "Admin");
            }
            catch (Exception ex)
            {
                if (ex is MenuAleadyIsExsistException || ex is MenuWasNotFoundException)
                {
                    userModel.Errormessage = ex.Message;
                }
                else
                {
                    throw;
                }
            }
            return View(userModel); 
        }

        public IActionResult SelectMenuForToday()
        {
            var menu = appContext.GetEntities<Menu>().ToList();
            ViewBag.menuname = menu;
            return View();
        }
        [HttpPost]
        public IActionResult SelectMenuForToday(Menu menu)
        {
            Admin.SelectMenuForToday(menu);
            return View();

        }
            



        public async Task<IActionResult> SelectMenuForToday(PushNotificationModel notification)
        {
            //TODO: save to database

            //notification
            await SendNotification(notification);

            return RedirectToAction("Menus", "Admin");
        }
        [HttpPost]
        public async Task SendNotification(PushNotificationModel notification)
        {
            await _pushNotificationService.SendAsync(notification);
        }
        public IActionResult AddUser() => View();

        [HttpPost]
        public IActionResult AddUser(User userData)
        {
            //is not empty
            userData.Salt = Helper.SaltGenerate();
            userData.Password = Helper.HashPassword(userData.Password, userData.Salt);

            return View(userData);
        }
        public IActionResult DeleteUser() => View();
        public IActionResult ChahgeUser() => View();

        public IActionResult CreateNewOrder() => View();
        public IActionResult ChahgeOrder() => View();

        public IActionResult DeleteOrder() => View();
        public IActionResult CreateOrder() => View();

    }
}