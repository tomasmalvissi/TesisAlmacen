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
    public class MovimientoRepository : DBConex
    {
        string orden;
        private static VentaRepository ventaRepository = new();
        private static CompraRepository compraRepository = new();
        private static MovimientosCaja IniciarObjeto(MovimientosCajaModel model)
        {
            MovimientosCaja mov = new();
            mov.Id = model.Id;
            mov.Fecha = model.Fecha;
            mov.Descripción = model.Descripción;
            mov.Ingreso = model.Ingreso;
            mov.Egreso = model.Egreso;
            mov.Total = model.Total;
            mov.FechaBaja = model.FechaBaja;
            mov.Venta_Id = model.Venta_Id;
            mov.Compra_Id = model.Compra_Id;

            return mov;
        }

        public List<MovimientosCaja> GetAll()
        {
            orden = $"SELECT * FROM MovimientosCaja ORDER BY Fecha DESC";
            List<MovimientosCaja> movimientos = new();

            SqlCommand sqlcmd = new(orden, conexion);
            try
            {
                AbrirConex();
                sqlcmd.CommandText = orden;
                SqlDataReader reader = sqlcmd.ExecuteReader();

                while (reader.Read())
                {
                    MovimientosCaja mov = new();
                    mov.Id = Convert.ToInt32(reader["Id"].ToString());
                    mov.Fecha = Convert.ToDateTime(reader["Fecha"]);
                    mov.Descripción = reader["Descripcion"].ToString();
                    mov.Ingreso = Convert.ToDecimal(reader["Ingreso"].ToString());
                    mov.Egreso = Convert.ToDecimal(reader["Egreso"].ToString());
                    mov.Total = Convert.ToDecimal(reader["Total"].ToString());
                    mov.FechaBaja = string.IsNullOrEmpty(reader["FechaBaja"].ToString()) ? null : Convert.ToDateTime(reader["FechaBaja"]);
                    mov.Venta_Id = string.IsNullOrEmpty(reader["Venta_Id"].ToString()) ? null : Convert.ToInt32(reader["Venta_Id"].ToString());
                    mov.Compra_Id = string.IsNullOrEmpty(reader["Compra_Id"].ToString()) ? null : Convert.ToInt32(reader["Compra_Id"].ToString());


                    movimientos.Add(mov);
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
            return movimientos;
        }

        public MovimientosCaja Post(MovimientosCajaModel model)
        {
            if (model == null)
            {
                throw new Exception("Error al tratar de ejecutar la operación");
            }
            else
            {
                MovimientosCaja mov = IniciarObjeto(model);

                AbrirConex();
                SqlTransaction transaction;
                transaction = conexion.BeginTransaction();
                SqlCommand sqlcmd = new(orden, conexion, transaction);
                mov.Fecha = DateTime.Now;

                try
                {
                    sqlcmd.Connection = conexion;
                    sqlcmd.Transaction = transaction;

                    orden = @"SELECT TOP 1 Total MovimientosCaja ORDER BY Fecha";
                    sqlcmd.CommandText = orden;
                    sqlcmd.ExecuteNonQuery();
                    SqlDataReader reader = sqlcmd.ExecuteReader();

                    decimal total = 0;

                    while (reader.Read())
                    {
                        total = string.IsNullOrEmpty(reader["Total"].ToString()) ? 0 : Convert.ToDecimal(reader["Total"].ToString());
                    }

                    mov.Total = (total - mov.Egreso + mov.Ingreso);

                    orden = @"INSERT INTO MovimientosCaja (Fecha, Descripcion, Ingreso, Egreso, Total)
                            VALUES (@Fecha, @Descripcion, @Ingreso, @Egreso, @Total) ";

                    sqlcmd.CommandText = orden;
                    sqlcmd.Parameters.AddWithValue("@Fecha", mov.Fecha);
                    sqlcmd.Parameters.AddWithValue("@Descripcion", mov.Descripción);
                    sqlcmd.Parameters.AddWithValue("@Ingreso", mov.Ingreso);
                    sqlcmd.Parameters.AddWithValue("@Egreso", mov.Egreso);
                    sqlcmd.Parameters.AddWithValue("@Total", mov.Total);

                    sqlcmd.ExecuteNonQuery();
                    sqlcmd.Parameters.Clear();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();

                    throw new Exception("Error al tratar de ejecutar la operación " + e.Message);
                }
                finally
                {
                    CerrarConex();
                    sqlcmd.Dispose();
                }

                return mov;
            }
        }
    }
}
