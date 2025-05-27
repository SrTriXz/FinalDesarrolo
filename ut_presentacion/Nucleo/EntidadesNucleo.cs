using lib_dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ut_presentacion.Nucleo
{
    class EntidadesNucleo
    {
        public static Bolas? Bolas()
        {
            var entidad = new Bolas();
            entidad.nombre = "Bola-" + DateTime.Now.ToString("yyyyMMdd");
            entidad.diametro = 10.5;
            entidad.Color = "Rojo";
            entidad.peso = 6.8;
            return entidad;
        }
        public static Jugadores? Jugadores()
        {
            var entidad = new Jugadores();
            entidad.nombre = "Jugador-" + DateTime.Now.ToString("yyyyMMdd");
            entidad.apellido = "ApellidoPrueba";
            entidad.edad = 25;
            return entidad;
        }
        public static Lanzamientos? Lanzamientos()
        {
            var entidad = new Lanzamientos();
            entidad.nombre = "Lanzamiento-" + DateTime.Now.ToString("HHmmss");
            return entidad;
        }
        public static Pistas? Pistas()
        {
            var entidad = new Pistas();
            entidad.nombre = "Pista-" + DateTime.Now.ToString("ddHHmm");
            entidad.estado = "Disponible";
            return entidad;
        }

        public static JugadoresBolas? JugadoresBolas(int jugadorId, int bolaId)
        {
            var entidad = new JugadoresBolas();
            entidad.jugadorId = jugadorId;
            entidad.bolaId = bolaId;
            entidad.efectividad = "Media-" + DateTime.Now.ToString("mmss");
            return entidad;
        }
        public static JugadoresLanzamientos? JugadoresLanzamientos(int jugadorId, int lanzamientoId)
        {
            var entidad = new JugadoresLanzamientos();
            entidad.jugadorId = jugadorId;
            entidad.lanzamientoId = lanzamientoId;
            entidad.pino_derribado = new Random().Next(1, 10);
            entidad.puntaje_obtenido = new Random().Next(50, 100);
            return entidad;
        }
        public static JugadoresPartidas? JugadoresPartidas(int jugadorId, int partidaId)
        {
            var entidad = new JugadoresPartidas();
            entidad.jugadorId = jugadorId;
            entidad.partidaId = partidaId;
            entidad.codigo = "JP-" + DateTime.Now.ToString("HHmmss");
            entidad.turno = new Random().Next(1, 5);
            entidad.puntaje = new Random().Next(50, 200);
            return entidad;
        }
        public static Partidas? Partidas()
        {
            var entidad = new Partidas();
            entidad.codigo = "PART-" + DateTime.Now.ToString("yyyyMMddHHmmss");
            entidad.fecha = DateTime.Now;
            entidad.hora_inicio = TimeSpan.FromHours(10);
            entidad.hora_final = TimeSpan.FromHours(12);
            entidad.estado = "En curso";
            entidad.ganadorId = new Random().Next(1, 10);
            entidad.puntaje_final = new Random().Next(100, 300);
            return entidad;
        }

    }
}
