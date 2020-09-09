using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventMaker.Modelos
{
    public class Evento
    {
        public int id { get; set; }
        public string nombre_evento { get; set; }
        public string lugar { get; set; }
        public decimal precio { get; set; }
        public DateTime fecha { get; set; }
        public string clave { get; set; }
        public int categoriaEventoid { get; set; }
        public CategoriaEvento categoriaEvento { get; set; }
        public int invitadoid { get; set; }
        public Invitado invitado { get; set; }
  

    }
}
