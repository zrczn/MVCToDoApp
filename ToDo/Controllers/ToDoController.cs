using Microsoft.AspNetCore.Mvc;
using ToDo.ApplicationDBContext;
using ToDo.Models;

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
            IEnumerable<ToDoModel> getToDos = _db.toDoModels.Select(x => x);

            return View(getToDos);
        }

        public IActionResult SevenDaySchedule()
        {
            IEnumerable<ToDoModel> getAllToDos = _db.toDoModels.Select(x => x);

            return View(getAllToDos);
        }
    }
}
