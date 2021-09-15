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
        [Required]
        public DateTime Fecha { get; set; }
        public int Proveedor_Id { get; set; }
        [Required]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Solo números")]
        public long NroRecibo { get; set; }
        public int Empleado_Id { get; set; }
        [Required]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Solo números")]
        public float Total { get; set; }
        [Required]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Solo números")]
        public float Saldo { get; set; }

        public ProveedorModel Proveedor { get; set; }
        public UsuarioModel Empleado { get; set; }
    }
}
