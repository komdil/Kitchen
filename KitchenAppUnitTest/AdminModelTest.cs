using KitchenApp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
namespace KitchenAppUnitTest
{
    [TestClass]
    public class AdminModelTest
    {
        [TestMethod]
        public void SelectMenu()
        {
            Admin admin = new Admin();
            Menu menu = new Menu() { Id = new System.Guid(), Name = "plov" };
            admin.SelectMenuForToday(menu);
            Assert.AreEqual(1, menu.Orders.Count, "If menu choosed, we must create Order object and list of orders in Menu class should be equal to 1");
            Order order = menu.Orders.Single();
            Assert.AreEqual(menu, order.Menu, "Field Menu of Order class must be equal to choosed menu!");
            Assert.AreEqual(0, order.PeopleCount, "When Order created count of people, who choosed this menu should be equal to 0");
            Assert.AreEqual(false, order.IsClosed, "When Order created Closed field should be false");
          
        }
    }
}
