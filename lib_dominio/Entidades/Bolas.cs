﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib_dominio.Entidades
{
    public class Bolas
    {
        public int id { get; set; }
        public string? nombre { get; set; }
        public double? diametro { get; set; }
        public string? Color { get; set; }
        public double? peso { get; set; }
        public byte[]? ImagenData { get; set; }
        public string? ImagenMimeType { get; set; }
        public List<JugadoresBolas> JugadoresBolas { get; set; } = new List<JugadoresBolas>();

    }
}
