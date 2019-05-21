using KitchenApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace KitchenAppUnitTest
{
    [TestClass]
    public abstract class BaseTest<T> : IDisposable where T : Entity
    {
        public T TestEntity { get; set; }
        public KitchenAppContext Context { get; set; }

        [TestInitialize]
        public void TestInitialized()
        {
            Context = GetContext();
            SetTestEntity();
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            Context.Database.EnsureDeleted();
            Context.Database.EnsureCreated();
        }
        public virtual void SetTestEntity()
        {

        }
        public KitchenAppContext GetContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<KitchenAppContext>().UseInMemoryDatabase(Helper.DATABASE);
            //optionsBuilder = new DbContextOptionsBuilder<KitchenAppContext>().UseSqlServer(Helper.CONNECTION_STRING);
            return new KitchenAppContext(optionsBuilder.Options);
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
