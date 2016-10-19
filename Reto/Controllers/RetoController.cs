using Reto.Entidades;
using Reto.LogicaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Reto.Controllers
{
    public class RetoController : BaseController
    {
        brJuego obrJuego;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Lista()
        {
            return View();
        }

        public ActionResult Registro()
        {
            return View();
        }        

        public JsonResult Juego(string Nivel)
        {
            obrJuego = new brJuego();
            var juego = obrJuego.ExtraerJuegoAzar(Nivel);

            return Json(juego, JsonRequestBehavior.AllowGet);            
        }

        [HttpPost]
        public JsonResult GuardarScore(Alumno_Juego score)
        {
            obrJuego = new brJuego();
            var result = obrJuego.GuardarScore(score, SessionHelper.Usuario.IdPersona);
            return Json(result);
        }

        //Metodos
        [HttpPost]
        public int crear(Juego be)
        {
            obrJuego = new brJuego();
            return obrJuego.Registro(be);
        }

        [HttpPost]
        public JsonResult ListarPorId(int id)
        {
            obrJuego = new brJuego();
            return Json(obrJuego.ListarPorId(id));
        }

        [HttpPost]
        public JsonResult Listado()
        {
            obrJuego = new brJuego();
            return Json(obrJuego.Listado());
        }

        [HttpPost]
        public JsonResult CreateId(string Nivel)
        {
            obrJuego = new brJuego();
            return Json(obrJuego.CreateId(Nivel));
        }
	}
}