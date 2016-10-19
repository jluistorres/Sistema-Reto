using Reto.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reto
{
    public class SessionHelper
    {
        public static usp_UsuarioLogin_Result Usuario
        {
            get
            {
                usp_UsuarioLogin_Result user = (usp_UsuarioLogin_Result)HttpContext.Current.Session["Usuario"];

                if (user == null)
                {
                    user = new usp_UsuarioLogin_Result();
                }

                return user;
            }
            set
            {
                HttpContext.Current.Session["Usuario"] = value;
            }
        }

        public static List<string> Roles
        {
            get
            {
                List<string> roles = (List<string>)HttpContext.Current.Session["Roles"];
                if (roles == null)
                {
                    roles = new List<string>();
                }
                return roles;
            }
            set
            {
                HttpContext.Current.Session["Roles"] = value;
            }
        }

        public static bool IsInRole(string Rol)
        {
            bool authorize = Roles.Contains(Rol);

            return authorize;
        }
    }
}