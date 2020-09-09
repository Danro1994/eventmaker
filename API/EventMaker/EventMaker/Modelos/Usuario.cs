using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventMaker.Modelos
{
    public class Usuario
    {
        public int id { get; set; }
        public string nombre_usuario{ get; set; }
        public string apellido_usuario{ get; set; }
        public int edad { get; set; }
        public string correo_electronico { get; set; }
        public DateTime fecha_Registro { get; set; }
        public List<Compra> compras { get; set; }

    }
}
