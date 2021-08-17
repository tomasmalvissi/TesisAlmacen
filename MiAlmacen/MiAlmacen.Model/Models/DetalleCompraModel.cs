using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAlmacen.Model.Models
{
    public class DetalleCompraModel
    {
        public int Id { get; set; }
        public int Articulo_Id { get; set; }
        public int Cantidad { get; set; }
        public float Precio_Mayor { get; set; }
        public float Precio_Unit { get; set; }
        public int Compra_Id { get; set; }

        public ArticuloModel Articulo { get; set; }
        public CompraModel Compra { get; set; }
    }
}
