﻿using KitchenApp.DateProvider;
using Microsoft.EntityFrameworkCore;
using System;

namespace KitchenApp.Models
{
    public abstract class Entity
    {
        Guid id;
        public Guid Id
        {
            get
            {
                if (id == null || id == Guid.Empty)
                {
                    id = new Guid();
                }
                return id;
            }
            set
            {
                id = value;
            }
        }

        public KitchenAppContext Context;

        public Entity()
        {
            var optionsBuilder = new DbContextOptionsBuilder<KitchenAppContext>();
            Context = new KitchenAppContext(optionsBuilder.Options);
        }
    }
}
