using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventMaker.Modelos
{
    public class Compra
    {
        public int id { get; set; }
        public int usuarioid { get; set; }
        public Usuario usuario { get; set; }
        public int eventoid { get; set; }
        public Evento evento { get; set; }
    }
}
