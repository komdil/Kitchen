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
            var menus = appContext.GetEntities<Menu>();
            return View(menus);
        }

        public IActionResult Users()
        {
            var users = appContext.GetEntities<User>();
            return View(users);
        }

        public IActionResult Orders() => View();
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
            var menuModel = appContext.GetEntities<Menu>().FirstOrDefault(m => m.Id == Id);
            return View(menuModel);

        }

        [HttpPost]
        [ActionName("UpdateMenu")]
        public IActionResult ShowUpdateMenu(MenuModel menuModel)
        {
            try
            {
                Admin.UpdateMenu(menuModel.Id, menuModel.Name, menuModel.Description);
            }
            catch (MenuWasNotFoundException ex)
            {
                menuModel.ErrorMessage = ex.Message;
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
                Admin.CreateNewUser(userModel.FirstName, userModel.LastName, userModel.Login,userModel.Password);
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
            return View(user);
        }

        [HttpPost]
        public IActionResult UpdateUser(Guid id, UserModel userModel)
        {
            var user = appContext.GetEntities<User>().FirstOrDefault(u => u.Id == userModel.Id);
            return View(user);
        }

        public IActionResult SelectMenuForToday() => View();

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