using System;
using System.ComponentModel.DataAnnotations;

namespace MiAlmacen.Model.Models
{
    public class FormaPagoVentaModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo obligatorio.")]
        public int Venta_Id { get; set; }
        [Required(ErrorMessage = "Campo obligatorio.")]
        public int FormaPago_Id { get; set; }
        [Required(ErrorMessage = "Campo obligatorio.")]
        public decimal Importe { get; set; }
        [Required(ErrorMessage = "Campo obligatorio.")]
        public DateTime Fecha { get; set; }

        public FormaPagoModel FormaPago { get; set; } = new();

    }
}