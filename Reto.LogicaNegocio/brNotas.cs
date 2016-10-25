﻿using Reto.AccesoDatos;
using Reto.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reto.LogicaNegocio
{
    public class brNotas
    {
        private daNotas odaNotas;

        public brNotas()
        {
            odaNotas = new daNotas();
        }

        public List<CriteriosEvaluacion> CriteriosEvaluacionSelect()
        {
            return odaNotas.CriteriosEvaluacionSelect();
        }

        public usp_DocenteSelect_Result DocenteSelect(int IdPersona)
        {
            return odaNotas.DocenteSelect(IdPersona);
        }

        public List<usp_NotasSelect_Result> NotasSelect(int IdNivelEscolar, int Grado, string Seccion, int Bimestre)
        {
            return odaNotas.NotasSelect(IdNivelEscolar, Grado, Seccion, Bimestre);
        }

        public int GuardarNotas(int IdNivelEscolar, int Grado, string Seccion, int Bimestre, List<NotasCriterio> criterios, List<Notas> notas)
        {
            return odaNotas.GuardarNotas(IdNivelEscolar, Grado, Seccion, Bimestre, criterios, notas);
        }
    }
}
