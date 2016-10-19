using Reto.AccesoDatos;
using Reto.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reto.LogicaNegocio
{
    public class brUsuario
    {
        daUsuario odaUsuario;

        public brUsuario()
        {
            odaUsuario = new daUsuario();
        }

        public usp_UsuarioLogin_Result ValidarUsuario(string username, string password)
        {
            return odaUsuario.ValidarUsuario(username, password);
        }
    }
}
