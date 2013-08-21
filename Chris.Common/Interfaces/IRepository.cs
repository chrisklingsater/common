using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Chris.Common.Interfaces
{
    using System.Linq;
    using System.Linq.Expressions;

    public interface IRepository<T> where T : class
    {
        IEnumerable<T> FindAll(Expression<Func<T, bool>> where);

        IEnumerable<T> FindAll<TKey>(Expression<Func<T, bool>> where, Expression<Func<T, TKey>> orderBy);
        
        IEnumerable<T> FindAll();

        T Find(params object[] keys);

        T Create();

        void Add(T item);

        void Remove(T item);

        void Update(T item);
    }
}
