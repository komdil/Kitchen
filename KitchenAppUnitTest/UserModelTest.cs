using KitchenApp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace KitchenAppUnitTest
{
    [TestClass]
    public class UserModelTest : BaseTest<User>
    {
        public override void SetTestEntity()
        {
            TestEntity = new User(Context) { FirstName="testUser"};
        }
        [TestMethod]
        public void AcceptMenu()
        {
            Admin admin = new Admin(Context);
            Menu menu = new Menu(Context) { Name = "plov" };
            admin.SelectMenuForToday(menu);
            Order order = menu.Orders.Single();

            TestEntity.AcceptMenu(order);

            Assert.AreNotEqual(0, order.Details.Count, "If some user choosed menu, than Order.OrderDetail can't be 0");
            Assert.IsTrue(order.Details.Any(x => x.User == TestEntity), "If user accepted menu, than OrderDetail should contain info about this");
        }
    }
}
