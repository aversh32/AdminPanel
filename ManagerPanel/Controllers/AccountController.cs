using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DataAccess.Core.Specification;
using DataAccess.Core.Specification.Filter;
using Diss.Core.DataServices;
using Diss.Core.DataServices.JoinSpec;
using Diss.Core.Enums;
using Diss.Core.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using WebService.Models;

namespace WebService.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUsersService _usersService;
        public AccountController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = (await _usersService.GetItemsList(new QuerySpec<User>()
                    {
                        Filter = new QueryFilterBase<User>(x => x.UserRoles.Any(ur => ur.RoleId == (int)RolesEnum.Manager)),
                        Join = new UserJoinSpec()
                    })).FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);
                if (user != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Email),
                        new Claim(ClaimTypes.Role, "admin")
                    };
                    ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "login");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                    await HttpContext.SignInAsync(principal);
                    return RedirectToAction("Index", "Manager");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль, или Вы не являетесь пользователем с ролью \"Куратор экспертного сообщества\".");
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
