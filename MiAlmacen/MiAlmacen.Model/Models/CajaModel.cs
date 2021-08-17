using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAlmacen.Model.Models
{
    public class CajaModel
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public int Empleado_Id { get; set; }
        public float Apertura { get; set; }
        public float Cierre { get; set; }
        public UsuarioModel Empleado { get; set; }
    }
}
