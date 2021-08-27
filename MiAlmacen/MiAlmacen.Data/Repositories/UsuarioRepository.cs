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
        string orden;
        private Usuarios IniciarObjeto(UsuarioModel model)   
        {
            Usuarios user = new();
            user.Id = model.Id;
            user.Nombre = model.Nombre;
            user.Usuario = model.Usuario;
            user.Email = model.Email;
            user.Contraseña = model.Contraseña;
            return user;
        }
        public List<Usuarios> Get(int? id)
        {
            if (id == null)
                orden = "SELECT * FROM Usuarios";
            else
                orden = "SELECT * FROM Usuarios WHERE Id = '" + id + "';";

            List<Usuarios> usuarios = new();

            SqlCommand sqlcmd = new SqlCommand(orden, conexion);
            try
            {
                AbrirConex();
                sqlcmd.CommandText = orden;
                SqlDataReader reader = sqlcmd.ExecuteReader();

                while (reader.Read())
                {
                    Usuarios user = new();
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
            if (usuarios.Count == 0)
            {
                throw new Exception("No se encontraron resultados.");
            }
            return usuarios;
        }
        public Usuarios Post(UsuarioModel model)
        {
            if (model == null)
            {
                throw new Exception("Error al tratar de ejecutar la operación");
            }
            orden = "INSERT INTO Usuarios values (" + "'" + model.Nombre + "',"
                                                    + "'" + model.Email + "',"
                                                    + "'" + model.Usuario + "',"
                                                    + "'" + model.Contraseña + "'" + ");";
            AccionSQL(orden);
            return IniciarObjeto(model);
        }

        public Usuarios Put(int id, UsuarioModel model)
        {
            var valoruser = Get(id);
            if (valoruser == null || model == null)
            {
                throw new Exception("Error al tratar de ejecutar la operación");
            }
            orden = "UPDATE Usuarios SET "
                            + "Nombre= '" + model.Nombre + "',"
                            + "Email= '" + model.Email + "',"
                            + "Usuario= '" + model.Usuario + "',"
                            + "Contraseña= '" + model.Contraseña + "'"
                            + "where Id= " + id;
            AccionSQL(orden);
            return IniciarObjeto(model);
        }
        public List<Usuarios> Delete(int id)
        {
            var valoruser = Get(id);
            orden = "DELETE FROM Usuarios WHERE Id = " + id;
            AccionSQL(orden);
            return valoruser;
        }
    }
}

