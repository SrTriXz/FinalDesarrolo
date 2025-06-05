using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_dominio.Entidades
{
    public class JugadoresPartidas
    {
        public int id { get; set; }
        public int? jugadorId { get; set; }
        public Jugadores? Jugador { get; set; }
        public int? partidaId { get; set; }
        public Partidas? Partida { get; set; }
        public string? codigo { get; set; }
        public int? turno { get; set; }
        public int? puntaje { get; set; }

    }
}
