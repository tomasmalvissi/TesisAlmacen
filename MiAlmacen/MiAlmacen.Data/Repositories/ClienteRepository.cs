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
        int resultado;
        string orden;
        private int AccionSQL(string orden)
        {
            SqlCommand sqlcmd = new SqlCommand(orden, conexion);
            try
            {
                AbrirConex();
                resultado = sqlcmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {

                throw new Exception("Error al tratar de ejecutar la operación", e);
            }
            finally
            {
                CerrarConex();
                sqlcmd.Dispose();
            }
            return resultado;
        }
        private Clientes IniciarObjeto(ClienteModel model)
        {
            Clientes cli = new();
            cli.Id = model.Id;
            cli.Nombre = model.Nombre;
            cli.Direccion = model.Direccion;
            cli.Telefono = model.Telefono;
            return cli;
        }
        public List<Clientes> Get(int? id)
        {
            if (id == null)
                orden = "SELECT * FROM Clientes";
            else
                orden = "SELECT * FROM Clientes WHERE Id = '" + id + "';";

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
        public Clientes Post(ClienteModel model)
        {
            if (model == null)
            {
                throw new Exception("Error al tratar de ejecutar la operación");
            }
            orden = "INSERT INTO Clientes values (" + "'" + model.Nombre + "',"
                                                    + "'" + model.Direccion + "',"
                                                    + "'" + model.Telefono + "'" + ");";
            AccionSQL(orden);
            return IniciarObjeto(model);
        }

        public Clientes Put(int id, ClienteModel model)
        {
            var valorcli = Get(id);
            if (valorcli == null || model == null)
            {
                throw new Exception("Error al tratar de ejecutar la operación");
            }
            orden = "UPDATE Clientes SET "
                            + "Nombre= '" + model.Nombre + "',"
                            + "Direccion= '" + model.Direccion + "',"
                            + "Telefono= '" + model.Telefono + "'"
                            + "where Id= " + id;
            AccionSQL(orden);
            return IniciarObjeto(model);
        }
        public List<Clientes> Delete(int id)
        {
            var valorcli = Get(id);
            orden = "DELETE FROM Clientes WHERE Id = " + id;
            AccionSQL(orden);
            return valorcli;
        }
    }
}
