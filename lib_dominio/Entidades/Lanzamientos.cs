using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_dominio.Entidades
{
    public class Lanzamientos
    {
        public int id { get; set; }
        public string? nombre { get; set; }
        public List<JugadoresLanzamientos> JugadoresLanzamientos { get; set; } = new List<JugadoresLanzamientos>();
        public byte[]? ImagenData { get; set; }
        public string? ImagenMimeType { get; set; }
    }
}
