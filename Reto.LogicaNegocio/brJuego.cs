﻿using Reto.AccesoDatos;
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

        public Juego ExtraerJuegoAzar(string Nivel)
        {
            return odaJuego.ExtraerJuegoAzar(Nivel);
        }

        public bool GuardarScore(Alumno_Juego score, int IdPersona)
        {
            return odaJuego.GuardarScore(score, IdPersona);
        }
    }
}
