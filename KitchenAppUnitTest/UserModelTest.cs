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
            User admin = new User();
            Menu menu = new Menu();
            admin.SelectMenuForToday(menu);
            Assert.AreEqual(1, menu.Orders.Count, "If menu choosed, we must create Order object and list of orders in Menu class should be equal to 1");
            Order order = menu.Orders.Single();
            Assert.AreEqual(menu, order.Menu, "Field Menu of Order class must be equal to choosed menu!");
            Assert.AreEqual(0, order.PeopleCount, "When Order created count of people, who choosed this menu should be equal to 0");
            Assert.AreEqual(false, order.IsClosed, "When Order created Closed field should be false");
            Assert.AreNotEqual(0, order.Price, "Price can't be 0");
        }

        [TestMethod]
        public void UserAdd()
        {
            Admin admin = new Admin();
            User user = new User();
            admin.AddNewUser(user);

            Assert.IsNotNull(user.Id,"User must have ID");
            Assert.IsNotNull(user.Login, "User must have Login");
            Assert.IsNotNull(user.Password, "User must have Password");
            Assert.AreNotEqual(UserRole.Admin, user.Role, "New user can't be Admin");
        }

        [TestMethod]
        public void UserDelete()
        {
            Admin admin = new Admin();
            User user = new User();

            admin.Delete(user);
        }
    }
}
