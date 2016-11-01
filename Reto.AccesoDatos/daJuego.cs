﻿using Reto.Entidades;
using Reto.Entidades.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reto.AccesoDatos
{
    public class daJuego
    {
        dbretoEntities cnx;

        public daJuego()
        {
            cnx = new dbretoEntities();
            cnx.Configuration.ProxyCreationEnabled = false;
        }

        public int Registro(Juego be)
        {
            if (be.IdJuego == 0)
            {
                cnx.Juego.Add(be);
            }
            else
            {
                var original = cnx.Juego.FirstOrDefault(x => x.IdJuego == be.IdJuego);
                cnx.Entry(original).CurrentValues.SetValues(be);
            }

            cnx.SaveChanges();
            return be.IdJuego;
        }

        public Juego ListarPorId(int Id)
        {
            return cnx.Juego.Where(x => x.IdJuego == Id).FirstOrDefault();
        }

        public List<Juego> Listado()
        {
            return cnx.Juego.ToList();
        }

        public int CreateId(string Nivel)
        {
            var result = this.cnx.Database.SqlQuery<int>("usp_CreateIdReto @Nivel = {0}", Nivel);
            return result.FirstOrDefault();
            //return (int)cnx.usp_CreateIdReto(Nivel).FirstOrDefault();
        }

        public Juego ExtraerJuegoAzar(int Nivel)
        {
            Juego juego = null;

            var result = cnx.Juego.Where(x => x.Nivel == Nivel).ToList();
            if (result.Count > 0)
            {
                int Azar = new Random().Next(0, result.Count);//result.Count - 1
                juego = result[Azar];
            }

            return juego;
        }

        public List<usp_ScoreSelectTop_Result> ScoreSelect(int Nivel, int IdAlumno)
        {
            var result = this.cnx.Database.SqlQuery<usp_ScoreSelectTop_Result>("usp_ScoreSelectTop @Nivel = {0}, @IdAlumno = {1}", Nivel, IdAlumno);
            return result.ToList();
        }

        public bool GuardarScore(JuegoScore score, int IdPersona, int Nivel)
        {
            var alumno = cnx.Alumno.FirstOrDefault(x => x.IdPersona == IdPersona);
            if (alumno != null)
            {
                score.IdAlumno = alumno.IdAlumno;

                var factor = cnx.JuegoScoreFactor.FirstOrDefault(x => x.Nivel == Nivel);

                var score_old = cnx.JuegoScore.Where(x => x.IdAlumno == score.IdAlumno && x.IdJuego == score.IdJuego).ToList();
                if (score_old != null && score_old.Count > 0)
                {
                    cnx.JuegoScore.RemoveRange(score_old);
                    cnx.SaveChanges();
                }                

                var fecha = DateTime.Now;

                //Calculo de puntaje
                score.Puntaje = factor.Base - ((score.TiempoResolucion + score.TiempoReaccion) * factor.Factor) / Nivel;
                score.FechaRegistro = fecha;
                cnx.JuegoScore.Add(score);
                cnx.SaveChanges();

                //Registramos la nota
                //Bimestre

                var bimestre = cnx.Bimestre.FirstOrDefault(x => x.Anyo == DateTime.Now.Year && fecha >= x.FechaInicio && fecha <= x.FechaFinal);

                if (bimestre != null)
                {
                    var lista = ScoreSelect(Nivel, score.IdAlumno);
                    if (lista != null && lista.Count >= 5)
                    {
                        var total = lista.Sum(x => x.Puntaje);
                        var nota_existente = cnx.Notas.FirstOrDefault(x => x.IdAlumno == score.IdAlumno && x.Anyo == fecha.Year && x.Grado == alumno.Grado && x.Bimestre == bimestre.NroBimestre);
                        double promedio = Math.Round(Double.Parse(((total / factor.PuntajeMax) * 20).ToString()));

                        if (nota_existente == null)
                        {
                            var nota = new Notas()
                            {
                                IdAlumno = alumno.IdAlumno,
                                IdNivelEscolar = (int)alumno.IdNivelEscolar,
                                Grado = (int)alumno.Grado,
                                Bimestre = bimestre.NroBimestre,
                                Anyo = fecha.Year,
                                PromedioVirtual = promedio,
                                IdDocente = null
                            };

                            cnx.Notas.Add(nota);
                        }
                        else
                        {
                            nota_existente.PromedioVirtual = promedio;
                        }

                        cnx.SaveChanges();                        
                    }
                }

                return true;
            }

            return false;
        }
    }
}
