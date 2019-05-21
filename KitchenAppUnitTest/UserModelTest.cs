using KitchenApp.Models;
using KitchenApp.Models.Exceptions;
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
            TestEntity = new User(Context);
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
        public void AcceptMenu()
        {
            Admin admin = new Admin(Context);
            SelectMenuIfNotSelected(admin);
            var menu = Context.GetSelectedMenuForToday();
            TestEntity.AcceptMenu(menu);
            Assert.AreNotEqual(0, menu.Orders.Count, "We can't choose menu if admin doesn't create order");
            var order = menu.Orders.First();
            Assert.AreNotEqual(0, order.Details.Count, "If some user choosed menu, than Order.OrderDetail can't be 0");
            Assert.IsTrue(order.Details.Any(x => x.User == TestEntity), "If user accepted menu, than OrderDetail should contain info about this");
            Assert.AreEqual(1, order.PeopleCount, "Pepople count should be 1 because 1 user accepted menu");
        }

        [TestMethod]
        public void RejectMenu()
        {
            Admin admin = new Admin(Context);
            SelectMenuIfNotSelected(admin);
            var menu = Context.GetSelectedMenuForToday();
            TestEntity.AcceptMenu(menu);
            TestEntity.RejectMenu(menu);
            Assert.IsFalse(menu.Orders.First().Details.Any(x => x.User == TestEntity), "If user accepted menu, than rejected OrderDetail should be removed");
        }
    }
}
