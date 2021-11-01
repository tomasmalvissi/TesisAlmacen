using MiAlmacen.Data.Conection;
using MiAlmacen.Data.Entities;
using MiAlmacen.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MiAlmacen.Data.Repositories
{
    public class ProveedorRepository : DBConex
    {
        string orden;
        private static Proveedores IniciarObjeto(ProveedorModel model)
        {
            Proveedores prov = new();
            prov.Id = model.Id;
            prov.Nombre = model.Nombre;
            prov.CUIL = model.CUIL;
            prov.Direccion = model.Direccion;
            prov.Telefono = model.Telefono;
            prov.FechaBaja = model.FechaBaja;
            return prov;
        }
        public List<Proveedores> GetAll()
        {
            orden = $@"SELECT * FROM Proveedores ORDER BY Nombre ASC";

            List<Proveedores> proveedores = new();

            SqlCommand sqlcmd = new SqlCommand(orden, conexion);
            try
            {
                AbrirConex();
                sqlcmd.CommandText = orden;
                SqlDataReader reader = sqlcmd.ExecuteReader();

                while (reader.Read())
                {
                    Proveedores prov = new();
                    prov.Id = Convert.ToInt32(reader["Id"].ToString());
                    prov.Nombre = reader["Nombre"].ToString();
                    prov.CUIL = Convert.ToInt64(reader["CUIL"].ToString());
                    prov.Direccion = reader["Direccion"].ToString();
                    prov.Telefono = Convert.ToInt64(reader["Telefono"].ToString());
                    prov.FechaBaja = string.IsNullOrEmpty(reader["FechaBaja"].ToString()) ? null : Convert.ToDateTime(reader["FechaBaja"]);
                    proveedores.Add(prov);
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
            return proveedores;
        }
        public Proveedores GetOne(int id)
        {
            orden = $@"SELECT * FROM Proveedores WHERE Id ={id}";
            SqlCommand sqlcmd = new SqlCommand(orden, conexion);
            Proveedores proveedor = new();
            try
            {
                AbrirConex();
                sqlcmd.CommandText = orden;
                SqlDataReader reader = sqlcmd.ExecuteReader();

                while (reader.Read())
                {
                    proveedor.Id = Convert.ToInt32(reader["Id"].ToString());
                    proveedor.Nombre = reader["Nombre"].ToString();
                    proveedor.CUIL = Convert.ToInt64(reader["CUIL"].ToString());
                    proveedor.Direccion = reader["Direccion"].ToString();
                    proveedor.Telefono = Convert.ToInt64(reader["Telefono"].ToString());
                    proveedor.FechaBaja = string.IsNullOrEmpty(reader["FechaBaja"].ToString()) ? null : Convert.ToDateTime(reader["FechaBaja"]);
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
            return proveedor;
        }

        public bool ExisteProveedor(long nroCUIT)
        {
            bool existe = false;

            orden = @"SELECT Id FROM Proveedores WHERE CUIL = @NroCUIT";

            SqlCommand sqlcmd = new(orden, conexion);
            Compras compra = new();
            try
            {
                AbrirConex();
                sqlcmd.CommandText = orden;
                sqlcmd.Parameters.AddWithValue("@NroCUIT", nroCUIT);
                SqlDataReader reader = sqlcmd.ExecuteReader();

                if (reader.HasRows)
                    existe = true;

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
            return existe;
        }

        public Proveedores Post(ProveedorModel model)
        {
            if (model == null)
            {
                throw new Exception("Error al tratar de ejecutar la operación");
            }
            else
            {
                Proveedores prov = IniciarObjeto(model);
                SqlCommand sqlcmd = new(orden, conexion);
                try
                {
                    AbrirConex();
                    orden = $@"INSERT INTO Proveedores (Nombre, CUIL, Direccion, Telefono) 
                                VALUES (@Nombre, @CUIL, @Direccion, @Telefono)";

                    sqlcmd.CommandText = orden;
                    sqlcmd.Parameters.AddWithValue("@Nombre", prov.Nombre);
                    sqlcmd.Parameters.AddWithValue("@CUIL", prov.CUIL);
                    sqlcmd.Parameters.AddWithValue("@Direccion", prov.Direccion);
                    sqlcmd.Parameters.AddWithValue("@Telefono", prov.Telefono);

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

                return prov;
            }
        }

        public Proveedores Put(int id, ProveedorModel model)
        {
            Proveedores proveedor = GetOne(id);
            if (proveedor == null || model == null)
            {
                throw new Exception("Error al tratar de ejecutar la operación");
            }
            else
            {
                Proveedores prov = IniciarObjeto(model);
                SqlCommand sqlcmd = new(orden, conexion);
                try
                {
                    AbrirConex();
                    orden = $@"UPDATE Proveedores SET Nombre=@Nombre, CUIL=@CUIL, Direccion=@Direccion, Telefono=@Telefono, FechaBaja=@FechaBaja
                                           WHERE Id=@Id";

                    sqlcmd.CommandText = orden;
                    sqlcmd.Parameters.AddWithValue("@Id", id);
                    sqlcmd.Parameters.AddWithValue("@Nombre", prov.Nombre);
                    sqlcmd.Parameters.AddWithValue("@CUIL", prov.CUIL);
                    sqlcmd.Parameters.AddWithValue("@Direccion", prov.Direccion);
                    sqlcmd.Parameters.AddWithValue("@Telefono", prov.Telefono);

                    if (model.FechaBaja == null)
                        sqlcmd.Parameters.AddWithValue("@FechaBaja", DBNull.Value);
                    else
                        sqlcmd.Parameters.AddWithValue("@FechaBaja", DateTime.Now);

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

                return prov;
            }
        }
        public int Delete(int id)
        {
            Proveedores valorprov = GetOne(id);
            if (valorprov != null)
            {
                SqlCommand sqlcmd = new(orden, conexion);
                try
                {
                    AbrirConex();
                    orden = $"UPDATE Proveedores SET FechaBaja=@FechaBaja WHERE Id=@Id";

                    sqlcmd.CommandText = orden;
                    sqlcmd.Parameters.AddWithValue("@Id", id);
                    sqlcmd.Parameters.AddWithValue("@FechaBaja", DateTime.Now);

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
            }
            else
            {
                id = 0;
            }
            return id;
        }
    }
}
