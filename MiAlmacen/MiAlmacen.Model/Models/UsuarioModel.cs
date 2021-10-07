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
        [Required(ErrorMessage = "Campo obligatorio.")]
        [MaxLength(100, ErrorMessage = "Máximo 100 carácteres."), MinLength(4, ErrorMessage = "Minimo 4 carácteres.")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Campo obligatorio")]
        [MaxLength(100, ErrorMessage = "Máximo 100 carácteres."), MinLength(3, ErrorMessage = "Minimo 3 carácteres.")]
        public string Usuario { get; set; }
        [Required(ErrorMessage = "Campo obligatorio.")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Campo obligatorio.")]
        [MaxLength(100, ErrorMessage = "La contraseña debe tener máximo 100 carácteres."), MinLength(8, ErrorMessage = "La contraseña debe tener mínimo 8 carácteres.")]
        public string Contraseña { get; set; }
        public DateTime? FechaBaja { get; set; }
    }
}
