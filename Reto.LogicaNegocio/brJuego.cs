using Reto.AccesoDatos;
using Reto.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reto.LogicaNegocio
{
    public class brJuego
    {
        private daJuego odaJuego;

        public brJuego()
        {
            odaJuego = new daJuego();
        }

        public int Registro(Juego be)
        {
            return odaJuego.Registro(be);
        }

        public Juego ListarPorId(int Id)
        {
            return odaJuego.ListarPorId(Id);
        }

        public List<Juego> Listado()
        {
            return odaJuego.Listado();
        }

        public int CreateId(string Nivel)
        {
            return odaJuego.CreateId(Nivel);
        }

        public Juego ExtraerJuegoAzar(int Nivel)
        {
            return odaJuego.ExtraerJuegoAzar(Nivel);
        }

        public bool GuardarScore(JuegoScore score, int IdPersona, int Nivel)
        {
            //int Bimestre = 1;
            //DateTime fecha = DateTime.Now;

            //if (fecha >= DateTime.Parse(string.Format("10/03/{0}", fecha.Year)) && fecha <= DateTime.Parse(string.Format("02/05/{0}", fecha.Year)))
            //{
            //    Bimestre = 1;
            //}

            return odaJuego.GuardarScore(score, IdPersona, Nivel);
        }
    }
}
