using KitchenApp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace KitchenAppUnitTest
{
    [TestClass]
    public class UserModelTest
    {
        [TestMethod]
        public void ChooseMenu()
        {
            User user = new User();
            Menu menu = new Menu();
            user.ChooseMenu(menu);
            Assert.AreNotEqual(0, menu.Orders[0].Details.Count, "If some user choosed menu, than Order.OrderDetail can't be 0");
            Assert.IsTrue(menu.Orders[0].Details.Any(x => x.User == user));
        }
    }
}
