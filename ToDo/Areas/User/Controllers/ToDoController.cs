using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Collections.Generic;
using System.Security.Claims;
using System.Xml.Linq;
using ToDo.ApplicationDBContext;
using ToDo.DataAccess.Repository.IRepository;
using ToDo.Filters;
using ToDo.Models;
using ToDo.Utility;

namespace ToDo.Areas.User.Controllers
{
    [Area("User")]
    public class ToDoController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ToDoController(IUnitOfWork unit, IWebHostEnvironment webHostEnvironment) : base()
        {
            _unitOfWork = unit;
            _webHostEnvironment = webHostEnvironment;
        }


        [ServiceFilter(typeof(UserIdAsyncFilter))]
        public IActionResult Index()
        {
            _unitOfWork.ToDoRepository.CleanExpiredDateData();

            if(HttpContext.Items.TryGetValue("CurrentUserId", out object value))
            {
                IEnumerable<ToDoModel> getTodayToDos = _unitOfWork.ToDoRepository.GetTodayToDos();

                getTodayToDos = getTodayToDos.Where(x => x.OwnerId == (string)value);

                return View(getTodayToDos);
            }
            else
            {
                IEnumerable<ToDoModel> getTodayToDos = _unitOfWork.ToDoRepository.GetTodayToDos();

                getTodayToDos = getTodayToDos.Where(x => x.OwnerId == (string)value);

                return View(getTodayToDos);
            }
        }


        [ServiceFilter(typeof(UserIdAsyncFilter))]
        public IActionResult SevenDaysSchedule()
        {
            if(HttpContext.Items.TryGetValue("CurrentUserId", out object value))
            {
                IEnumerable<(string, IEnumerable<ToDoModel>)> getSevenDaysToDos = _unitOfWork.ToDoRepository.GetSevenDayToDos(
                HttpContext.Items["CurrentUserId"].ToString());

                return View(getSevenDaysToDos);
            }

            return BadRequest();
        }


        //Create
        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return View(new ToDoModel());
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(ToDoModel model, IFormFile file)
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

            ModelState.Remove(nameof(file));


            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (ModelState.IsValid)
            {
                model.AudioId = 1;

                //Image handling
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(wwwRootPath, @"Images\Product");

                    using (var fileStream = new FileStream(Path.Combine(filePath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    PhotoModel newObj = new PhotoModel();

                    newObj.Name = fileName;
                    newObj.URL = @"\Images\Product\" + fileName;

                    _unitOfWork.photoRepository.Add(newObj);

                    model.Photo = newObj;
                }
                else
                {
                    model.PhotoId = 1;
                }

                string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                model.OwnedBy = _unitOfWork.applicationUser.Get(x => x.Id == currentUserId);
                
                _unitOfWork.ToDoRepository.Add(model);
                _unitOfWork.Save();

                TempData["success"] = "new record has been added";

                return RedirectToAction("Index");
            }

            return View();
        }

        //Edit
        [HttpGet]
        [Authorize]
        public IActionResult Edit(int Id)
        {
            var getRecord = _unitOfWork.ToDoRepository.Get(x => x.Id == Id, "Photo");

            if(getRecord != null)
                return View(getRecord);

            return NotFound();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(ToDoModel model, IFormFile file)
        {

            if (!(model.RoutineOption == 0 ^ model.ShowAtSingleDay == new DateTime(1, 1, 1, 0, 0, 0)))
            {
                ViewData["ShowAtError"] = "Please insert either Set as routine or Show once";

                return View(model);
            }
            if (model.ShowAtSingleDay < DateTime.Now && model.ShowAtSingleDay != new DateTime(1, 1, 1, 0, 0, 0))
            {
                ViewData["ShowAtError"] = "Please enter future date";
            }

            ModelState.Remove(nameof(file));

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (file != null)
            {
                var getOldPhoto = _unitOfWork.photoRepository.Get(x => x.Id == model.PhotoId);

                //Image handling
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(wwwRootPath, @"Images\Product");

                using (var fileStream = new FileStream(Path.Combine(filePath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                getOldPhoto.Name = fileName;
                getOldPhoto.URL = @"\Images\Product\" + fileName;

                _unitOfWork.photoRepository.Update(getOldPhoto);
                _unitOfWork.Save();

                model.Photo = getOldPhoto;

            }

            model.AudioId = 1;

            _unitOfWork.ToDoRepository.Update(model);
            _unitOfWork.Save();


            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize]
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
        [Authorize]
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
        [Authorize]
        public IActionResult ManageAll()
        {
            IEnumerable<ToDoModel> getAllToDos = _unitOfWork.ToDoRepository.GetAll("Photo");

            return View(getAllToDos);
        }
    }
}
