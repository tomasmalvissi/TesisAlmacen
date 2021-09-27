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
        public string Nombre { get; set; }
        [Required]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Solo números")]
        public long Codigo_Art { get; set; }
        [Required]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Solo números")]
        public float Precio_Unit { get; set; }
        [Required]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Solo números")]
        public float Precio_Mayor { get; set; }
        [Required]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Solo números")]
        public int Stock_Act { get; set; }
        public DateTime Ultima_Modif { get; set; }
        public DateTime? FechaBaja { get; set; }
    }
}
