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
        [RegularExpression("^[0-9]+$", ErrorMessage = "Solo números")]
        public float Apertura { get; set; }
        [RegularExpression("^[0-9]+$", ErrorMessage = "Solo números")]
        public float Cierre { get; set; }
        public UsuarioModel Empleado { get; set; }
    }
}
