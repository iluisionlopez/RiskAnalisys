using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CSRTool.Core.Interfaces
{
    public interface IGenericService<T> where T : class
    {
        IList<T> GetAll();
        IList<T> FindBy(Expression<Func<T, bool>> predicate);
        void Delete(T entity);
        void RemoveBy(Expression<Func<T, bool>> predicate);
        object Save(T entity);
        object SaveRange(IList<T> entities);
    }
}
