using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_dominio.Entidades
{
    public class Jugadores
    {
        public int id { get; set; }
        public string? nombre { get; set; }
        public string? apellido { get; set; }
        public int? edad { get; set; }
        public List<JugadoresLanzamientos> JugadoresLanzamientos { get; set; } = new List<JugadoresLanzamientos>();
        public List<JugadoresBolas> JugadoresBolas { get; set; } = new List<JugadoresBolas>();
        public List<JugadoresPartidas> JugadoresPartidas { get; set; } = new List<JugadoresPartidas>();
        public byte[]? ImagenData { get; set; }
        public string? ImagenMimeType { get; set; }

    }
}
