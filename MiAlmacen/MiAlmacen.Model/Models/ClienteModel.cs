using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAlmacen.Model.Models
{
    public class ClienteModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo obligatorio.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Máximo 100 caracteres")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Campo obligatorio.")]
        [RegularExpression(@"^(?!00000)[0-9]{7,8}$", ErrorMessage = "Ingrese sólo los números del DNI.")]
        public long DNI { get; set; }
        [Required(ErrorMessage = "Campo obligatorio.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El campo debe tener 3 carácteres como mínimo.")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "Campo obligatorio.")]
        [Range(1, 9999999999, ErrorMessage = "Ingrese un teléfono válido.")]
        public long Telefono { get; set; }
        public DateTime? FechaBaja { get; set; }
    }
}
