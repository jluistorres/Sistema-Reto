using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Reto.Controllers
{
    public class BaseController : Controller
    {
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            if (requestContext.HttpContext.Session["Usuario"] == null)
            {
                string url = requestContext.HttpContext.Request.Url.AbsoluteUri;
                string ReturnUrl = "";
                if (url != "/")
                {
                    ReturnUrl = "?ReturnUrl=" + url;
                }

                requestContext.HttpContext.Response.Redirect("~/Account/Login" + ReturnUrl, true);
            }

            base.Initialize(requestContext);
        }

        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonResult()
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                MaxJsonLength = System.Int32.MaxValue
            };
        }
    }
}