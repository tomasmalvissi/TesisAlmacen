using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAlmacen.Data.Entities
{
    public class Compras
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int Proveedor_Id { get; set; }
        public int NroRecibo { get; set; }
        public int Empleado_Id { get; set; }
        public float Total { get; set; }
        public float Saldo { get; set; }
        public DateTime Fecha_Baja { get; set; }

        public Proveedores Proveedor { get; set; }
        public Usuarios Empleado { get; set; }
    }
}
