using KitchenApp.DateProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KitchenApp.Models
{
    public class DemoData
    {
        public void CreateDemoData(KitchenAppContext context)
        {
            User u1 = new User() { IsAdmin = true, Login = "Dilshod", Password = "123" };
            User u2 = new User() { IsAdmin = true, Login = "Raufjon", Password = "321" };
            User u3= new User() { IsAdmin = true, Login = "Navruz", Password = "333" };
            context.Add(u1);
            context.Add(u2);
            context.Add(u3);
            context.SaveChanges();

            Menu m1 = new Menu() { Name = "Osh", Description = "Devzira" };
            Menu m2 = new Menu() { Name = "Hom Shurbo", Description = "Gusfandin" };
            Menu m3 = new Menu() { Name = "Borsh", Description = "Ukrainskiy" };
            context.Add(m1);
            context.Add(m2);
            context.Add(m3);
            context.SaveChanges();

            Order o = new Order() { Menu = m1, IsClosed = false, Date = DateTime.Today, Price = 0, PeopleCount = 0 };
            context.Add(o);
            context.SaveChanges();


        }
    }
}
