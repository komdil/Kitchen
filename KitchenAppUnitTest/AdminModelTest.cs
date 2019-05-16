using KitchenApp.Models;
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
            var seletedMenu = admin.GetTodaysMenu();
            if (seletedMenu == null)
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
            Order order = Context.Orders.FirstOrDefault(o => o.Date == DateTime.Today);
            Assert.IsNotNull(order, "We cant close order, which was not created");
            Assert.IsTrue(order.IsClosed, "Order should be closed!");

            TestEntity.CloseOrderOfToday();//Exception will be thrown here 
        }

        [TestMethod]
        public void SetPrice()
        {
            decimal price = 105.20M;
            TestEntity.SetPrice(price);

            Assert.IsNotNull(TestEntity.Details.FirstOrDefault(), "We should have OrderDetails to get Order!");
            Assert.IsNotNull(TestEntity.Details.FirstOrDefault().Order, "We should have created order to which we can set price!");
            Assert.AreEqual(price, TestEntity.Details.FirstOrDefault().Order.Price, "Price should be equal to setted value!");
        }

        [TestMethod]
        public void AddPayment()
        {
            User user = new User(Context);
            decimal amount = 10.50M;
            TestEntity.AddPayment(user, amount);

            Assert.IsTrue(TestEntity.Payments.Any(x => x.User == user && x.Amount == amount), "If user payed, this info should saved in list of payments!");
        }
    }
}