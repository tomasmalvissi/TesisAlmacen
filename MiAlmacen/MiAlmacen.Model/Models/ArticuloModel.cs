using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAlmacen.Model.Models
{
    public class ArticuloModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo obligatorio.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El campo debe tener 3 carácteres como mínimo.")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Campo obligatorio.")]
        [RegularExpression(@"^(?!00000)[0-9]{3,13}$", ErrorMessage = "El campo debe tener 3 carácteres como mínimo y 13 como máximo.")]
        public long Codigo_Art { get; set; }
        [Required(ErrorMessage = "Campo obligatorio.")]
        public decimal Precio_Unit { get; set; }
        [Required(ErrorMessage = "Campo obligatorio.")]
        public decimal Precio_Mayor { get; set; }
        [Required(ErrorMessage = "Campo obligatorio.")]
        [Range(-30, 1000, ErrorMessage = "Solo números de entre -30 y 1000.")]
        public int Stock_Act { get; set; }
        public DateTime Ultima_Modif { get; set; }
        public DateTime? FechaBaja { get; set; }
    }
}
