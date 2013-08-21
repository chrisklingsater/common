namespace Chris.Common.EF.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Data.Entity;
    using System.Linq.Expressions;

    using Chris.Common.Interfaces;

    public class SqlRepository<T> : IRepository<T> where T : class, IEntity<T>
    {
        protected DbSet<T> dbSet;

        private readonly DbContext context;

        public SqlRepository(DbContext dbContext)
        {
            context = dbContext;
            dbSet = context.Set<T>();
        }

        #region Implementation of IRepository<T>

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where).ToList();
        }

        public IEnumerable<T> FindAll<TKey>(Expression<Func<T, bool>> where, Expression<Func<T, TKey>> orderBy)
        {
            return dbSet.Where(where).OrderBy(orderBy).ToList();
        }

        public IEnumerable<T> FindAll()
        {
            return dbSet.ToList();
        }

        public T Find(params object[] keys)
        {
            return dbSet.Find(keys);
        }

        public T Create()
        {
            return dbSet.Create();
        }

        public void Add(T item)
        {
            dbSet.Add(item);
        }

        public void Remove(T item)
        {
            dbSet.Remove(item);
        }

        public void Update(T item)
        {
            context.Entry(item).State = EntityState.Modified;
        }

        #endregion
    }
}