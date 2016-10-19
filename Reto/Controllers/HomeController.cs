using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Reto.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Metodo realizado para probar errores con elmah
        /// </summary>
        public void forzarerror()
        {
            int numero = Convert.ToInt32("number");
        }
	}
}