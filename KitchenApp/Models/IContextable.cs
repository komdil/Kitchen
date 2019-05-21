using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace KitchenApp.Models
{
    internal class IContextable<T> : IQueryable<T> where T : Entity
    {
        public IQueryable<T> SourceQuery { get; set; }
        public KitchenAppContext Context { get; set; }
        public IContextable(IQueryable<T> query, KitchenAppContext context)
        {
            SourceQuery = query;
            Context = context;
        }

        public Type ElementType => SourceQuery.ElementType;

        public Expression Expression => SourceQuery.Expression;

        public IQueryProvider Provider => SourceQuery.Provider;

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var entity in SourceQuery)
            {
                if (entity.Context == null || entity.Context != Context)
                    entity.Context = Context;
                yield return entity;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
