using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAlmacen.Model.Models
{
    public class ProveedorModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo obligatorio")]
        [MaxLength(100, ErrorMessage = "Máximo 100 carácteres"), MinLength(3, ErrorMessage = "Minimo 3 carácteres.")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Campo obligatorio")]
        [RegularExpression(@"^(\d{11})$", ErrorMessage = "Ingrese sólo los 11 números del CUIL/CUIT.")]
        public long CUIL { get; set; }
        [Required(ErrorMessage = "Campo obligatorio")]
        [MaxLength(100, ErrorMessage = "Máximo 100 carácteres"), MinLength(3, ErrorMessage = "Minimo 3 carácteres.")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "Campo obligatorio")]
        [Range(1, 9999999999, ErrorMessage = "Ingrese un teléfono válido.")]
        public long Telefono { get; set; }
        public DateTime? FechaBaja { get; set; }
    }
}
