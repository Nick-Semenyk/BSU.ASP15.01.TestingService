using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using TestingService.BLL.Interface.Entities;
using TestingService.BLL.Services;

namespace TestingService.WebApplication.Security
{
    public class CustomPrincipal : IPrincipal
    {
        private UserEntity user;
        private RoleService roleService = System.Web.Mvc.DependencyResolver.Current.GetService(typeof(RoleService)) as RoleService;

        public CustomPrincipal(UserEntity user)
        {
            this.user = user;
            this.Identity = new GenericIdentity(user.Login);
        }

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