using Reto.Entidades;
using Reto.LogicaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Reto.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login(string ReturnUrl)
        {
            Session.Clear();
            ViewBag.ReturnUrl = ReturnUrl;

            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password, string ReturnUrl)
        {
            brUsuario obrUsuario = new brUsuario();
            usp_UsuarioLogin_Result oUsuario = obrUsuario.ValidarUsuario(username, password);

            if (oUsuario != null)
            {
                SessionHelper.Usuario = oUsuario;
                SessionHelper.Roles = new List<string>() { oUsuario.Rol };

                if (ReturnUrl != null && ReturnUrl != string.Empty)
                {
                    return Redirect(Url.Content(ReturnUrl));
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Mensaje = "El usuario y/o contraseña son incorrectos";
            }

            return View();
        }
    }
}