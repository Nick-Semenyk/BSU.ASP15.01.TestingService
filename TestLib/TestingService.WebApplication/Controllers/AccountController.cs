using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestingService.BLL.Interface.Entities;
using TestingService.BLL.Interface.Services;
using TestingService.BLL.Services;
using TestingService.ORM;
using TestingService.WebApplication.Models;
using TestingService.WebApplication.Security;

namespace TestingService.WebApplication.Controllers
{
    public class AccountController : Controller
    {
        private IUserService userService;
        private IRoleService roleService = System.Web.Mvc.DependencyResolver.Current.GetService(typeof(RoleService)) as RoleService;

        public AccountController(IUserService service)
        {
            userService = service;
        }

        // GET: Account
        public ActionResult Index()
        {
            if (!SessionPersister.IsInRole("user"))
            {
                return Redirect("/Home/Index");
            }
            UserEntity user = userService.GetByLogin(SessionPersister.Username);
            AccountViewModel avm = new AccountViewModel()
            {
                Login = user.Login,
                Email = user.Email,
                FirstName = user.FirstName,
                SecondName = user.SecondName,
                ThirdName = user.ThirdName
            };
            return View(avm);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(AccountViewModel avm)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid data");
                return View(avm);
            }
            UserEntity user = new UserEntity()
            {
                Login = avm.Login,
                Email = avm.Email,
                PasswordHash = userService.GetHash(avm.Password),
                FirstName = avm.FirstName,
                SecondName = avm.SecondName,
                ThirdName = avm.ThirdName
            };
            if (userService.GetByPredicate(entity => entity.Login == user.Login || entity.Email == user.Email).Any())
            {
                ModelState.AddModelError("", "This login already used");
                return View(avm);
            }
            RoleEntity role = roleService.GetByPredicate(entity => entity.Name == "user").First();
            userService.CreateUser(user);
            user = userService.GetByLogin(user.Login);
            userService.SetRole(user, role);
            SessionPersister.Username = user.Login;
            return Redirect("/Home/Index");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(AuthenticationViewModel avm)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid data");
                return View(avm);
            }
            UserEntity user = new UserEntity()
            {
                Login = avm.Login,
                PasswordHash = userService.GetHash(avm.Password)
            };
            if (userService.ValidAuthentication(user.Login, user.PasswordHash))
            {
                SessionPersister.Username = user.Login;
                return Redirect("/Home/Index");
            }
            else
            {
                ModelState.AddModelError("", "Wrong login or password");
                return View(avm);
            }
        }

        [CustomAuthorize(Roles = "user")]
        public ActionResult Logout()
        {
            SessionPersister.Username = "";
            return Redirect("/Home/Index");
        }


    }
}