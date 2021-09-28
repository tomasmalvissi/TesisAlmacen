using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAlmacen.Model.Models
{
    public class DetalleVentaModel
    {
        public int Id { get; set; }
        public int Articulo_Id { get; set; }
        [Required]
        public float Precio { get; set; }
        [Required]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Solo números")]
        public int Cantidad { get; set; }

        public float SubTotal { get; set; }
        public int Venta_Id { get; set; }

        public ArticuloModel Articulo { get; set; }
    }
}
