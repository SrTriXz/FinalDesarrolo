using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_dominio.Entidades
{
    public class Partidas
    {
        public int id { get; set; }
        public string? codigo { get; set; }
        public DateTime fecha { get; set; }
        public TimeSpan hora_inicio { get; set; }
        public TimeSpan hora_final { get; set; }
        public string? estado { get; set; }
        public int? ganadorId { get; set; }
        public Jugadores? Ganador { get; set; }
        public int? puntaje_final { get; set; }
        public byte[]? ImagenData { get; set; }
        public string? ImagenMimeType { get; set; }
        public List<JugadoresPartidas> JugadoresPartidas { get; set; } = new List<JugadoresPartidas>();
    }
}
