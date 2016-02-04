using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Web.UI.WebControls;
using TestingService.BLL.Interface.Entities;
using TestingService.BLL.Interface.Services;
using TestingService.BLL.Services;
using TestingService.ORM;
using TestingService.WebApplication.Infrastructure.Mappers;
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

        [HttpGet]
        public ActionResult ShowProfile(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            UserViewModel e = userService.GetUserEntity(id.Value).ToMvcUser();
            if (e == null)
            {
                //!404.g
                return Redirect("/Home/Index");
            }
            return View(e);
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
            LogIn(user.Login);
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
                LogIn(user.Login);
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
            /*  FormsAuthentication.SignOut();
              Session.Abandon();
              // clear authentication cookie
              HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
              cookie1.Expires = DateTime.Now.AddYears(-1);
              cookie1.Path = FormsAuthentication.FormsCookiePath;
              Response.Cookies.Add(cookie1);
              // clear session cookie (not necessary for your current problem but i would recommend you do it anyway)
              HttpCookie cookie2 = new HttpCookie("ASP.NET_SessionId", "");
              cookie2.Expires = DateTime.Now.AddYears(-1);
              Response.Cookies.Add(cookie2);*/
            UserEntity user = userService.GetByLogin(SessionPersister.Username);
            CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel();
            serializeModel.Id = user.Id;
            serializeModel.Email = user.Email;
            serializeModel.Login = user.Login;

            JavaScriptSerializer serializer = new JavaScriptSerializer();

            string userData = serializer.Serialize(serializeModel);

            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                     1,
                     serializeModel.Login,
                     DateTime.Now,
                     DateTime.Now.AddDays(-1),
                     false,
                     userData);

            string encTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            Response.Cookies.Add(faCookie);


            SessionPersister.Username = "";
            return Redirect("/Home/Index");
        }

        //[ValidateAntiForgeryToken]
        [HttpPost]
        public JsonResult DoesEmailExist(string Email)
        {
            return Json(!userService.GetByPredicate(entity => entity.Email == Email).Any());
        }

        //[ValidateAntiForgeryToken]
        [HttpPost]
        public JsonResult DoesLoginExist(string Login)
        {
            return Json(userService.GetByLogin(Login) == null);
        }

        private void LogIn(string login)
        {
            UserEntity user = userService.GetByLogin(login);
            CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel();
            serializeModel.Id = user.Id;
            serializeModel.Email = user.Email;
            serializeModel.Login = user.Login;

            JavaScriptSerializer serializer = new JavaScriptSerializer();

            string userData = serializer.Serialize(serializeModel);

            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                     1,
                     serializeModel.Login,
                     DateTime.Now,
                     DateTime.Now.AddDays(1),
                     true,
                     userData);

            string encTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            Response.Cookies.Add(faCookie);
        }
    }
}