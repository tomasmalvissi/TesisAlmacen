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
        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Máximo 100 caracteres")]
        public string Nombre { get; set; }
        [Required]
        [Range(1, 99999999, ErrorMessage = "Máximo 8 caracteres")]
        public long DNI { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Máximo 100 caracteres")]
        public string Direccion { get; set; }
        [Required]
        [Range(1, 9999999999, ErrorMessage = "Máximo 11 dígitos")]
        public long Telefono { get; set; }
        public DateTime? FechaBaja { get; set; }
    }
}
