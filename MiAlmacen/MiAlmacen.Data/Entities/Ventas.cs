using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAlmacen.Data.Entities
{
    public class Ventas
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string FormaPago { get; set; }
        public int Cliente_Id { get; set; }
        public int Empleado_Id { get; set; }
        public float Total { get; set; }
        public float Saldo { get; set; }
        public DateTime? Fecha_Baja { get; set; }

        public List<DetalleVentas> Detalle { get; set; } = new();
        public Clientes Cliente { get; set; } = new ();
        public Usuarios Empleado { get; set; } = new ();
    }
}
