using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using TestingService.BLL.Services;

namespace TestingService.WebApplication.Security
{
    public class CustomAuthorizeAttribute:AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (string.IsNullOrEmpty(SessionPersister.Username))
            {
                filterContext.Result =
                    new RedirectToRouteResult(new RouteValueDictionary(
                        new { controller = "Account", action = "Index" }));
            }
            else
            {
                var service = System.Web.Mvc.DependencyResolver.Current.GetService(typeof(UserService)) as UserService;
                CustomPrincipal principal = new CustomPrincipal(service.GetByLogin(SessionPersister.Username));
                //FormsAuthentication.SetAuthCookie(SessionPersister.Username, true);
                if (!principal.IsInRole(Roles))
                {
                    filterContext.Result =
                    new RedirectToRouteResult(new RouteValueDictionary(
                        new { controller = "Account", action = "AccessDenied" }));
                }
            }
        }
    }
}