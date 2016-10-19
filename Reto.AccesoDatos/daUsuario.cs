using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reto.Entidades;

namespace Reto.AccesoDatos
{
    public class daUsuario
    {
        dbretoEntities cnx;

        public daUsuario()
        {
            cnx = new dbretoEntities();
            cnx.Configuration.ProxyCreationEnabled = false;
        }

        public usp_UsuarioLogin_Result ValidarUsuario(string idUsuario, string clave)
        {
            var result = this.cnx.Database.SqlQuery<usp_UsuarioLogin_Result>("usp_UsuarioLogin @idUsuario = {0}, @clave = {1}", idUsuario, clave);
            return result.FirstOrDefault();
        }
    }
}
