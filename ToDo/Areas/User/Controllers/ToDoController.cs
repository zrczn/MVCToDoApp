using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using ToDo.ApplicationDBContext;
using ToDo.DataAccess.Repository.IRepository;
using ToDo.Models;
using ToDo.Utility;
using ToDoScheduler = ToDo.Utility.TaskScheduler;

namespace ToDo.Areas.User.Controllers
{
    [Area("User")]
    public class ToDoController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ToDoController(IUnitOfWork unit)
        {
            _unitOfWork = unit;
        }

        public IActionResult Index()
        {
            _unitOfWork.ToDoRepository.CleanExpiredDateData();

            IEnumerable<ToDoModel> getTodayToDos = _unitOfWork.ToDoRepository.GetTodayToDos();

            return View(getTodayToDos);
        }

        public IActionResult SevenDaysSchedule()
        {
            IEnumerable<(string, IEnumerable<ToDoModel>)> getSevenDaysToDos = _unitOfWork.ToDoRepository.GetSevenDayToDos();

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
            if (!(model.RoutineOption == 0 ^ model.ShowAtSingleDay == new DateTime(1, 1, 1, 0, 0, 0)))
            {
                ViewData["ShowAtError"] = "Please insert either Set as routine or Show once";

                return View(model);
            }
            if (model.ShowAtSingleDay < DateTime.Today && model.RoutineOption == 0)
            {
                ViewData["ShowAtError"] = "Please enter future date";

                return View(model);
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _unitOfWork.ToDoRepository.Add(model);
            _unitOfWork.Save();

            TempData["success"] = "new record has been added";

            return RedirectToAction("Index");
        }

        //Edit
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var getRecord = _unitOfWork.ToDoRepository.Get(x => x.Id == Id);

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
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model != _unitOfWork.ToDoRepository.Get(x => x.Id == model.Id))
            {
                _unitOfWork.ToDoRepository.Update(model);
                _unitOfWork.Save();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Completed(int Id)
        {
            var getRecord = _unitOfWork.ToDoRepository.Get(x => x.Id == Id);

            if (getRecord != null)
            {
                if (getRecord.IsItDone == true)
                {
                    getRecord.IsItDone = false;
                }
                else if (getRecord.IsItDone == false)
                {
                    getRecord.IsItDone = true;
                }

                _unitOfWork.Save();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Remove(int Id)
        {
            var getRecord = _unitOfWork.ToDoRepository.Get(x => x.Id == Id);

            if (getRecord != null)
            {
                _unitOfWork.ToDoRepository.Remove(getRecord);
                _unitOfWork.Save();

            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ManageAll()
        {
            IEnumerable<ToDoModel> getAllToDos = _unitOfWork.ToDoRepository.GetAll();

            return View(getAllToDos);
        }
    }
}
