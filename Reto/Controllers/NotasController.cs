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

        public JsonResult CargarDatosInicial()
        {
            obrNotas = new brNotas();
            var docente = obrNotas.DocenteSelect(SessionHelper.Usuario.IdPersona);
            var criterios = obrNotas.CriteriosEvaluacionSelect();
            return Json(new
            {
                Docente = docente,
                Criterios = criterios
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarRegistro(int IdNivelEscolar, int Grado, string Seccion, int Bimestre)
        {
            obrNotas = new brNotas();
            var result = obrNotas.NotasSelect(IdNivelEscolar, Grado, Seccion, Bimestre);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public int GuardarNotas(int IdNivelEscolar, int Grado, string Seccion, int Bimestre, List<NotasCriterio> criterios, List<Notas> notas)
        {
            obrNotas = new brNotas();

            var docente = obrNotas.DocenteSelect(SessionHelper.Usuario.IdPersona);
            if (docente == null) return 0;

            var fecha = DateTime.Now;

            if (criterios != null)
            {
                foreach (var item in criterios)
                {
                    item.IdDocente = docente.IdDocente;
                    item.Anyo = fecha.Year;
                    item.Fecha = fecha;
                }
            }

            if (notas != null)
            {
                foreach (var item in notas)
                {
                    item.IdDocente = docente.IdDocente;
                    item.Anyo = fecha.Year;
                    item.Fecha = fecha;
                }
            }

            return obrNotas.GuardarNotas(IdNivelEscolar, Grado, Seccion, Bimestre, criterios, notas);
        }

        public int RegistrarObservacion(Notas objEN)
        {
            obrNotas = new brNotas();
            var result = obrNotas.RegistrarObservaciones(objEN);
            return result;
        }
    }
}