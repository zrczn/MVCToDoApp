using ToDo.ApplicationDBContext;
using ToDo.Models;

namespace ToDo.SupportiveMaterials
{
    internal class ExpiredDateHandler
    {
        internal static void CleanExpiredDateToDos(AppDbCon db)
        {
            IEnumerable<ToDoModel> expiredToDos = db.toDoModels.Select(x => x)
                                                .Where(x => x.ShowAtSingleDay < DateTime.Today && x.ShowAtSingleDay != new DateTime(1, 1, 1, 0, 0, 0));

            foreach (var instance in expiredToDos)
            {
                db.Remove(instance);
            }

            //db.Remove(getAllToDos);
            db.SaveChanges();

        }
    }
}
