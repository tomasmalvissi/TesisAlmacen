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
        [Required(ErrorMessage = "Campo obligatorio.")]
        public string Usuario { get; set; }
        [Required(ErrorMessage = "Campo obligatorio.")]
        public string Contraseña { get; set; }
    }
}
