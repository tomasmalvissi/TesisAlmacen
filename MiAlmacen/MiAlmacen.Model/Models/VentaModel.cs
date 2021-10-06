using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MiAlmacen.Model.Models
{
    public class VentaModel
    {
        public int Id { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        public int Cliente_Id { get; set; }
        public int Empleado_Id { get; set; }
        [Required]
        public float Total { get; set; }
        [Required]
        public float Saldo { get; set; }
        public DateTime? Fecha_Baja { get; set; }

        public List<DetalleVentaModel> Detalle { get; set; } = new ();
        public ClienteModel Cliente { get; set; }
        public UsuarioModel Empleado { get; set; }
        public List<FormaPagoVentaModel> FormasPago { get; set; } = new();
    }
}
