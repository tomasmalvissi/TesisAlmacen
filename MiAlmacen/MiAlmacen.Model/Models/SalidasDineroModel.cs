using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAlmacen.Model.Models
{
    public class SalidasDineroModel
    {
        public int Id { get; set; }
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "Campo obligatorio.")]
        public decimal Importe { get; set; }
        public int Caja_Id { get; set; }

        public CajaModel Caja { get; set; }
    }
}
