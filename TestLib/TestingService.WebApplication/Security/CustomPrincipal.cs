using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using TestingService.BLL.Interface.Entities;
using TestingService.BLL.Interface.Services;
using TestingService.BLL.Services;

namespace TestingService.WebApplication.Security
{
    public class CustomPrincipal : IPrincipal
    {
        private UserEntity user;
        private RoleService roleService = System.Web.Mvc.DependencyResolver.Current.GetService(typeof(RoleService)) as RoleService;
        private IUserService userService =
            System.Web.Mvc.DependencyResolver.Current.GetService(typeof(UserService)) as UserService;

        public CustomPrincipal(UserEntity user)
        {
            this.user = user;
            this.Identity = new GenericIdentity(user.Login);
        }

        public CustomPrincipal(string login)
        {
            user = userService.GetByLogin(login);
            this.Identity = new GenericIdentity(login);
        }

        public string Login => user.Login;
        public string Email => user.Email;

        public bool IsInRole(string role)
        {
            var roles = role.Split(new char[] { ',' });
            IEnumerable<RoleEntity> usersRoles = roleService.GetUsersRoles(user);
            foreach (string r in roles)
            {
                if (!usersRoles.Any(entity => entity.Name == r))
                    return false;
            }
            return true;
        }

        public IIdentity Identity { get; set; }
    }
}