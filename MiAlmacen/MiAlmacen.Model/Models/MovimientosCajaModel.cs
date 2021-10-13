using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAlmacen.Model.Models
{
    public class MovimientosCajaModel
    {
        public string Descripción { get; set; }
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Empleado { get; set; }
        public string RazonSocial { get; set; }
        public decimal Importe { get; set; }

        public VentaModel Venta { get; set; }
        public CompraModel Compras { get; set; }
    }
}
