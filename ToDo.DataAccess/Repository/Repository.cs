using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ToDo.ApplicationDBContext;
using ToDo.DataAccess.Repository.IRepository;

namespace ToDo.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbCon _db;
        private readonly DbSet<T> DbSet;

        public Repository(AppDbCon db)
        {
            _db = db;
            this.DbSet = _db.Set<T>();
        }

        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter, params string[] optionalParams)
        {
            IQueryable<T> query = DbSet;
            query = query.Where(filter);

            foreach (var item in optionalParams)
            {
                query = query.Include(item);
            }

            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(params string[] optionalParams)
        {
            IQueryable<T> query = DbSet;

            foreach (var item in optionalParams)
            {
                query = query.Include(item);
            }

            return query.Select(x => x).ToList();
        }

        public void Remove(T entity)
        {
            DbSet.Remove(entity);
        }
    }
}
