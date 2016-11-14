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

        [CustomAuthorize(Roles = "Docente")]
        public ActionResult Gestionar()
        {
            brPreguntas obrPreguntas = new brPreguntas();
            var result = obrPreguntas.Listar();

            if (TempData["Eliminado"] != null)
            {
                ViewBag.Eliminado = TempData["Eliminado"];
            }

            return View(result);
        }

        public ActionResult Eliminar(int id)
        {
            brPreguntas obrPreguntas = new brPreguntas();
            var result = obrPreguntas.Eliminar(id);

            if (result)
            {
                TempData["Eliminado"] = "Se ha eliminado la pregunta";
            }

            return RedirectToAction("Gestionar");
        }

        public ActionResult Registro(int? id)
        {
            ViewBag.id = id;
            return View();
        }

        [HttpPost]
        public int Registro(Preguntas objEN)
        {
            brPreguntas obrPreguntas = new brPreguntas();
            return obrPreguntas.Registrar(objEN);
        }

        public JsonResult ObtenerPreguntaById(int id)
        {
            brPreguntas obrPreguntas = new brPreguntas();
            var result = obrPreguntas.ObtenerPorId(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarAzar()
        {
            brPreguntas obrPreguntas = new brPreguntas();
            var result = obrPreguntas.ListarAzar(10);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public int RegistrarScore(List<PreguntasScore> score)
        {
            brPreguntas obrPreguntas = new brPreguntas();
            return obrPreguntas.RegistrarScore(score, SessionHelper.Usuario.IdPersona);
        }
    }
}