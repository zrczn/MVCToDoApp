using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.ApplicationDBContext;
using ToDo.DataAccess.Repository.IRepository;

namespace ToDo.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbCon _db;
        internal DbSet<T> DbSet;

        public Repository(AppDbCon db)
        {
            _db = db;
            this.DbSet = _db.Set<T>();
        }

        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public T Get(System.Linq.Expressions.Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = DbSet;
            query = query.Where(filter);

            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = DbSet;
            return query.Select(x => x).ToList();
        }

        public void Remove(T entity)
        {
            DbSet.Remove(entity);
        }
    }
}
