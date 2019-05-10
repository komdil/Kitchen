using KitchenApp.DateProvider;
using KitchenApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace KitchenAppUnitTest
{
    [TestClass]
    public abstract class BaseTest
    {
        public KitchenAppContext Context { get; set; }

        [TestInitialize]
        public void TestInitialized()
        {
            Context = GetContext();
            Context.IsUnitTest = true;
        }
        public KitchenAppContext GetContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<KitchenAppContext>();
            optionsBuilder.UseSqlServer(Helper.ConnectionString);
            return new KitchenAppContext(optionsBuilder.Options);
        }
    }
}
