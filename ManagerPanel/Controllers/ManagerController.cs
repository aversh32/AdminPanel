using System;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Core.Specification;
using DataAccess.Core.Specification.Filter;
using DataAccess.Core.Specification.Order;
using Diss.Core;
using Diss.Core.DataServices;
using Diss.Core.DataServices.JoinSpec;
using Diss.Core.Enums;
using Diss.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebService.Models;

namespace ManagerPanel.Controllers
{
    public class ManagerController : Controller
    {
        private readonly ITasksDataService _tasksSvc;
        private readonly IUsersService _usersSvc;
        private readonly IUserTaskService _userTaskSvc;
        private const int PageSize = 20;

        private readonly DissContext _context;

        public ManagerController(ITasksDataService tasksSvc, IUsersService usersSvc, IUserTaskService userTaskSvc, DissContext context)
        {
            _tasksSvc = tasksSvc;
            _usersSvc = usersSvc;
            _userTaskSvc = userTaskSvc;
            _context = context;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction("NewTasksList");
        }

        /// <summary>
        /// Список новых задач
        /// </summary>
        [HttpGet]
        public async Task<ViewResult> NewTasksList(int? page = null)
       {

            var data = (await _tasksSvc.GetItemsList(new QuerySpec<Diss.Core.Models.Task>
            {
                Filter = new QueryFilterBase<Diss.Core.Models.Task>(x => x.StatusId == (int)StatusesEnum.WaitingModeration),
                Paging = null,
                Join = new TasksJoinSpec(),
                Order = new QueryOrderBase<Diss.Core.Models.Task>(x => x.OrderBy(с => с.CreatedAt))
            })).ToList();

           ViewBag.page = "newTasks";
           return View("TasksList", data);
        }      

        [HttpGet]
        public async Task<IActionResult> EditNewTask(int? id)
        {
            var item = await _tasksSvc.GetItem(new GetByIdSpec<Diss.Core.Models.Task>()
            {
                Id = id.GetValueOrDefault(),
                Join = new TasksJoinSpec()
            });

            return View("EditNewTask", item);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> EditNewTask([FromRoute]int id, [FromForm] Diss.Core.Models.Task task)
        {
            if (ModelState.IsValid)
            {
                task.StatusId = (int) StatusesEnum.WaitingForAppropriation;
                task.UpdatedAt = DateTime.Now;
                task = await _tasksSvc.UpsertAndSave(task);
                return RedirectToAction("NewTasksList");
            }

            return RedirectToAction("NewTasksList");
        }

        /// <summary>
        /// Список задач, требующих назначения аналитика и координатора
        /// </summary>
        [HttpGet]
        public async Task<ViewResult> TasksForAppropriationList(int? page = null)
        {

            var data = (await _tasksSvc.GetItemsList(new QuerySpec<Diss.Core.Models.Task>
            {
                Filter = new QueryFilterBase<Diss.Core.Models.Task>(x => x.StatusId == (int)StatusesEnum.WaitingForAppropriation),
                Paging = null,
                Join = new TasksJoinSpec(),
                Order = new QueryOrderBase<Diss.Core.Models.Task>(x => x.OrderBy(с => с.CreatedAt))
            })).ToList();

            ViewBag.page = "tasksForAppropriation";
            return View("TasksList", data);
        }

        [HttpGet]
        public async Task<IActionResult> EditAppropriationTask(int? id)
        {
            var item = await _tasksSvc.GetItem(new GetByIdSpec<Diss.Core.Models.Task>()
            {
                Id = id.GetValueOrDefault(),
            });

            var model = new TaskViewModel
            {
                Task = item,
                AnalystList = await _usersSvc.GetItemsList(new QuerySpec<User>()
                {
                    Filter = new QueryFilterBase<User>(x => x.UserRoles.Select(r => r.RoleId).Contains((int) RolesEnum.Analyst)),
                }),
                CoordinatorList = await _usersSvc.GetItemsList(new QuerySpec<User>()
                {
                    Filter = new QueryFilterBase<User>(x => x.UserRoles.Select(r => r.RoleId).Contains((int) RolesEnum.Coordinator)),
                })
            };

            return View("EditAppropriationTask", model);
        }

        [HttpPost]
        public async Task<IActionResult> EditAppropriationTask([FromRoute]int id, [FromForm] TaskViewModel model)
        {
            if (ModelState.IsValid)
            {
                var task = model.Task;
                task.StatusId = (int)StatusesEnum.ReviewByAnalists;
               // task.updated_at = DateTime.Now;
                task = await _tasksSvc.UpsertAndSave(task);

                var userTaskAnalyst = new UserTask()
                {
                    TaskId = task.Id,
                  //  CreatedAt = DateTime.Now,
                 //   updated_at = DateTime.Now,
                    UserId = model.AnalystId,
                    RoleId = (int)RolesEnum.Analyst
                };

                var userTaskCoordinator = new UserTask()
                {
                    TaskId = task.Id,
                    //created_at = DateTime.Now,
                    //updated_at = DateTime.Now,
                    UserId = model.CoordinatorId,
                    RoleId = (int)RolesEnum.Coordinator
                };

                _userTaskSvc.AddItem(userTaskAnalyst);
                _userTaskSvc.AddItem(userTaskCoordinator);
                await _userTaskSvc.Repository.SaveChangesAsync();

                return RedirectToAction("TasksForAppropriationList");
            }

            return RedirectToAction("TasksForAppropriationList");
        }

        /// <summary>
        /// Список текущих задач
        /// </summary>
        [HttpGet]
        public async Task<ViewResult> CurrentTasksList(int? page = null)
        {
            var data = (await _tasksSvc.GetItemsList(new QuerySpec<Diss.Core.Models.Task>
            {
                Filter = new QueryFilterBase<Diss.Core.Models.Task>(x => x.StatusId != (int)StatusesEnum.Completed),
                Paging = null,
                Join = new TasksJoinSpec(),
                Order = new QueryOrderBase<Diss.Core.Models.Task>(x => x.OrderBy(с => с.CreatedAt))
            })).ToList();

            return View("TasksList", data);
        }

        /// <summary>
        /// Список решенных задач
        /// </summary>
        [HttpGet]
        public async Task<ViewResult> CompletedTasksList(int? page = null)
        {

            var data = (await _tasksSvc.GetItemsList(new QuerySpec<Diss.Core.Models.Task>
            {
                Filter = new QueryFilterBase<Diss.Core.Models.Task>(x => x.StatusId == (int)StatusesEnum.Completed),
                Paging = null,
                Join = new TasksJoinSpec(),
                Order = new QueryOrderBase<Diss.Core.Models.Task>(x => x.OrderBy(с => с.CreatedAt))
            })).ToList();

            return View("TasksList", data);
        }
    }
}
