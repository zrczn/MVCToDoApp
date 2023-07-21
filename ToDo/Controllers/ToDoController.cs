using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using ToDo.ApplicationDBContext;
using ToDo.Models;
using ToDo.SupportiveMaterials;
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
            ExpiredDateHandler.CleanExpiredDateToDos(_db);

            IEnumerable<ToDoModel> getTodayToDos = ToDoScheduler.ZenData(_db);

            return View(getTodayToDos);
        }

        public IActionResult SevenDaysSchedule()
        {
            IEnumerable<(string, IEnumerable<ToDoModel>)> getSevenDaysToDos = ToDoScheduler.SevenDaysData(_db);

            return View(getSevenDaysToDos);
        }


        //Create
        [HttpGet]
        public IActionResult Create()
        {
            return View(new ToDoModel());
        }

        [HttpPost]
        public IActionResult Create(ToDoModel model)
        {
            if(!(model.RoutineOption == 0 ^ model.ShowAtSingleDay == new DateTime(1, 1, 1, 0, 0, 0)))
            {
                ViewData["ShowAtError"] = "Please insert either Set as routine or Show once";

                return View(model);
            }
            if(model.ShowAtSingleDay < DateTime.Today && model.RoutineOption == 0)
            {
                ViewData["ShowAtError"] = "Please enter future date";

                return View(model);
            }
            if (!(ModelState.IsValid))
            {
                return View(model);
            }

            _db.toDoModels.Add(model);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Edit
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var getRecord = _db.toDoModels.FirstOrDefault(x => x.Id == Id);

            return View(getRecord);
        }

        [HttpPost]
        public IActionResult Edit(ToDoModel model)
        {
            if (!(model.RoutineOption == 0 ^ model.ShowAtSingleDay == new DateTime(1, 1, 1, 0, 0, 0)))
            {
                ViewData["ShowAtError"] = "Please insert either Set as routine or Show once";

                return View(model);
            }
            if (model.ShowAtSingleDay < DateTime.Now)
            {
                ViewData["ShowAtError"] = "Please enter future date";
            }
            if (!(ModelState.IsValid))
            {
                return View(model);
            }

            _db.toDoModels.Update(model);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Completed(int Id)
        {
            var getRecord = _db.toDoModels.FirstOrDefault(x => x.Id == Id);

            if(getRecord != null)
            {
                if (getRecord.IsItDone == true)
                {
                    getRecord.IsItDone = false;
                }
                else if (getRecord.IsItDone == false)
                {
                    getRecord.IsItDone = true;
                }

                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Remove(int Id)
        {
            var getRecord = _db.toDoModels.FirstOrDefault(x => x.Id == Id);

            if(getRecord != null)
            {
                _db.toDoModels.Remove(getRecord);
                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ManageAll()
        {
            IEnumerable<ToDoModel> getAllToDos = _db.toDoModels.Select(x => x);

            return View(getAllToDos);
        }
    }
}
