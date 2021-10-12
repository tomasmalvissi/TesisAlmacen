using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAlmacen.Data.Entities
{
    public class MovimientosCaja
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Descripción { get; set; }
        public decimal Ingreso { get; set; }
        public decimal Egreso { get; set; }
        public decimal Total { get; set; }
        public DateTime? FechaBaja { get; set; }
        public int? Venta_Id { get; set; }
        public int? Compra_Id { get; set; }

        public Ventas Venta { get; set; }
        public Compras Compras { get; set; }
    }
}
