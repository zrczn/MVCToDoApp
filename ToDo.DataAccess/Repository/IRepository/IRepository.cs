using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(params string[] optionalParams);
        T Get(Expression<Func<T, bool>> filter, params string[] optionalParams);
        void Add(T entity);
        void Remove(T entity);

    }
}
