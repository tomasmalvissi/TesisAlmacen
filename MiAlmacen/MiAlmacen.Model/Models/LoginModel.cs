using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAlmacen.Model.Models
{
    public class LoginModel
    {
        [Required]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Máximo 20 dígitos")]
        public string Usuario { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Máximo 20 dígitos")]
        public string Contraseña { get; set; }
    }
}
