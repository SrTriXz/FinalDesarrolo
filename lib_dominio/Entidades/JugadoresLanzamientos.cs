using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_dominio.Entidades
{
    public class JugadoresLanzamientos
    {
        public int id { get; set; }
        public int? jugadorId { get; set; }
        public Jugadores? Jugador { get; set; }
        public int? lanzamientoId { get; set; }
        public Lanzamientos? Lanzamiento { get; set; }
        public int? pino_derribado { get; set; }
        public int? puntaje_obtenido { get; set; }

    }
}
