using KitchenApp.Models;
using KitchenApp.Models.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
namespace KitchenAppUnitTest
{
    [TestClass]
    public class AdminModelTest : BaseTest<Admin>
    {
        public override void SetTestEntity()
        {
            TestEntity = new Admin(Context);
        }

        [TestMethod]
        [ExpectedException(typeof(MenuAlreadySelectedException), "It should throw exception, because we're executing SelectMenuForToday twice")]
        public void SelectMenu()
        {
            Menu menu = new Menu(Context) { Name = "plov" };
            TestEntity.SelectMenuForToday(menu);
            Assert.AreEqual(1, menu.Orders.Count, "If menu choosed, we must create Order object and list of orders in Menu class should be equal to 1");
            Order order = menu.Orders.Single(d => d.Date == DateTime.Today);
            Assert.AreEqual(menu, order.Menu, "Field Menu of Order class must be equal to choosed menu!");
            Assert.AreEqual(0, order.PeopleCount, "When Order created, count of people, who choosed this menu should be equal to 0");
            Assert.AreEqual(false, order.IsClosed, "When Order created, Closed field should be false");

            TestEntity.SelectMenuForToday(menu); //There should be exception
        }

        void SelectMenuIfNotSelected(Admin admin)
        {
            try
            {
                var seletedMenu = Context.GetSelectedMenuForToday();
            }
            catch (MenuWasNotSelectedForTodayException)
            {
                Menu menu = new Menu(Context) { Name = "plov" };
                admin.SelectMenuForToday(menu);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(OrderAlreadyClosedException), "It should throw exception, because we're executing CloseOrderOfToday twice")]
        public void CloseOrderTest()
        {
            SelectMenuIfNotSelected(TestEntity);

            TestEntity.CloseOrderOfToday();
            Order order = Context.GetEntities<Order>().FirstOrDefault(o => o.Date == DateTime.Today);
            Assert.IsNotNull(order, "We cant close order, which was not created");
            Assert.IsTrue(order.IsClosed, "Order should be closed!");

            TestEntity.CloseOrderOfToday();//Exception will be thrown here 
        }

        [TestMethod]
        [ExpectedException(typeof(PriceAlreadySetException), "It should throw exception, RejectMenu calls after setting price")]
        public void SetPrice()
        {
            Admin admin = new Admin(Context);
            SelectMenuIfNotSelected(admin);
            var menu = Context.GetSelectedMenuForToday();

            //Users accepts menu
            User firstUser = new User(Context);
            User secondUser = new User(Context);
            firstUser.AcceptMenu(menu);
            secondUser.AcceptMenu(menu);

            //Admin sets price
            decimal price = 105.20M;
            TestEntity.SetPrice(price);

            //Price should be setted
            var order = Context.GetSelectedMenuForToday().Orders.First();
            Assert.AreEqual(price, order.Price, "Price should be equal to setted value!");

            //Price for each people
            Assert.AreEqual(price / 2, order.PriceForEach, "Price for each people should be price/2 because 2 users accepted menu");

            firstUser.RejectMenu(menu); // Exception throws here
        }


        [TestMethod]
        public void SetPrice_PaymentTest()
        {
            // TODO
        }

        [TestMethod]
        public void AddPayment()
        {
            User user = new User(Context);
            decimal amount = 10.50M;
            TestEntity.AddPayment(user, amount);

            Assert.IsTrue(TestEntity.Payments.Any(x => x.User == user && x.SummAmount == amount), "If user payed, this info should saved in list of payments!");
        }
    }
}