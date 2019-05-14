﻿using KitchenApp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace KitchenAppUnitTest
{
    [TestClass]
    public class UserModelTest : BaseTest<User>
    {
        [TestMethod]
        public void AcceptMenu()
        {
            User user = new User(Context);
            Menu menu = new Menu(Context);
            user.AcceptMenu(menu);
            Assert.AreNotEqual(0, menu.Orders.Count, "We can't choose menu if admin doesn't create order");
            Assert.AreNotEqual(0, menu.Orders.First().Details.Count, "If some user choosed menu, than Order.OrderDetail can't be 0");
            Assert.IsTrue(menu.Orders.First().Details.Any(x => x.User == user), "If user accepted menu, than OrderDetail should contain info about this");
        }
    }
}
