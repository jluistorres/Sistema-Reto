using Reto.Entidades;
using Reto.LogicaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Reto.Controllers
{
    [CustomAuthorize(Roles = "Docente")]
    public class NotasController : BaseController
    {
        brNotas obrNotas;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Registro()
        {
            return View();
        }

        public JsonResult CargarDatosDocente(int IdPersona)
        {
            obrNotas = new brNotas();
            var result = obrNotas.DocenteCursoSelect(IdPersona);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarRegistro(int Grado, string Seccion, int IdCurso)
        {
            obrNotas = new brNotas();
            var result = obrNotas.RegistroNotasSelect(Grado, Seccion, IdCurso);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public int GuardarNotas(DocenteCurso dc, List<NotasCriterio> notas)
        {
            obrNotas = new brNotas();
            return obrNotas.GuardarNotas(dc, notas);
        }
    }
}