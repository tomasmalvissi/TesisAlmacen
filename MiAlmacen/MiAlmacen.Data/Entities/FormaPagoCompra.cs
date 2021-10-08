using MiAlmacen.Data.Entities;
using System;

public class FormaPagoCompra
{
    public int Id { get; set; }
    public int Compra_Id { get; set; }
    public int FormaPago_Id { get; set; }
    public decimal Importe { get; set; }
    public DateTime Fecha { get; set; }

    public FormaPago FormaPago { get; set; } = new();
}