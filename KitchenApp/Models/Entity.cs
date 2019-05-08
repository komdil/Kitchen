using KitchenApp.DateProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KitchenApp.Models
{
    public class Entity
    {
        public KitchenAppContext Context { get; }
        public Entity(KitchenAppContext _context)
        {
            Context = _context;
        }
    }
}
