using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ToDo.Models;

namespace ToDo.DataAccess.Repository.IRepository
{
    public interface IToDoRepository : IRepository<ToDoModel>
    {
        void Update(ToDoModel obj);
        void CleanExpiredDateData();
        IEnumerable<ToDoModel> GetTodayToDos(int increaseDate = 0);
        IEnumerable<(string, IEnumerable<ToDoModel>)> GetSevenDayToDos(string loggedinUserId);

    }
}
