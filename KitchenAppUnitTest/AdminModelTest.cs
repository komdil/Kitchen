using KitchenApp.DateProvider;
using KitchenApp.Models;
using KitchenApp.Models.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
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
            Order order = menu.Orders.Single();
            Assert.AreEqual(DateTime.Today, order.Date);
            Assert.AreEqual(menu, order.Menu, "Field Menu of Order class must be equal to choosed menu!");
            Assert.AreEqual(0, order.PeopleCount, "When Order created, count of people, who choosed this menu should be equal to 0");
            Assert.IsFalse(order.IsClosed, "When Order created, Closed field should be false");

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
            SelectMenuIfNotSelected(TestEntity);
            Order order = Context.Orders.Single(o => o.Date == DateTime.Today);

            decimal price = 105.20M;
            //TODO Payment creation test
            TestEntity.SetPrice(order, price);

            List<Order> orders = TestEntity.GetListOfOrders();
            Assert.AreNotEqual(0, orders.Count, "We should have created order to which we can set price!");

            Order currentOrder = orders.SingleOrDefault(o => o.Id == order.Id);
            Assert.IsNotNull(currentOrder, "Passed order should be exist!");
            Assert.AreEqual(price, currentOrder.Price, "Price should be equal to setted value!");
        }

        [TestMethod]
        public void AddPayment()
        {
            SelectMenuIfNotSelected(TestEntity);
            Order order = Context.Orders.FirstOrDefault(o => o.Date == DateTime.Today);

            User user = new User(Context) { FirstName = "Axe" };
            decimal amount = 10.50M;
            OrderDetail orderDetail = new OrderDetail(Context) { User = user, Order = order };
            //TODO Improve test of payment
            TestEntity.AddPayment(orderDetail, amount);
            Payment payment = user.Payments.SingleOrDefault(p => p.Amount == amount);
            Assert.IsNotNull(payment, "Passed user should have any payment with setted amount");
            //Assert.IsNotNull(payment.Details.Any(pd => pd.OrderDetail == orderDetail,"Payment ");
            Assert.IsTrue(user.Payments.Any(x => x.Amount == amount), "If user payed, this info should saved in list of payments!");
        }
    }
}