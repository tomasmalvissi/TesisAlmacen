using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAlmacen.Model.Models
{
    public class IngresoModel
    {
        public decimal Efectivo { get; set; }
        public decimal TarjetaDebito { get; set; }
        public decimal TarjetaCredito { get; set; }
        public decimal Cheque { get; set; }
        public decimal Transferencia { get; set; }
    }
}
