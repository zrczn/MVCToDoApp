using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.ApplicationDBContext;
using ToDo.DataAccess.Repository.IRepository;

namespace ToDo.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public IToDoRepository ToDoRepository { get; private set; }
        private AppDbCon _db;

        public UnitOfWork(AppDbCon db)
        {
            _db = db;
            ToDoRepository = new ToDoRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
