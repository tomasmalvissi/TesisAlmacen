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
        public string Usuario { get; set; }
        [Required]
        public string Contraseña { get; set; }
    }
}
