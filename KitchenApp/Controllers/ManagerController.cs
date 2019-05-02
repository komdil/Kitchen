using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KitchenApp.DateProvider;
using KitchenApp.Models;
using System;
using System.Linq;

namespace KitchenApp.Controllers
{
    [Route("api/[controller]")]
    public class ManagerController : Controller
    {
        KitchenAppContext db;
        public ManagerController(KitchenAppContext context)
        {
            db = context;
        }
        [HttpGet("{request}")]
        public IActionResult Get(string request)
        {
            if (request == "CreateDemoData")
            {
                DemoData demo = new DemoData();
                demo.CreateDemoData(db);
                return Ok("Success");
            }
            if (request == "TodaysMenu")
            {
                var todaysFood = db.Orders.Include("Menu").FirstOrDefault(o => o.Date == DateTime.Today);
                if (todaysFood != null)
                {
                    var m = todaysFood.Menu;
                    var menuToSend = new Menu { Id = m.Id, Name = m.Name, Description = m.Description };
                    return Ok(menuToSend);
                }
                else
                {
                    return Ok("Order is not created yet!");
                }
            }
            return Ok("You didn't request anything");
        }
        [HttpPost("addMe/{login}/{include}")]
        public IActionResult AddMe(string login, bool include)
        {
            var user = db.Users.FirstOrDefault(u => u.Login == login);
            if (user != null)
            {
                var order = db.Orders.Include("Details").FirstOrDefault(o => o.Date == DateTime.Today);
                if (order == null)
                {
                    return Ok("Order is not created yet");
                }
                else
                {
                    var orderDetail = order.Details.FirstOrDefault(d => d.UserId == user.Id);
                    if (orderDetail == null)
                    {
                        if (include)
                        {
                            var newOrderDetail = new OrderDetail() { OrderedDateTime = DateTime.Now, Order = order, User = user };
                            db.Add(newOrderDetail);
                            db.SaveChanges();
                            return Ok("Your name got added for today's food. You won't starve!");
                        }
                        else
                        {
                            return Ok("There is not order created for you");
                        }
                    }
                    else
                    {
                        if (include)
                        {
                            return Ok("You have already signed ip for today's food!");
                        }
                        else
                        {
                            if (!order.IsClosed)
                            {
                                db.Remove(orderDetail);
                                db.SaveChanges();
                                return Ok("Your name has been removed from today's order");
                            }
                            else
                            {
                                return Ok("Order is already closed, so we can't remove your name!");
                            }
                        }
                    }

                }
            }
            return Ok("NULL");
        }

        [HttpPost("changeOrder/{closeOrder}/{amount}")]
        public IActionResult ChangeOrderStatus(bool closeOrder, decimal amount)
        {
            var order = db.Orders.Include("Details").SingleOrDefault(o => o.Date == DateTime.Today);
            if (order == null)
            {
                return Ok("There is no order created for today");
            }
            else
            {
                if (closeOrder)
                {
                    order.PeopleCount = order.Details.Count;
                    order.Price = amount;
                    decimal perPerson = 0;
                    if (amount > 0)
                    {
                        perPerson = amount / order.PeopleCount;
                        foreach (var orderDetail in order.Details)
                        {
                          //  orderDetail.Amount = perPerson;
                        }
                    }
                    order.IsClosed = true;
                    db.SaveChanges();
                    return Ok($"Order is closed sucessfully. Today price is {amount}SMN and per person {perPerson}SMN");
                }
                else
                {
                    order.IsClosed = false;
                    db.SaveChanges();
                    return Ok("Order has been opened for users!");
                }
            }
        }
        [HttpPost("CraeteOrder/{menuName}")]
        public IActionResult CreateOrder(string menuName)
        {
            var menu = db.Menus.FirstOrDefault(m => m.Name == menuName);
            if (menu == null)
            {
                return Ok($"There is no food with name {menuName}. Please eneter correct menu name.");
            }
            else
            {
                var order = new Order() { Date = DateTime.Today, Menu = menu };
                db.Add(order);
                db.SaveChanges();
                return Ok("Today's order is created and is open for orders.");
            }
        }
        [HttpPost("AddMenu/{FoodName}/{Description}")]
        public IActionResult Addmenu(string FoodName,string description)
        {
            var menu = db.Menus.FirstOrDefault(m => m.Name == FoodName );
            if (menu != null)
            {
                return Ok($"The food you are trying to add already exists in the DB.");
            }
            else
            {
                var food = new Menu () { Name =FoodName , Description=description  };
                db.Add(food);
                db.SaveChanges();
                return Ok("Food is added to the menu.");
            }
        }
        [HttpPost("AddUser/{userLogin}/{userPassword}")]
        public IActionResult AddUser(string userLogin,string userPassword)
        {
            var user = db.Users.FirstOrDefault(m => m.Login  == userLogin);
            if (user != null)
            {
                return Ok($" There is User with login {userLogin}.");
            }
            else
            {
                var user1   = new User () { Login =userLogin ,Password =userPassword  };
                db.Add(user1 );
                db.SaveChanges();
                return Ok("User is added.");
            }
        }
        /*
         - Admin order sohta tonad
        ++ Admin ordera mahkam karda tonad. Vakti close kadaginda obshiy rashoda darovarda tonad. 
            > Chand kas plus mondagisha yofta orderba PeopleCounasha set kunad
            > Haar yak imruzangi orderba Amountasha set kunad. OrderDetail.Amount = Order.Price/Order.PeopleCount
         - Admin useri nav kush karda tonad. Login, Password metiyad. 
         - Admin menuba ovkoti nava kush karda tonad. 
         - Admin az nomi user plus monda tonad yo girifta partofta tonad. OrderDetail ba AdminAdded polya kush kuniton. In kayki admin ba joi user plus monad.
    */
    }
}
