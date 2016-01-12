using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestingService.BLL.Interface.Entities;
using TestingService.BLL.Services;

namespace TestingService.WebApplication.Security
{
    public static class SessionPersister
    {
        private static string usernameSessionvar = "";

        public static string Username
        {
            get
            {
                if (HttpContext.Current == null)
                    return string.Empty;
                var sessionVar = HttpContext.Current.Session[usernameSessionvar];
                return sessionVar as string;
            }
            set { HttpContext.Current.Session[usernameSessionvar] = value; }
        }

        public static bool IsInRole(string role)
        {
            if (string.IsNullOrEmpty(Username))
                return false;
            var userService = System.Web.Mvc.DependencyResolver.Current.GetService(typeof(UserService)) as UserService;
            var roleService = System.Web.Mvc.DependencyResolver.Current.GetService(typeof(RoleService)) as RoleService;

            UserEntity user = userService.GetByLogin(Username);
            IEnumerable<RoleEntity> roles = roleService.GetUsersRoles(user);
            if (roles.Any(entity => entity.Name == role))
            {
                return true;
            }
            return false;
        }
    }
}