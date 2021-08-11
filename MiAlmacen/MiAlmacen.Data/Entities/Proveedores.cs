using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAlmacen.Data.Entities
{
    public class Proveedores
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string CUIL { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
    }
}
