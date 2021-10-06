using System;

namespace MiAlmacen.Data.Entities
{
    public class FormaPagoVentas
    {
        public int Id { get; set; }
        public int Venta_Id { get; set; }
        public int FormaPago_Id { get; set; }
        public float Importe { get; set; }
        public DateTime Fecha { get; set; }

        public FormaPago FormaPago { get; set; } = new();
    }
}
