using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_dominio.Entidades
{
    public class Pistas
    {
        public int id { get; set; }
        public string? nombre { get; set; }
        public string? estado { get; set; }
        public byte[]? ImagenData { get; set; }
        public string? ImagenMimeType { get; set; }
        public List<Partidas> Partidas { get; set; } = new List<Partidas>();
    }
}
