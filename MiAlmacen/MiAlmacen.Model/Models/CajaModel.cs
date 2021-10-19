using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAlmacen.Model.Models
{
    public class CajaModel
    {
        public int Id { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        public int Empleado_Id { get; set; }
        public decimal Apertura { get; set; }
        public decimal Actual { get; set; }
        public decimal Cierre { get; set; }
        public DateTime? FechaCierre { get; set; }
        public UsuarioModel Empleado { get; set; }
    }
}
