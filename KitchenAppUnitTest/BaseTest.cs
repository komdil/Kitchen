using KitchenApp.DateProvider;
using KitchenApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KitchenAppUnitTest
{
    [TestClass]
    public abstract class BaseTest<T> where T : Entity
    {
        public T TestEntity { get; set; }
        public KitchenAppContext Context { get; set; }

        [TestInitialize]
        public void TestInitialized()
        {
            Context = GetContext();
            SetTestEntity();
        }
        public virtual void SetTestEntity()
        {

        }
        public KitchenAppContext GetContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<KitchenAppContext>().UseInMemoryDatabase(Helper.DATABASE);
            return new KitchenAppContext(optionsBuilder.Options);
        }
    }
}
