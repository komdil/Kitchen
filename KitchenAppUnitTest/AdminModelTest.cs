using KitchenApp.DateProvider;
using KitchenApp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
namespace KitchenAppUnitTest
{
    [TestClass]
    public class AdminModelTest : BaseTest
    {
        [TestMethod]
        public void SelectMenu()
        {
            Menu menu = new Menu() { Id = Guid.NewGuid(), Name = "plov" };
            try
            {
                testAdmin.SelectMenuForToday(menu);
                Assert.AreEqual(1, menu.Orders.Count, "If menu choosed, we must create Order object and list of orders in Menu class should be equal to 1");
                Order order = menu.Orders.Single(d => d.Date == DateTime.Today);
                Assert.AreEqual(menu, order.Menu, "Field Menu of Order class must be equal to choosed menu!");
                Assert.AreEqual(0, order.PeopleCount, "When Order created, count of people, who choosed this menu should be equal to 0");
                Assert.AreEqual(false, order.IsClosed, "When Order created, Closed field should be false");
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.Message != "", "We must notify admin with some message!");
            }
        }

        void SelectMenuIfNotSelected(Admin admin)
        {
            var seletedMenu = admin.GetTodaysMenu();
            if (seletedMenu == null)
            {
                Menu menu = new Menu() { Id = Guid.NewGuid(), Name = "plov" };
                admin.SelectMenuForToday(menu);
            }
        }

        [TestMethod]
        public void CloseOrderTest()
        {
            SelectMenuIfNotSelected(testAdmin);

            testAdmin.CloseOrderOfToday();
            Order order = Context.Orders.Local.FirstOrDefault(o => o.Date == DateTime.Today);
            Assert.IsNotNull(order, "We cant close order, which was not created");
            Assert.IsTrue(order.IsClosed, "Order should be closed!");

            Exception orderIsAlreadyClosedException = null;
            try
            {
                testAdmin.CloseOrderOfToday();
            }
            catch (Exception ex)
            {
                orderIsAlreadyClosedException = ex;
            }
            Assert.IsNotNull(orderIsAlreadyClosedException, "We must notify admin with some message!");
        }

        [TestMethod]
        public void SetPrice()
        {
            decimal price = 105.20M;
            testAdmin.SetPrice(price);

            Assert.IsNotNull(testAdmin.Details.FirstOrDefault(), "We should have OrderDetails to get Order!");
            Assert.IsNotNull(testAdmin.Details.FirstOrDefault().Order, "We should have created order to which we can set price!");
            Assert.AreEqual(price, testAdmin.Details.FirstOrDefault().Order.Price, "Price should be equal to setted value!");
        }

        [TestMethod]
        public void AddPayment()
        {
            User user = new User(Context);
            decimal amount = 10.50M;
            testAdmin.AddPayment(user, amount);

            Assert.IsTrue(testAdmin.Payments.Any(x => x.User == user && x.Amount == amount), "If user payed, this info should saved in list of payments!");
        }
    }
}