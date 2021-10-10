using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAlmacen.Model.Models
{
    public class TopModel
    {
        public string Clave { get; set; }
        public int Valor { get; set; }
    }

    public class PeriodoModel
    {
        public string Mes { get; set; }
        public decimal Monto { get; set; }
    }
}
