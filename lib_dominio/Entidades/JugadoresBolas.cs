using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_dominio.Entidades
{
    public class JugadoresBolas
    {
        public int id { get; set; }
        public int? jugadorId { get; set; }
        public Jugadores? Jugador { get; set; }
        public int? bolaId { get; set; }
        public Bolas? Bola { get; set; }
        public string? efectividad { get; set; }
        public byte[]? ImagenData { get; set; }
        public string? ImagenMimeType { get; set; }
    }
}
