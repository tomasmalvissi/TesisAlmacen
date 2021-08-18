using MiAlmacen.Data.Conection;
using MiAlmacen.Data.Entities;
using MiAlmacen.Model.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAlmacen.Data.Repositories
{
    public class UsuarioRepository : DBConex
    {
        int resultado;
        string orden;
        public int AccionSQL(string orden)
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

        public List<UsuarioModel> Get(int? id)
        {
            if (id == null)
                orden = "SELECT * FROM Usuarios";
            else
                orden = "SELECT * FROM Usuarios WHERE Id = '" + id + "';";

            List<UsuarioModel> usuarios = new();

            SqlCommand sqlcmd = new SqlCommand(orden, conexion);
            try
            {
                AbrirConex();
                sqlcmd.CommandText = orden;
                SqlDataReader reader = sqlcmd.ExecuteReader();

                while (reader.Read())
                {
                    UsuarioModel user = new();
                    user.Id = Convert.ToInt32(reader["Id"].ToString());
                    user.Nombre = reader["Nombre"].ToString();
                    user.Email = reader["Email"].ToString();
                    user.Usuario = reader["Usuario"].ToString();
                    user.Contraseña = reader["Contraseña"].ToString();
                    usuarios.Add(user);
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
            return usuarios;
        }
        public UsuarioModel Post(UsuarioModel usuario)
        {
            if (usuario == null)
            {
                throw new Exception("Error al tratar de ejecutar la operación");
            }
            orden = "INSERT INTO Usuarios values (" + "'" + usuario.Nombre + "',"
                                                    + "'" + usuario.Email + "',"
                                                    + "'" + usuario.Usuario + "',"
                                                    + "'" + usuario.Contraseña + "'" + ");";
            AccionSQL(orden);
            return usuario;
        }

        public UsuarioModel Put(int id, UsuarioModel usuario)
        {
            var valoruser = Get(id);
            if (valoruser == null || usuario == null)
            {
                throw new Exception("Error al tratar de ejecutar la operación");
            }
            orden = "UPDATE Usuarios SET "
                            + "Nombre= '" + usuario.Nombre + "',"
                            + "Email= '" + usuario.Email + "',"
                            + "Usuario= '" + usuario.Usuario + "',"
                            + "Contraseña= '" + usuario.Contraseña + "'"
                            + "where Id= " + id;
            AccionSQL(orden);
            return usuario;
        }
        public List<UsuarioModel> Delete(int id)
        {
            var valoruser = Get(id);
            orden = "DELETE FROM Usuarios WHERE Id = " + id;
            AccionSQL(orden);
            return valoruser;
        }
    }
}

