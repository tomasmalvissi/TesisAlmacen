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
        private static CajaRepository cajaRepository = new();
        private static VentaRepository ventaRepository = new();
        private static CompraRepository compraRepository = new();
        private static MovimientosCaja IniciarObjeto(MovimientosCajaModel model)
        {

            MovimientosCaja mov = new();
            mov.Id = model.Id;
            mov.Caja_Id = model.Caja_Id;
            mov.Descripción = model.Descripción;
            mov.FormaPago = model.FormaPago;
            mov.Ingreso = model.Ingreso;
            mov.Egreso = model.Egreso;
            mov.Total = model.Total;
            mov.Venta_Id = model.Venta_Id;
            mov.Compra_Id = model.Compra_Id;
            mov.Caja = cajaRepository.GetOne(model.Caja_Id);
            mov.Venta = ventaRepository.GetOne(model.Venta_Id);
            mov.Compras = compraRepository.GetOne(model.Compra_Id);

            return mov;
        }

        public List<MovimientosCaja> GetAll()
        {
            orden = $"SELECT * FROM MovimientosCaja ORDER BY Id ASC";
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
                    mov.Caja_Id = Convert.ToInt32(reader["Caja_Id"].ToString());
                    mov.Descripción = reader["Descripción"].ToString();
                    mov.FormaPago = reader["FormaPago"].ToString();
                    mov.Ingreso = Convert.ToDecimal(reader["Ingreso"].ToString());
                    mov.Egreso = Convert.ToDecimal(reader["Egreso"].ToString());
                    mov.Total = Convert.ToDecimal(reader["Total"].ToString());
                    mov.Venta_Id = Convert.ToInt32(reader["Venta_Id"].ToString());
                    mov.Compra_Id = Convert.ToInt32(reader["Compra_Id"].ToString());

                    mov.Caja = cajaRepository.GetOne(Convert.ToInt32(reader["Caja_Id"].ToString()));
                    mov.Venta = ventaRepository.GetOne(Convert.ToInt32(reader["Venta_Id"].ToString()));
                    mov.Compras = compraRepository.GetOne(Convert.ToInt32(reader["Compra_Id"].ToString()));

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

                try
                {
                    sqlcmd.Connection = conexion;
                    sqlcmd.Transaction = transaction;

                    orden = @"INSERT INTO MovimientosCaja (Caja_Id, Descripcion, FormaPago, Ingreso, Egreso, Total, Venta_Id, Compra_Id)
                            VALUES (@Caja_Id, @Descripcion, @FormaPago, @Ingreso, @Egreso, @Total, @Venta_Id, @Compra_Id) ";

                    sqlcmd.CommandText = orden;
                    sqlcmd.Parameters.AddWithValue("@Caja_Id", mov.Caja_Id);
                    sqlcmd.Parameters.AddWithValue("@Descripcion", mov.Descripción);
                    sqlcmd.Parameters.AddWithValue("@FormaPago", mov.FormaPago);
                    sqlcmd.Parameters.AddWithValue("@Ingreso", mov.Ingreso);
                    sqlcmd.Parameters.AddWithValue("@Egreso", mov.Egreso);
                    sqlcmd.Parameters.AddWithValue("@Total", mov.Total);
                    sqlcmd.Parameters.AddWithValue("@Venta_Id", mov.Venta_Id);
                    sqlcmd.Parameters.AddWithValue("@Compra_Id", mov.Compra_Id);

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
