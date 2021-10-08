using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAlmacen.Model.Models
{
    public class CompraModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo obligatorio.")]
        public DateTime Fecha { get; set; }
        public int Proveedor_Id { get; set; }
        [Required(ErrorMessage = "Campo obligatorio.")]
        [RegularExpression(@"^(?!00000)[0-9]{3,13}$", ErrorMessage = "El campo debe tener 3 carácteres como mínimo y 13 como máximo.")]
        public long NroRecibo { get; set; }
        public int Empleado_Id { get; set; }
        [Required(ErrorMessage = "Campo obligatorio.")]
        public decimal Total { get; set; }
        public DateTime? Fecha_Baja { get; set; }

        public ProveedorModel Proveedor { get; set; }
        public UsuarioModel Empleado { get; set; } 
        public List<DetalleCompraModel> Detalle { get; set; } = new();
        public List<FormaPagoCompraModel> FormasPago { get; set; } = new();
    }
}