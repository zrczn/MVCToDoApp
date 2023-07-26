using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ToDo.ApplicationDBContext;
using ToDo.DataAccess.Repository.IRepository;
using ToDo.Models;

namespace ToDo.DataAccess.Repository
{
    public class ToDoRepository : Repository<ToDoModel>, IToDoRepository
    {
        private AppDbCon _db;

        public ToDoRepository(AppDbCon db) : base(db)
        {
            _db = db;
        }

        public void CleanExpiredDateData()
        {
            IEnumerable<ToDoModel> expiredToDos = _db.toDoModels.Select(x => x)
                                                .Where(x => x.ShowAtSingleDay < DateTime.Today && x.ShowAtSingleDay != new DateTime(1, 1, 1, 0, 0, 0));

            foreach (var instance in expiredToDos)
            {
                _db.Remove(instance);
            }

            _db.SaveChanges();

        }

        public void Save(ToDoModel obj)
        {
            _db.SaveChanges();
        }

        public void Update(ToDoModel obj)
        {
            _db.toDoModels.Update(obj);
        }
    }
}
