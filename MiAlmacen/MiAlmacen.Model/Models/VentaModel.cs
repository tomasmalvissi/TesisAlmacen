using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAlmacen.Model.Models
{
    public class VentaModel
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string FormaPago { get; set; }
        public int Cliente_Id { get; set; }
        public int Empleado_Id { get; set; }
        public float Total { get; set; }
        public float Saldo { get; set; }

        public ClienteModel Cliente { get; set; }
        public UsuarioModel Empleado { get; set; }
    }
}
