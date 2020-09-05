using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventMaker.Modelos
{
    public class CategoriaEvento
    {
        public int id { get; set; }
        public string categoria_evento { get; set; }
        public string icono { get; set; }
        public List<Evento> eventos { get; set; }
    }
}
