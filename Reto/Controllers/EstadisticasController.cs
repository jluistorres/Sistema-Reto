using Reto.LogicaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Reto.Controllers
{
    public class EstadisticasController : BaseController
    {
        brNotas obrNotas;

        public ActionResult Juego()
        {
            return View();
        }

        public ActionResult SituacionAcademica()
        {
            return View();
        }

        public ActionResult Informe()
        {
            return View();
        }

        public JsonResult ListarRegistro(int IdNivelEscolar, int Grado, string Seccion, int Bimestre)
        {
            obrNotas = new brNotas();
            var result = obrNotas.NotasSelect(IdNivelEscolar, Grado, Seccion, Bimestre);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult HistorialNotas(int IdAlumno)
        {
            obrNotas = new brNotas();
            var result = obrNotas.HistorialNotas(IdAlumno);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ScoreHistorial(int? Modulo, int? IdJuego, DateTime FechaInicio, DateTime FechaFin)
        {
            brJuego obrJuego = new brJuego();
            var result = obrJuego.ScoreHistorial(Modulo, IdJuego, FechaInicio, FechaFin);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult HistorialJuego(int IdAlumno, int Bimestre)
        {
            brJuego obrJuego = new brJuego();
            var score = obrJuego.ScorePromedioBimestre(IdAlumno, Bimestre);

            obrNotas = new brNotas();
            var ranking = obrNotas.NotasAlumnoRanking(Bimestre, DateTime.Now.Year);

            return Json(new
            {
                Resumen = score,
                Ranking = ranking
            }, JsonRequestBehavior.AllowGet);
        }
    }
}