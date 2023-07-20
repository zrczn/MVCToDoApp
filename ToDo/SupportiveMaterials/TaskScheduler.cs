using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Xml.Linq;
using ToDo.ApplicationDBContext;
using ToDo.Controllers;
using ToDo.Models;

namespace ToDo.SupportiveMaterials
{
    internal static class TaskScheduler
    {
        private static List<int> convertDateToRoutineNumber(this DateTime DateOption)
        {
            List<int> outcomes = new List<int>();

            if (((int)DateOption.DayOfWeek) == 0)
                outcomes.Add(11);
            if (((int)DateOption.DayOfWeek) == 1)
                outcomes.Add(5);
            if (((int)DateOption.DayOfWeek) == 2)
                outcomes.Add(6);
            if (((int)DateOption.DayOfWeek) == 3)
                outcomes.Add(7);
            if (((int)DateOption.DayOfWeek) == 4)
                outcomes.Add(8);
            if (((int)DateOption.DayOfWeek) == 5)
                outcomes.Add(9);
            if (((int)DateOption.DayOfWeek) == 6)
                outcomes.Add(10);

            if (((int)DateOption.DayOfWeek) % 2 == 0)
                outcomes.Add(2);
            if (((int)DateOption.DayOfWeek) % 2 != 0)
                outcomes.Add(3);
            if (((int)DateOption.DayOfWeek) == 6 || ((int)DateOption.DayOfWeek) == 0)
                outcomes.Add(4);
            
            outcomes.Add(1);

            return outcomes;
        }

        internal static IEnumerable<ToDoModel> ZenData(AppDbCon DbData, int increaseDate = 0)
        {
            DateTime date = DateTime.Today;
            date = date.AddDays(increaseDate);

            IEnumerable<ToDoModel> getSingleDayData = DbData.toDoModels.Where(x => x.ShowAtSingleDay == date);

            IEnumerable<ToDoModel> getRoutineData = DbData.toDoModels.Where(x => date.convertDateToRoutineNumber()
                                                                        .Contains(x.RoutineOption)).AsEnumerable();

            return getRoutineData.Union(getSingleDayData).OrderBy(x => x.Priority);
        }

        internal static IEnumerable<(string, IEnumerable<ToDoModel>)> SevenDaysData(AppDbCon DbData)
        {
            List<(string, IEnumerable<ToDoModel>)> total = new List<(string, IEnumerable<ToDoModel>)>();

            for (int i = 1; i <= 7; i++)
            {
                IEnumerable<ToDoModel> current = ZenData(DbData, i).OrderBy(x => x.Priority);

                if (i == 1)
                    total.Add(("Tomorrow", current));

                if (i == 2)
                    total.Add(($"{DateTime.Today.AddDays(i).ToString("dddd, dd MMMM yyyy")}", current));

                else
                    total.Add(($"{DateTime.Today.AddDays(i).ToString("dddd, dd MMMM yyyy")}", current));
            }

            return total;
        }
    }
}
