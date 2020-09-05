using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventMaker.Modelos
{
    public class Reservacion
    {
        public int id { get; set; }
        public int usuarioid { get; set; }
        public Usuario usuario { get; set; }
        public List<Evento> eventos { get; set; }
    }
}
