using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
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

        public void Update(ToDoModel obj)
        {
            _db.toDoModels.Update(obj);
        }

        private List<int> convertDateToRoutineNumber(DateTime DateOption)
        {
            List<int> outcomes = new List<int>();

            if (DateOption.DayOfWeek == 0)
                outcomes.Add(11);
            if ((int)DateOption.DayOfWeek == 1)
                outcomes.Add(5);
            if ((int)DateOption.DayOfWeek == 2)
                outcomes.Add(6);
            if ((int)DateOption.DayOfWeek == 3)
                outcomes.Add(7);
            if ((int)DateOption.DayOfWeek == 4)
                outcomes.Add(8);
            if ((int)DateOption.DayOfWeek == 5)
                outcomes.Add(9);
            if ((int)DateOption.DayOfWeek == 6)
                outcomes.Add(10);

            if ((int)DateOption.DayOfWeek % 2 == 0)
                outcomes.Add(2);
            if ((int)DateOption.DayOfWeek % 2 != 0)
                outcomes.Add(3);
            if ((int)DateOption.DayOfWeek == 6 || DateOption.DayOfWeek == 0)
                outcomes.Add(4);

            outcomes.Add(1);

            return outcomes;
        }

        public IEnumerable<ToDoModel> GetTodayToDos(int increaseDate = 0)
        {
            DateTime date = DateTime.Today;
            date = date.AddDays(increaseDate);

            IEnumerable<ToDoModel> getSingleDayData = _db.toDoModels.Where(x => x.ShowAtSingleDay == date);

            IEnumerable<ToDoModel> getRoutineData = _db.toDoModels.Where(x => convertDateToRoutineNumber(date)
                                                                        .Contains(x.RoutineOption)).AsEnumerable();

            return getRoutineData.Union(getSingleDayData).OrderBy(x => x.Priority);
        }

        public IEnumerable<(string, IEnumerable<ToDoModel>)> GetSevenDayToDos()
        {
            List<(string, IEnumerable<ToDoModel>)> total = new List<(string, IEnumerable<ToDoModel>)>();

            for (int i = 1; i <= 7; i++)
            {
                IEnumerable<ToDoModel> current = GetTodayToDos(i).OrderBy(x => x.Priority);

                if (i == 1)
                {
                    total.Add(("Tomorrow", current));
                    continue;
                }

                if (i == 2)
                {
                    total.Add(($"{DateTime.Today.AddDays(i).ToString("dddd, dd MMMM yyyy")}", current));
                    continue;
                }

                else
                    total.Add(($"{DateTime.Today.AddDays(i).ToString("dddd, dd MMMM yyyy")}", current));
            }

            return total;
        }
    }
}
