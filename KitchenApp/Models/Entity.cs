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
            Context.Add(this);
        }

        protected Entity()
        {

        }
        public KitchenAppContext Context;
    }
}
