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
                    cli.Direccion = reader["Direccion"].ToString();
                    cli.Telefono = reader["Telefono"].ToString();
                    cli.FechaBaja = string.IsNullOrEmpty(reader["FechaBaja"].ToString()) ? null : Convert.ToDateTime(reader["FechaBaja"]);
                    clientes.Add(cli);
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
                    cliente.Direccion = reader["Direccion"].ToString();
                    cliente.Telefono = reader["Telefono"].ToString();
                    cliente.FechaBaja = string.IsNullOrEmpty(reader["FechaBaja"].ToString()) ? null : Convert.ToDateTime(reader["FechaBaja"]);
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
            return cliente;
        }
        public Clientes Post(ClienteModel model)
        {
            if (model == null)
            {
                throw new Exception("Error al tratar de ejecutar la operación");
            }
            else
            {
                orden = $@"INSERT INTO Clientes (Nombre, Direccion, Telefono) 
                    VALUES ('{model.Nombre}','{model.Direccion}','{model.Telefono}')";
                AccionSQL(orden);
                return IniciarObjeto(model);
            }
        }

        public Clientes Put(int id, ClienteModel model)
        {
            var cliente = GetOne(id);
            if (cliente == null || model == null)
            {
                throw new Exception("Error al tratar de ejecutar la operación");
            }
            else
            {
                orden = $@"UPDATE Clientes SET Nombre='{model.Nombre}',
                                            Direccion='{model.Direccion}',
                                            Telefono='{model.Telefono}',";
                if (model.FechaBaja == null)
                    orden += $"FechaBaja = null";
                else
                    orden += $"FechaBaja = '{model.FechaBaja}'";
                orden += $" WHERE Id={model.Id}";

                AccionSQL(orden);
                return IniciarObjeto(model);
            }
        }
        public int Delete(int id)
        {
            var valorcli = GetOne(id);
            if (valorcli != null)
            {
                orden = $@"UPDATE Clientes SET FechaBaja='{DateTime.Now}' WHERE Id={id}";
                AccionSQL(orden);
            }
            else
            {
                id = 0;
            }
            return id;
        }
    }
}
