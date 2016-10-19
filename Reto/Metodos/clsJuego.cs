using Reto.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Reto.Metodos
{
    public class clsJuego
    {
        dbretoEntities cnx;

        public clsJuego()
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
            return (int)cnx.usp_CreateIdReto(Nivel).FirstOrDefault();
        }

        public Juego ExtraerJuegoAzar(string Nivel)
        {
            Juego juego = null;

            var result = cnx.Juego.Where(x => x.Nivel == "Nivel " + Nivel).ToList();
            if (result.Count > 0)
            {
                int Azar = new Random().Next(0, result.Count - 1);
                juego = result[Azar];
            }

            return juego;
        }

        public bool GuardarScore(Alumno_Juego score)
        {
            var alumno = cnx.Alumno.FirstOrDefault(x => x.IdPersona == SessionHelper.Usuario.IdPersona);
            if (alumno != null)
            {
                score.IdAlumno = alumno.IdAlumno;

                cnx.Alumno_Juego.RemoveRange(cnx.Alumno_Juego.Where(x => x.IdAlumno == score.IdAlumno && x.IdJuego == score.IdJuego).ToList());
                cnx.SaveChanges();

                cnx.Alumno_Juego.Add(score);
               return cnx.SaveChanges() != 0;
            }

            return false;
        }
    }
}