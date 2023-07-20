using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using ToDo.ApplicationDBContext;
using ToDo.Models;

using ToDoScheduler = ToDo.SupportiveMaterials.TaskScheduler;

namespace ToDo.Controllers
{
    public class ToDoController : Controller
    {
        private readonly AppDbCon _db;

        public ToDoController(AppDbCon db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<ToDoModel> getTodayToDos = ToDoScheduler.ZenData(_db);

            return View(getTodayToDos);
        }

        public IActionResult SevenDaysSchedule()
        {
            IEnumerable<(string, IEnumerable<ToDoModel>)> getSevenDaysToDos = ToDoScheduler.SevenDaysData(_db);

            return View(getSevenDaysToDos);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new ToDoModel());
        }

        [HttpPost]
        public IActionResult Create(ToDoModel model)
        {
            if (ModelState.IsValid && 
                (model.RoutineOption == 0 ^ model.ShowAtSingleDay == new DateTime(1, 1, 1, 0, 0, 0)))
            {
                _db.toDoModels.Add(model);
                _db.SaveChanges();
            }
            else
                return View();

            return RedirectToAction("Index");
        }
    }
}
