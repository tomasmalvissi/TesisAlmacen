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
    public class CajaRepository : DBConex
    {
        string orden;
        private static Caja IniciarObjeto(CajaModel model)
        {
            Caja ca = new();
            ca.Fecha = model.Fecha;
            ca.Empleado_Id = model.Empleado_Id;
            ca.Apertura = model.Apertura;
            ca.Cierre = model.Cierre;
            //ca.Empleado.Id = model.Empleado.Id;
            //ca.Empleado.Nombre = model.Empleado.Nombre;
            //ca.Empleado.Email = model.Empleado.Email;
            //ca.Empleado.Usuario = model.Empleado.Usuario;
            //ca.Empleado.Contraseña = model.Empleado.Contraseña;

            return ca;
        }

        public List<Caja> GetAll()
        {
            orden = $"SELECT * FROM Caja ORDER BY Nombre ASC";
            List<Caja> cajas = new();

            SqlCommand sqlcmd = new(orden, conexion);
            try
            {
                AbrirConex();
                sqlcmd.CommandText = orden;
                SqlDataReader reader = sqlcmd.ExecuteReader();

                while (reader.Read())
                {
                    Caja ca = new();
                    ca.Id = Convert.ToInt32(reader["Id"].ToString());
                    ca.Fecha = Convert.ToDateTime(reader["Fecha"].ToString());
                    ca.Empleado_Id = Convert.ToInt32(reader["Empleado_Id"].ToString());
                    ca.Apertura = Convert.ToSingle(reader["Apertura"].ToString());
                    ca.Cierre = Convert.ToSingle(reader["Cierre"].ToString());

                    Usuarios usuario = new();
                    UsuarioRepository usuarioRepository = new UsuarioRepository();
                    usuario = usuarioRepository.GetOne(Convert.ToInt32(reader["Empleado_Id"].ToString()));
                    ca.Empleado = usuario;

                    cajas.Add(ca);
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
            return cajas;
        }

        public Caja GetLast()
        {
            orden = @$"SELECT TOP 1 * FROM Caja
                        ORDER BY Id DESC";
            SqlCommand sqlcmd = new(orden, conexion);
            Caja caja = new();
            try
            {
                AbrirConex();
                sqlcmd.CommandText = orden;
                SqlDataReader reader = sqlcmd.ExecuteReader();

                while (reader.Read())
                {
                    caja.Id = Convert.ToInt32(reader["Id"].ToString());
                    caja.Fecha = Convert.ToDateTime(reader["Fecha"].ToString());
                    caja.Empleado_Id = Convert.ToInt32(reader["Empleado_Id"].ToString());
                    caja.Apertura = Convert.ToSingle(reader["Apertura"].ToString());
                    caja.Cierre = Convert.ToSingle(reader["Cierre"].ToString());

                    Usuarios usuario = new();
                    UsuarioRepository usuarioRepository = new UsuarioRepository();
                    usuario = usuarioRepository.GetOne(Convert.ToInt32(reader["Empleado_Id"].ToString()));
                    caja.Empleado = usuario;
                }

                CerrarConex();
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
            return caja;
        }

        public Caja GetOne(int id)
        {
            orden = $"SELECT * FROM Caja WHERE Id ={id}";
            SqlCommand sqlcmd = new(orden, conexion);
            Caja caja = new();
            try
            {
                AbrirConex();
                sqlcmd.CommandText = orden;
                SqlDataReader reader = sqlcmd.ExecuteReader();

                while (reader.Read())
                {
                    caja.Id = Convert.ToInt32(reader["Id"].ToString());
                    caja.Fecha = Convert.ToDateTime(reader["Fecha"].ToString());
                    caja.Empleado_Id = Convert.ToInt32(reader["Empleado_Id"].ToString());
                    caja.Apertura = Convert.ToSingle(reader["Apertura"].ToString());
                    caja.Cierre = Convert.ToSingle(reader["Cierre"].ToString());

                    Usuarios usuario = new();
                    UsuarioRepository usuarioRepository = new UsuarioRepository();
                    usuario = usuarioRepository.GetOne(Convert.ToInt32(reader["Empleado_Id"].ToString()));
                    caja.Empleado = usuario;
                }

                CerrarConex();
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
            return caja;
        }

        public Caja Post(CajaModel model)
        {
            if (model == null)
            {
                throw new Exception("Error al tratar de ejecutar la operación");
            }
            else
            {
                AbrirConex();

                SqlTransaction transaction;
                transaction = conexion.BeginTransaction();
                SqlCommand sqlcmd = new(orden, conexion, transaction);

                try
                {
                    sqlcmd.Connection = conexion;
                    sqlcmd.Transaction = transaction;

                    orden = @"INSERT INTO Caja (Fecha, Empleado_Id, Apertura, Cierre)
                            VALUES (@Fecha, @Empleado_Id, @Apertura, @Cierre) ";

                    sqlcmd.CommandText = orden;
                    sqlcmd.Parameters.AddWithValue("@Fecha", DateTime.Now);
                    sqlcmd.Parameters.AddWithValue("@Empleado_Id", model.Empleado_Id);
                    sqlcmd.Parameters.AddWithValue("@Apertura", model.Apertura);
                    sqlcmd.Parameters.AddWithValue("@Cierre", model.Cierre);

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

                var caja = IniciarObjeto(model);
                return caja;
            }
        }
        public Caja Put(int id, CajaModel model)
        {
            var ca = GetOne(id);
            if (ca == null || model == null)
            {
                throw new Exception("Error al tratar de ejecutar la operación");
            }
            else
            {
                Caja caja = IniciarObjeto(model);
                SqlCommand sqlcmd = new(orden, conexion);
                try
                {
                    AbrirConex();
                    orden = $@"UPDATE Caja 
                               SET Cierre=@Cierre WHERE Id=@Id";

                    sqlcmd.CommandText = orden;
                    sqlcmd.Parameters.AddWithValue("@Id", id);
                    sqlcmd.Parameters.AddWithValue("@Cierre", caja.Cierre);



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
                return caja;
            }
        }
    }
}
