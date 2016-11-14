using Reto.AccesoDatos;
using Reto.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reto.LogicaNegocio
{
    public class brPreguntas
    {
        daPreguntas odaPreguntas;

        public int Registrar(Preguntas objEN)
        {
            odaPreguntas = new daPreguntas();
            return odaPreguntas.Registrar(objEN);
        }

        public Preguntas ObtenerPorId(int id)
        {
            odaPreguntas = new daPreguntas();
            return odaPreguntas.ObtenerPorId(id);
        }

        public List<Preguntas> Listar()
        {
            odaPreguntas = new daPreguntas();
            return odaPreguntas.Listar();
        }

        public List<Preguntas> ListarAzar(int cantidad)
        {
            var preguntas = Listar();
            int max = preguntas.Count;

            if (max < cantidad) return preguntas;
            else
            {
                //Extraemos al azar
                Random rnd = new Random();
                List<Preguntas> cuestionario = preguntas.OrderBy(x => rnd.Next()).Take(cantidad).ToList();
                return cuestionario;
            }
        }

        public bool Eliminar(int id)
        {
            odaPreguntas = new daPreguntas();
            return odaPreguntas.Eliminar(id);
        }

        public int RegistrarScore(List<PreguntasScore> score, int IdPersona)
        {            
            odaPreguntas = new daPreguntas();            
            return odaPreguntas.RegistrarScore(score, IdPersona);
        }
    }
}
