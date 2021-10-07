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
        public string Descripción { get; set; }
        [Required(ErrorMessage = "Campo obligatorio.")]
        public string FormaPago { get; set; }
        [Required(ErrorMessage = "Campo obligatorio.")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Solo números.")]
        public decimal Ingreso { get; set; }
        [Required(ErrorMessage = "Campo obligatorio.")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Solo números.")]
        public decimal Egreso { get; set; }
        [Required(ErrorMessage = "Campo obligatorio.")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Solo números.")]
        public decimal Total { get; set; }
        public int Venta_Id { get; set; }
        public int Compra_Id { get; set; }

        public CajaModel Caja { get; set; }
        public VentaModel Venta { get; set; }
        public CompraModel Compras { get; set; }
    }
}
