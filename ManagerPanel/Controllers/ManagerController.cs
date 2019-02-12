using System;
using System.Collections.Generic;
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
using ManagerPanel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManagerPanel.Controllers
{
    [Authorize(Roles = "Manager")]
    public class ManagerController : Controller
    {
        private readonly ITasksDataService _tasksSvc;
        private readonly IUsersService _usersSvc;
        private readonly IUserTaskService _userTaskSvc;
        private readonly IUserRoleService _userRoleSvc;
        private readonly IRolesService _rolesSvc;
        private const int PageSize = 20;

        private readonly DissContext _context;

        public ManagerController(ITasksDataService tasksSvc, IUsersService usersSvc, IUserTaskService userTaskSvc, IUserRoleService userRoleSvc, IRolesService rolesSvc, DissContext context)
        {
            _tasksSvc = tasksSvc;
            _usersSvc = usersSvc;
            _userTaskSvc = userTaskSvc;
            _userRoleSvc = userRoleSvc;
            _rolesSvc = rolesSvc;
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
                AnalystList = await _usersSvc.GetItemsList(new QuerySpec<User>
                {
                    Filter = new QueryFilterBase<User>(x => x.UserRoles.Select(r => r.RoleId).Contains((int) RolesEnum.Analyst)),
                }),
                CoordinatorList = await _usersSvc.GetItemsList(new QuerySpec<User>
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
             
                var userTaskAnalyst = new UserTask
                {
                    TaskId = task.Id,
                    UserId = model.AnalystId,
                    RoleId = (int)RolesEnum.Analyst,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                var userTaskCoordinator = new UserTask
                {
                    TaskId = task.Id,
                    UserId = model.CoordinatorId,
                    RoleId = (int)RolesEnum.Coordinator,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };

                try
                {
                    userTaskAnalyst = await _userTaskSvc.UpsertAndSave(userTaskAnalyst);
                    userTaskCoordinator = await _userTaskSvc.UpsertAndSave(userTaskCoordinator);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
             

                task.StatusId = (int)StatusesEnum.ReviewByAnalists;
                task.UpdatedAt = DateTime.Now;

                try
                {
                    task = await _tasksSvc.UpsertAndSave(task);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
               

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

        /// <summary>
        /// Заявки на вступление в сообщество
        /// </summary>
        [HttpGet]
        public async Task<ViewResult> ApplicationForJoinList(int? page = null)
        {

            return View("Index");
        }

        /// <summary>
        /// Регистрация пользователей вручную
        /// </summary>
        [HttpGet]
        public async Task<ViewResult> RegisterUser()
        {
            var roles = await _rolesSvc.GetItemsList(new QuerySpec<Role>());
            var userViewModel = new RegisterUserViewModel()
            {
                User = new User(),
                Roles = roles
            };

            return View("RegisterUser", userViewModel);
        }

        /// <summary>
        /// Регистрация пользователей вручную
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromForm] RegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                //todo сообщение при повторяющемся email
                model.User.CreatedAt = DateTime.Now;
                model.User.UpdatedAt = DateTime.Now;
                var user = await _usersSvc.UpsertAndSave(model.User);
                //для каждой роли создаем запись в таблице UserRole
                foreach (var roleId in model.SelectedRoles)
                {
                    var userRole = new UserRole()
                    {
                        RoleId = int.Parse(roleId),
                        UserId = user.Id,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    };

                    _userRoleSvc.UpsertItem(userRole);
                }

                await _context.SaveChangesAsync();
            }
         
            return RedirectToAction("RegisterUser");
        }
    }
}
