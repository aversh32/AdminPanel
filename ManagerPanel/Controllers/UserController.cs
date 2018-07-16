using System.Linq;
using System.Threading.Tasks;
using DataAccess.Core.Specification;
using DataAccess.Core.Specification.Order;
using Diss.Core.DataServices;
using Diss.Core.DataServices.JoinSpec;
using Diss.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace ManagerPanel.Controllers
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
                Order = new QueryOrderBase<User>(x => x.OrderBy(u => u.LastName)),
                Join = new UserJoinSpec()
            })).ToList();
            return View("UserList", data);
        }

        [HttpGet]
        public async Task<ViewResult> GetUser([FromRoute]int id, int? page = null)
        {
            var user = await _usersSvc.GetItem(new GetByIdSpec<User>
            {
                Id = id,
                Join = new UserJoinSpec()
            });

            return View("UserInfo", user);
        }
    }
}
