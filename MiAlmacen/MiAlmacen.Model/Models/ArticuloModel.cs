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
        [Required]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Máximo 20 caracteres")]
        public string Nombre { get; set; }
        [Required]
        [Range(0, 99999999999999, ErrorMessage = "Solo números de hasta 13 dígitos")]
        public long Codigo_Art { get; set; }
        [Required]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Solo números")]
        public float Precio_Unit { get; set; }
        [Required]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Solo números")]
        public float Precio_Mayor { get; set; }
        [Required]
        [Range(-30, 1000, ErrorMessage = "Solo números de entre -30 y 1000")]
        public int Stock_Act { get; set; }
        public DateTime Ultima_Modif { get; set; }
        public DateTime? FechaBaja { get; set; }
    }
}
