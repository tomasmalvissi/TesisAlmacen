using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAlmacen.Data.Entities
{
    public class Clientes
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public long DNI { get; set; }
        public string Direccion { get; set; }
        public long Telefono { get; set; }
        public DateTime? FechaBaja { get; set; }
    }
}
