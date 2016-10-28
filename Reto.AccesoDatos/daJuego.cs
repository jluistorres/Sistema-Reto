using Reto.Entidades;
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

        public bool GuardarScore(JuegoScore score, int IdPersona)
        {
            var alumno = cnx.Alumno.FirstOrDefault(x => x.IdPersona == IdPersona);
            if (alumno != null)
            {
                score.IdAlumno = alumno.IdAlumno;

                cnx.JuegoScore.RemoveRange(cnx.JuegoScore.Where(x => x.IdAlumno == score.IdAlumno && x.IdJuego == score.IdJuego).ToList());
                cnx.SaveChanges();

                cnx.JuegoScore.Add(score);
               return cnx.SaveChanges() != 0;
            }

            return false;
        }
    }
}
