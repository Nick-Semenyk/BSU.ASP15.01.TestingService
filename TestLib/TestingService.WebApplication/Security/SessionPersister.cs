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
        public static string Username { get;set; }
        public static int Id { get; set; }
        public static string Email { get; set; }

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