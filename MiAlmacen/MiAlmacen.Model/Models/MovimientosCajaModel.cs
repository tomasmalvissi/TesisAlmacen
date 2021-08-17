using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAlmacen.Model.Models
{
    public class MovimientosCajaModel
    {
        public int Id { get; set; }
        public int Caja_Id { get; set; }
        public string FormaPago { get; set; }
        public float Ingreso { get; set; }
        public float Egreso { get; set; }
        public float Total { get; set; }
        public int Venta_Id { get; set; }
        public int Compra_Id { get; set; }

        public CajaModel Caja { get; set; }
        public VentaModel Venta { get; set; }
        public CompraModel Compras { get; set; }
    }
}
