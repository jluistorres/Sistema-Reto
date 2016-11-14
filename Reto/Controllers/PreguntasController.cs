using Reto.Entidades;
using Reto.LogicaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Reto.Controllers
{
    public class PreguntasController : BaseController
    {
        //
        // GET: /Preguntas/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Gestionar()
        {
            return View();
        }

        public ActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public int Registro(Preguntas objEN)
        {
            brPreguntas obrPreguntas = new brPreguntas();
            return obrPreguntas.Registrar(objEN);
        }
	}
}