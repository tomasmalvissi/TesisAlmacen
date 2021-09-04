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
        private static Usuarios IniciarObjeto(UsuarioModel model)   
        {
            Usuarios user = new();
            user.Id = model.Id;
            user.Nombre = model.Nombre;
            user.Usuario = model.Usuario;
            user.Email = model.Email;
            user.Contraseña = model.Contraseña;
            return user;
        }
        public List<Usuarios> GetAll()
        {
            orden = "SELECT * FROM Usuarios WHERE FechaBaja IS NOT NULL";

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
        public Usuarios GetOne(int id)
        {
            orden = $"SELECT * FROM Usuarios WHERE Id ={id}";
            SqlCommand sqlcmd = new SqlCommand(orden, conexion);
            Usuarios usuario = new();
            try
            {
                AbrirConex();
                sqlcmd.CommandText = orden;
                SqlDataReader reader = sqlcmd.ExecuteReader();

                while (reader.Read())
                {
                    usuario.Id = Convert.ToInt32(reader["Id"].ToString());
                    usuario.Nombre = reader["Nombre"].ToString();
                    usuario.Email = reader["Email"].ToString();
                    usuario.Usuario = reader["Usuario"].ToString();
                    usuario.Contraseña = reader["Contraseña"].ToString();
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
            return usuario;
        }
        public Usuarios Post(UsuarioModel model)
        {
            if (model == null)
            {
                throw new Exception("Error al tratar de ejecutar la operación");
            }
            orden = $"INSERT INTO Usuarios VALUES (" +
                $"{model.Nombre}, {model.Email}, {model.Usuario}, {model.Contraseña})";

            AccionSQL(orden);
            return IniciarObjeto(model);
        }

        public Usuarios Put(int id, UsuarioModel model)
        {
            var valoruser = GetOne(id);
            if (valoruser == null || model == null)
            {
                throw new Exception("Error al tratar de ejecutar la operación");
            }
            orden = $"UPDATE Usuarios SET Nombre={model.Nombre}, " +
                                        $"Email={model.Email}, " +
                                        $"Usuario={model.Usuario}, " +
                                        $"Contraseña={model.Contraseña } " +
                                        $"WHERE Id={id}";
            AccionSQL(orden);
            return IniciarObjeto(model);
        }
        public int Delete(int id)
        {
            var valoruser = GetOne(id);
            if (valoruser != null)
            {
                orden = $"UPDATE Usuarios SET FechaBaja={DateTime.Now} WHERE Id={id}";
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

