using MiAlmacen.Model.Models;
using System;
using System.ComponentModel.DataAnnotations;

public class FormaPagoCompraModel
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Campo obligatorio.")]
    public int Compra_Id { get; set; }
    [Required(ErrorMessage = "Campo obligatorio.")]
    public int FormaPago_Id { get; set; }
    [Required(ErrorMessage = "Campo obligatorio.")]
    public decimal Importe { get; set; }
    [Required(ErrorMessage = "Campo obligatorio.")]
    public DateTime Fecha { get; set; }

    public FormaPagoModel FormaPago { get; set; } = new();
}