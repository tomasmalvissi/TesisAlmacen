using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAlmacen.Model.Models
{
    public class CajaModel
    {
        public int Id { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        public int Empleado_Id { get; set; }
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Solo números")]
        public decimal Apertura { get; set; }
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Solo números")]
        public decimal Cierre { get; set; }
        public UsuarioModel Empleado { get; set; }
    }
}
