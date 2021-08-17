using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAlmacen.Model.Models
{
    public class SalidasDineroModel
    {
        public int Id { get; set; }
        public string Operacion { get; set; }
        public int MovimientosCaja_Id { get; set; }

        public MovimientosCajaModel MovimientosCaja { get; set; }
    }
}
