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

        public Preguntas ObtenerPorId(int id)
        {
            return cnx.Preguntas.FirstOrDefault(x => x.IdPregunta == id);
        }

        public List<Preguntas> Listar()
        {
            return cnx.Preguntas.ToList();
        }

        public bool Eliminar(int id)
        {
            var objEN = cnx.Preguntas.FirstOrDefault(x => x.IdPregunta == id);
            if (objEN != null)
            {
                cnx.Preguntas.Remove(objEN);
                return cnx.SaveChanges() != 0;
            }

            return false;
        }

        public int RegistrarScore(List<PreguntasScore> score, int IdPersona)
        {
            var alumno = cnx.Alumno.FirstOrDefault(x => x.IdPersona == IdPersona);

            if (alumno != null)
            {
                var fecha = DateTime.Now;

                foreach (var item in score)
                {
                    item.IdAlumno = alumno.IdAlumno;
                    item.FechaRegistro = fecha;
                }
                
                var lista = cnx.PreguntasScore.Where(x => x.IdAlumno == alumno.IdAlumno).ToList();
                cnx.PreguntasScore.RemoveRange(lista);
                cnx.SaveChanges();

                cnx.PreguntasScore.AddRange(score);
                return cnx.SaveChanges();
            }

            return 0;
        }
    }
}
