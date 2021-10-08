using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAlmacen.Model.Models
{
    public class DetalleCompraModel
    {
        public int Id { get; set; }
        public int Articulo_Id { get; set; }
        [Required(ErrorMessage = "Campo obligatorio.")]
        [Range(0, 1000, ErrorMessage = "El campo admite cantidades menores a 1000.")]
        public int Cantidad { get; set; }
        [Required(ErrorMessage = "Campo obligatorio.")]
        public decimal Precio_Mayor { get; set; }
        [Required(ErrorMessage = "Campo obligatorio.")]
        public decimal Precio_Unit { get; set; }
        public decimal SubTotal { get; set; }
        public int Compra_Id { get; set; }

        public ArticuloModel Articulo { get; set; } = new();
        public CompraModel Compra { get; set; }
    }
}
