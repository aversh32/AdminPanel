using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Core.Specification;
using DataAccess.Core.Specification.Filter;
using DataAccess.Core.Specification.Order;
using Diss.Core.DataServices;
using Diss.Core.DataServices.JoinSpec;
using Diss.Core.Enums;
using Diss.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebService.Controllers
{
    public class UserController : Controller
    {
        private readonly IUsersService _usersSvc;

        public UserController(IUsersService usersSvc)
        {
            _usersSvc = usersSvc;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToAction("MemberList");
        }

        [HttpGet]
        public async Task<ViewResult> MemberList(int? page = null)
        {
            var data = (await _usersSvc.GetItemsList(new QuerySpec<User>
            {
                Order = new QueryOrderBase<User>(x => x.OrderBy(u => u.Email)),
                Join = new UserJoinSpec()
            })).ToList();
            return View("UserList", data);
        }
    }
}
