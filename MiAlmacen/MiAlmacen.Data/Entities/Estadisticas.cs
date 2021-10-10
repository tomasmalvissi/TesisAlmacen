using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAlmacen.Data.Entities
{
    public class Top
    {
        public string Clave { get; set; }
        public int Valor { get; set; }
    }

    public class Periodo
    {
        public string Mes { get; set; }
        public decimal Monto { get; set; }
    }
}
