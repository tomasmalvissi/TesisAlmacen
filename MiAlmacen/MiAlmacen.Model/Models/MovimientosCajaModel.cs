using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAlmacen.Model.Models
{
    public class MovimientosCajaModel
    {
        public int Id { get; set; }
        public int Caja_Id { get; set; }
        [Required]
        public string FormaPago { get; set; }
        [Required]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Solo números")]
        public float Ingreso { get; set; }
        [Required]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Solo números")]
        public float Egreso { get; set; }
        [Required]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Solo números")]
        public float Total { get; set; }
        public int Venta_Id { get; set; }
        public int Compra_Id { get; set; }

        public CajaModel Caja { get; set; }
        public VentaModel Venta { get; set; }
        public CompraModel Compras { get; set; }
    }
}
