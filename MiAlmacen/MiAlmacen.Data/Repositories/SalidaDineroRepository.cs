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
    public class SalidaDineroRepository : DBConex
    {
        string orden;
        private static SalidasDinero IniciarObjeto(SalidasDineroModel model)
        {
            CajaRepository cajaRepository = new();

            SalidasDinero sd = new();
            sd.Id = model.Id;
            sd.Descripcion = model.Descripcion;
            sd.Importe = model.Importe;
            sd.Caja_Id = model.Caja_Id;
            sd.Caja = cajaRepository.GetOne(model.Caja_Id);
            return sd;
        }

        public List<SalidasDinero> GetAll(int idcaja)
        {
            orden = @"SELECT * FROM SalidasDinero 
                      WHERE Caja_Id = @Caja_Id;";
            List<SalidasDinero> salidas = new();

            SqlCommand sqlcmd = new(orden, conexion);
            try
            {
                AbrirConex();
                sqlcmd.CommandText = orden;
                sqlcmd.Parameters.AddWithValue("@Caja_Id", idcaja);
                SqlDataReader reader = sqlcmd.ExecuteReader();

                while (reader.Read())
                {
                    SalidasDinero sd = new();
                    sd.Id = Convert.ToInt32(reader["Id"].ToString());
                    sd.Descripcion = reader["Descripcion"].ToString();
                    sd.Importe = Convert.ToDecimal(reader["Importe"].ToString());
                    sd.Caja_Id = Convert.ToInt32(reader["Caja_Id"].ToString());

                    Caja caja = new();
                    CajaRepository cajaRepository = new();
                    caja = cajaRepository.GetOne(Convert.ToInt32(reader["Caja_Id"].ToString()));
                    sd.Caja = caja;

                    salidas.Add(sd);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al tratar de ejecutar la operación " + ex.Message);
            }
            finally
            {
                CerrarConex();
                sqlcmd.Dispose();
            }
            return salidas;
        }

        public SalidasDinero Post(SalidasDineroModel model)
        {
            if (model == null)
            {
                throw new Exception("Error al tratar de ejecutar la operación");
            }
            else
            {
                AbrirConex();

                //SqlTransaction transaction;
                //transaction = conexion.BeginTransaction();
                SqlCommand sqlcmd = new(orden, conexion);
                SalidasDinero salida = IniciarObjeto(model);

                try
                {
                    sqlcmd.Connection = conexion;

                    orden = @"INSERT INTO SalidasDinero (Descripcion, Importe, Caja_Id)
                            VALUES (@Descripcion, @Importe, @Caja_Id) ";

                    sqlcmd.CommandText = orden;
                    sqlcmd.Parameters.AddWithValue("@Descripcion", salida.Descripcion);
                    sqlcmd.Parameters.AddWithValue("@Importe", salida.Importe);
                    sqlcmd.Parameters.AddWithValue("@Caja_Id", salida.Caja_Id);

                    sqlcmd.ExecuteNonQuery();
                    sqlcmd.Parameters.Clear();

                    orden = @"UPDATE Caja
                            SET Actual = Actual - @Importe
                            WHERE Id = @Caja_Id ";

                    sqlcmd.CommandText = orden;
                    sqlcmd.Parameters.AddWithValue("@Importe", salida.Importe);
                    sqlcmd.Parameters.AddWithValue("@Caja_Id", salida.Caja_Id);

                    sqlcmd.ExecuteNonQuery();
                    sqlcmd.Parameters.Clear();
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

                return salida;
            }
        }
    }
}
