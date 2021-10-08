using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAlmacen.Data.Entities
{
    public class DetalleCompras
    {
        public int Id { get; set; }
        public int Articulo_Id { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio_Mayor { get; set; }
        public decimal Precio_Unit { get; set; }
        public decimal SubTotal { get; set; }
        public int Compra_Id { get; set; }

        public Articulos Articulo { get; set; } = new();
        public Compras Compra { get; set; }
    }
}
