using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Reto
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //base.OnAuthorization(filterContext);

            bool authorize = false;

            if (SessionHelper.Roles.Count > 0)
            {
                string[] allowedroles = Roles.Split(new char[] { ',' });

                foreach (string rol in allowedroles)
                {
                    if (SessionHelper.Roles.Contains(rol.Trim()))
                    {
                        authorize = true;
                        break;
                    }
                }
            }

            if (!authorize)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    // Forbidden
                    filterContext.HttpContext.Response.StatusCode = 403;
                    filterContext.HttpContext.Response.End();
                }
                else
                {
                    filterContext.Result = new RedirectResult("~/Error/Restringido");
                }
            }
        }
    }
}