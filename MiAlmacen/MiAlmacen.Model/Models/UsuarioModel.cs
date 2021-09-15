using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAlmacen.Model.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        [MaxLength(8, ErrorMessage = "Máximo 8 carácter"), MinLength(1, ErrorMessage = "Minimo 1 carácter")]
        public string Usuario { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(8, ErrorMessage = "Máximo 8 carácter"), MinLength(1, ErrorMessage = "Minimo 1 carácter")]
        public string Contraseña { get; set; }
        public DateTime? FechaBaja { get; set; }
    }
}
