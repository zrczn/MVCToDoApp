using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Models;

namespace ToDo.DataAccess.Repository.IRepository
{
    public interface IToDoRepository : IRepository<ToDoModel>
    {
        void Update(ToDoModel obj);
        void Save(ToDoModel obj);
        void CleanExpiredDateData();

    }
}
