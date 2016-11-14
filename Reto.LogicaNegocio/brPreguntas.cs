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
    }
}
