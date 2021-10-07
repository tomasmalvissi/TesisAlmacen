using System;
using System.ComponentModel.DataAnnotations;

namespace MiAlmacen.Model.Models
{
    public class FormaPagoVentaModel
    {
        public int Id { get; set; }
        [Required]
        public int Venta_Id { get; set; }
        [Required]
        public int FormaPago_Id { get; set; }
        [Required]
        public decimal Importe { get; set; }
        [Required]
        public DateTime Fecha { get; set; }

        public FormaPagoModel FormaPago { get; set; } = new();

    }
}