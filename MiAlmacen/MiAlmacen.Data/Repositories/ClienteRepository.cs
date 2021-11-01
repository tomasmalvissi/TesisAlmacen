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
    public class ClienteRepository : DBConex
    {
        string orden;
        private static Clientes IniciarObjeto(ClienteModel model)
        {
            Clientes cli = new();
            cli.Id = model.Id;
            cli.Nombre = model.Nombre;
            cli.DNI = model.DNI;
            cli.Direccion = model.Direccion;
            cli.Telefono = model.Telefono;
            cli.FechaBaja = model.FechaBaja;
            return cli;
        }
        public List<Clientes> GetAll()
        {
            orden = $@"SELECT * FROM Clientes ORDER BY Nombre ASC";

            List<Clientes> clientes = new();

            SqlCommand sqlcmd = new SqlCommand(orden, conexion);
            try
            {
                AbrirConex();
                sqlcmd.CommandText = orden;
                SqlDataReader reader = sqlcmd.ExecuteReader();

                while (reader.Read())
                {
                    Clientes cli = new();
                    cli.Id = Convert.ToInt32(reader["Id"].ToString());
                    cli.Nombre = reader["Nombre"].ToString();
                    cli.DNI = Convert.ToInt64(reader["DNI"].ToString());
                    cli.Direccion = reader["Direccion"].ToString();
                    cli.Telefono = Convert.ToInt64(reader["Telefono"].ToString());
                    cli.FechaBaja = string.IsNullOrEmpty(reader["FechaBaja"].ToString()) ? null : Convert.ToDateTime(reader["FechaBaja"]);
                    clientes.Add(cli);
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
            return clientes;
        }
        public Clientes GetOne(int id)
        {
            orden = $@"SELECT * FROM Clientes WHERE Id ={id}";
            SqlCommand sqlcmd = new SqlCommand(orden, conexion);
            Clientes cliente = new();
            try
            {
                AbrirConex();
                sqlcmd.CommandText = orden;
                SqlDataReader reader = sqlcmd.ExecuteReader();

                while (reader.Read())
                {
                    cliente.Id = Convert.ToInt32(reader["Id"].ToString());
                    cliente.Nombre = reader["Nombre"].ToString();
                    cliente.DNI = Convert.ToInt32(reader["DNI"].ToString());
                    cliente.Direccion = reader["Direccion"].ToString();
                    cliente.Telefono = Convert.ToInt64(reader["Telefono"].ToString());
                    cliente.FechaBaja = string.IsNullOrEmpty(reader["FechaBaja"].ToString()) ? null : Convert.ToDateTime(reader["FechaBaja"]);
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
            return cliente;
        }

        public decimal GetDeuda(int id)
        {
            decimal deuda = 0;
            orden = @"SELECT ISNULL((SELECT SUM(v.Saldo) 
                      FROM Ventas v
                      WHERE v.Cliente_Id = @Id), 0) AS Deuda";

            SqlCommand sqlcmd = new SqlCommand(orden, conexion);
            sqlcmd.Parameters.AddWithValue("@Id", id);

            Clientes cliente = new();
            try
            {
                AbrirConex();
                sqlcmd.CommandText = orden;
                SqlDataReader reader = sqlcmd.ExecuteReader();

                while (reader.Read())
                {
                    deuda = Convert.ToDecimal(reader["Deuda"].ToString());
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
            return deuda;
        }

        public Clientes Post(ClienteModel model)
        {
            if (model == null)
            {
                throw new Exception("Error al tratar de ejecutar la operación");
            }
            else
            {
                Clientes cli = IniciarObjeto(model);
                SqlCommand sqlcmd = new(orden, conexion);
                try
                {
                    AbrirConex();
                    orden = $@"INSERT INTO Clientes (Nombre, DNI, Direccion, Telefono) 
                                VALUES (@Nombre, @DNI, @Direccion, @Telefono)";

                    sqlcmd.CommandText = orden;
                    sqlcmd.Parameters.AddWithValue("@Nombre", cli.Nombre);
                    sqlcmd.Parameters.AddWithValue("@DNI", cli.DNI);
                    sqlcmd.Parameters.AddWithValue("@Direccion", cli.Direccion);
                    sqlcmd.Parameters.AddWithValue("@Telefono", cli.Telefono);

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

                return cli;
            }
        }

        public Clientes Put(int id, ClienteModel model)
        {
            Clientes cliente = GetOne(id);
            if (cliente == null || model == null)
            {
                throw new Exception("Error al tratar de ejecutar la operación");
            }
            else
            {
                Clientes cli = IniciarObjeto(model);
                SqlCommand sqlcmd = new(orden, conexion);
                try
                {
                    AbrirConex();
                    orden = $@"UPDATE Clientes SET Nombre=@Nombre, DNI=@DNI, Direccion=@Direccion, Telefono=@Telefono, FechaBaja=@FechaBaja
                                           WHERE Id=@Id";

                    sqlcmd.CommandText = orden;
                    sqlcmd.Parameters.AddWithValue("@Id", id);
                    sqlcmd.Parameters.AddWithValue("@Nombre", cli.Nombre);
                    sqlcmd.Parameters.AddWithValue("@DNI", cli.DNI);
                    sqlcmd.Parameters.AddWithValue("@Direccion", cli.Direccion);
                    sqlcmd.Parameters.AddWithValue("@Telefono", cli.Telefono);

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

                return cli;
            }
        }
        public int Delete(int id)
        {
            Clientes valorcli = GetOne(id);
            if (valorcli != null)
            {
                SqlCommand sqlcmd = new(orden, conexion);
                try
                {
                    AbrirConex();
                    orden = $"UPDATE Clientes SET FechaBaja=@FechaBaja WHERE Id=@Id";

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
