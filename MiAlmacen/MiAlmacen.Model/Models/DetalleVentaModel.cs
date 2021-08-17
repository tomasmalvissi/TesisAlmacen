using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAlmacen.Model.Models
{
    public class DetalleVentaModel
    {
        public int Id { get; set; }
        public int Articulo_Id { get; set; }
        public float Precio { get; set; }
        public int Cantidad { get; set; }
        public int Venta_Id { get; set; }

        public ArticuloModel Articulo { get; set; }
        public VentaModel Venta { get; set; }
    }
}
