using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAlmacen.Model.Models
{
    public class CompraModel
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int Proveedor_Id { get; set; }
        public int NroRecibo { get; set; }
        public int Empleado_Id { get; set; }
        public float Total { get; set; }
        public float Saldo { get; set; }

        public ProveedorModel Proveedor { get; set; }
        public UsuarioModel Empleado { get; set; }
    }
}
