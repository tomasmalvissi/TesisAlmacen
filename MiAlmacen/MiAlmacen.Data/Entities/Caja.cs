using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAlmacen.Data.Entities
{
    public class Caja
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int Empleado_Id { get; set; }
        public decimal Apertura { get; set; }
        public decimal Cierre { get; set; }
        public Usuarios Empleado { get; set; }
    }
}
