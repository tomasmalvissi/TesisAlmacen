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
        public string? Descripcion { get; set; }
        public decimal Importe { get; set; }
        public int Caja_Id { get; set; }

        public Caja Caja { get; set; }
    }
}
