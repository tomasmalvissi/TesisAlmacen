using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAlmacen.Data.Entities
{
    public class SalidasDinero
    {
        public int Id { get; set; }
        public string Operacion { get; set; }
        public int MovimientosCaja_Id { get; set; }

        public MovimientosCaja MovimientosCaja { get; set; }
    }
}
