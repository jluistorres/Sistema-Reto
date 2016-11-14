using Reto.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reto.AccesoDatos
{
    public class daPreguntas
    {
        dbretoEntities cnx;

        public daPreguntas()
        {
            cnx = new dbretoEntities();
            cnx.Configuration.ProxyCreationEnabled = false;
        }

        public int Registrar(Preguntas objEN)
        {
            if (objEN.IdPregunta == 0)
            {
                cnx.Preguntas.Add(objEN);
            }
            else
            {
                var original = cnx.Preguntas.FirstOrDefault(x => x.IdPregunta == objEN.IdPregunta);
                cnx.Entry(original).CurrentValues.SetValues(objEN);
            }

            cnx.SaveChanges();

            return objEN.IdPregunta;
        }
    }
}
