using MiAlmacen.Data.Conection;
using MiAlmacen.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MiAlmacen.Data.Repositories
{
    public class FormaPagoRepository : DBConex
    {
        string orden;

        public FormaPago GetOne(int id)
        {
            orden = @"SELECT * FROM FormasPago WHERE Id = @Id";

            FormaPago fpago = null;

            SqlCommand sqlcmd = new(orden, conexion);
            try
            {
                AbrirConex();
                sqlcmd.CommandText = orden;
                sqlcmd.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = sqlcmd.ExecuteReader();

                while (reader.Read())
                {
                    fpago = new();
                    fpago.Id = Convert.ToInt32(reader["Id"].ToString());
                    fpago.Descripcion = reader["Descripcion"].ToString();
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error al tratar de ejecutar la operación " + e.Message);
            }
            finally
            {
                CerrarConex();
                sqlcmd.Dispose();
            }
            return fpago;
        }

        public List<FormaPago> GetAll()
        {
            orden = @"SELECT * FROM FormasPago";

            List<FormaPago> formaPagos = new();

            SqlCommand sqlcmd = new(orden, conexion);
            try
            {
                AbrirConex();
                sqlcmd.CommandText = orden;
                SqlDataReader reader = sqlcmd.ExecuteReader();

                while (reader.Read())
                {
                    FormaPago fpago = new();
                    fpago.Id = Convert.ToInt32(reader["Id"].ToString());
                    fpago.Descripcion = reader["Descripcion"].ToString();
                    formaPagos.Add(fpago);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error al tratar de ejecutar la operación " + e.Message);
            }
            finally
            {
                CerrarConex();
                sqlcmd.Dispose();
            }
            return formaPagos;
        }

        public Ventas GetAllFormasPagoXVenta(Ventas venta)
        {
            orden = @"SELECT * FROM FormasPagoVentas WHERE Venta_Id = @Venta_Id";

            List<FormaPagoVentas> formaPagos = new();

            SqlCommand sqlcmd = new(orden, conexion);
            try
            {
                AbrirConex();
                sqlcmd.CommandText = orden;
                sqlcmd.Parameters.AddWithValue("@Venta_Id", venta.Id);
                SqlDataReader reader = sqlcmd.ExecuteReader();

                while (reader.Read())
                {
                    FormaPagoVentas fpago = new();
                    fpago.Id = Convert.ToInt32(reader["Id"].ToString());
                    fpago.Venta_Id = Convert.ToInt32(reader["Venta_Id"].ToString());
                    fpago.FormaPago_Id = Convert.ToInt32(reader["FormaPago_Id"].ToString());
                    fpago.Importe = Convert.ToDecimal(reader["Importe"].ToString());
                    fpago.Fecha = Convert.ToDateTime(reader["Fecha"].ToString());
                    formaPagos.Add(fpago);
                }

                CerrarConex();

                venta.FormasPago = formaPagos;

                foreach (var item in venta.FormasPago)
                {
                    item.FormaPago = GetOne(item.FormaPago_Id);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error al tratar de ejecutar la operación " + e.Message);
            }
            finally
            {
                CerrarConex();
                sqlcmd.Dispose();
            }
            return venta;
        }

        public Compras GetAllFormasPagoXCompra(Compras compra)
        {
            orden = @"SELECT * FROM FormasPagoCompras WHERE Compra_Id = @Compra_Id";

            List<FormaPagoCompra> formaPagos = new();

            SqlCommand sqlcmd = new(orden, conexion);
            try
            {
                AbrirConex();
                sqlcmd.CommandText = orden;
                sqlcmd.Parameters.AddWithValue("@Compra_Id", compra.Id);
                SqlDataReader reader = sqlcmd.ExecuteReader();

                while (reader.Read())
                {
                    FormaPagoCompra fpago = new();
                    fpago.Id = Convert.ToInt32(reader["Id"].ToString());
                    fpago.Compra_Id = Convert.ToInt32(reader["Compra_Id"].ToString());
                    fpago.FormaPago_Id = Convert.ToInt32(reader["FormaPago_Id"].ToString());
                    fpago.Importe = Convert.ToDecimal(reader["Importe"].ToString());
                    fpago.Fecha = Convert.ToDateTime(reader["Fecha"].ToString());
                    formaPagos.Add(fpago);
                }

                CerrarConex();

                compra.FormasPago = formaPagos;

                foreach (var item in compra.FormasPago)
                {
                    item.FormaPago = GetOne(item.FormaPago_Id);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error al tratar de ejecutar la operación " + e.Message);
            }
            finally
            {
                CerrarConex();
                sqlcmd.Dispose();
            }
            return compra;
        }
    }
}