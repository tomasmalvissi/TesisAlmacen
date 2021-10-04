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
        [Required]
        public string Nombre { get; set; }
        [Required]
        [Range(1, 99999999999, ErrorMessage = "Máximo 11 caracteres")]
        public long CUIL { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Máximo 20 dígitos")]
        public string Direccion { get; set; }
        [Required]
        [Range(1, 9999999999, ErrorMessage = "Máximo 11 dígitos")]
        public long Telefono { get; set; }
        public DateTime? FechaBaja { get; set; }
    }
}
