using JetBrains.Annotations;
using KitchenApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

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
                    id = Guid.NewGuid();
                }
                return id;
            }
            set
            {
                id = value;
            }
        }

        public Entity(KitchenAppContext context)
        {
            Context = context;
        }

        protected Entity()
        {

        }
        public KitchenAppContext Context;
    }
}
