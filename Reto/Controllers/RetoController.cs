﻿using Reto.Entidades;
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

        [ActionName("gestionar-juegos")]
        public ActionResult GestionarJuegos()
        {
            return View();
        }

        public ActionResult Lista()
        {
            return View();
        }

        public ActionResult Modulos()
        {
            return View();
        }

        public JsonResult SeleccionarModulo(int Modulo)
        {
            Session["Modulo"] = Modulo;
            return Json(true);
        }

        [ActionName("mayor-menor")]
        public ActionResult MayorMenor()
        {
            return View();
        }

        public JsonResult Juego(int Nivel)
        {
            obrJuego = new brJuego();
            var juego = obrJuego.ExtraerJuegoAzar(Nivel);

            return Json(juego, JsonRequestBehavior.AllowGet);            
        }

        [HttpPost]
        public JsonResult GuardarScore(JuegoScore score, int nivel)
        {
            obrJuego = new brJuego();
            var result = obrJuego.GuardarScore(score, SessionHelper.Usuario.IdPersona, nivel);
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