using MiAlmacen.Data.Conection;
using MiAlmacen.Data.Entities;
using MiAlmacen.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAlmacen.Data.Repositories
{
    public class DetalleVentaRepository : DBConex
    {
        string orden;
        public DetalleVentas IniciarObjeto(DetalleVentaModel model)
        {
            DetalleVentas detVentas = new();
            detVentas.Id = model.Id;
            detVentas.Cantidad = model.Cantidad;
            detVentas.Articulo_Id = model.Articulo_Id;
            detVentas.Venta_Id = model.Venta_Id;
            detVentas.Precio = model.Precio;

            return detVentas;
        }

        public List<DetalleVentas> GetAll(Ventas venta)
        {
            SqlCommand sqlcmd = new(orden, conexion);

            List<DetalleVentas> detalles = new();
            try
            {
                orden = @"SELECT * FROM DetalleVentas  
                         WHERE Venta_Id = @Venta_Id";

                AbrirConex();
                sqlcmd.CommandText = orden;

                sqlcmd.Parameters.AddWithValue("@Venta_Id", venta.Id);
                SqlDataReader reader = sqlcmd.ExecuteReader();

                while (reader.Read())
                {
                    DetalleVentas detVenta = new();
                    detVenta.Venta = venta;
                    detVenta.Id = Convert.ToInt32(reader["Id"].ToString());
                    detVenta.Articulo_Id = Convert.ToInt32(reader["Articulo_Id"].ToString());
                    detVenta.Cantidad = Convert.ToInt32(reader["Cantidad"].ToString());
                    detVenta.Precio = Convert.ToSingle(reader["Precio"].ToString());
                    detVenta.Venta_Id = Convert.ToInt32(reader["Venta_Id"].ToString());

                    detalles.Add(detVenta);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CerrarConex();
                sqlcmd.Dispose();
            }
            return detalles;
        }

        public DetalleVentas GetOne(int id)
        {
            orden = $"SELECT * FROM DetalleVentas WHERE Id ={id}";
            SqlCommand sqlcmd = new(orden, conexion);

            DetalleVentas detVenta = new();
            try
            {
                AbrirConex();
                sqlcmd.CommandText = orden;
                SqlDataReader reader = sqlcmd.ExecuteReader();

                while (reader.Read())
                {
                    detVenta.Id = Convert.ToInt32(reader["Id"].ToString());
                    detVenta.Articulo_Id = Convert.ToInt32(reader["Articulo_Id"].ToString());
                    detVenta.Cantidad = Convert.ToInt32(reader["Cantidad"].ToString());
                    detVenta.Precio = Convert.ToSingle(reader["Precio"].ToString());
                    detVenta.Venta_Id = Convert.ToInt32(reader["Venta_Id"].ToString());
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
            return detVenta;
        }
    }
}
