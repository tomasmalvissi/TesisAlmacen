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
            user.FechaBaja = model.FechaBaja;
            return user;
        }
        public List<Usuarios> GetAll()
        {
            orden = "SELECT * FROM Usuarios ORDER BY Nombre ASC";

            List<Usuarios> usuarios = new();

            SqlCommand sqlcmd = new(orden, conexion);
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
                    user.FechaBaja = string.IsNullOrEmpty(reader["FechaBaja"].ToString()) ? null : Convert.ToDateTime(reader["FechaBaja"]);
                    usuarios.Add(user);
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

            return usuarios;
        }
        public Usuarios GetOne(int id)
        {
            orden = $"SELECT * FROM Usuarios WHERE Id ={id}";
            SqlCommand sqlcmd = new(orden, conexion);
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
                    usuario.FechaBaja = string.IsNullOrEmpty(reader["FechaBaja"].ToString()) ? null : Convert.ToDateTime(reader["FechaBaja"]);
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
            return usuario;
        }

        public Usuarios GetUser(string user)
        {
            orden = $"SELECT * FROM Usuarios WHERE Usuario='{user}'";
            SqlCommand sqlcmd = new(orden, conexion);
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
                    usuario.FechaBaja = string.IsNullOrEmpty(reader["FechaBaja"].ToString()) ? null : Convert.ToDateTime(reader["FechaBaja"]);
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
            return usuario;
        }

        public int Login(string user, string pass)
        {
            orden = @"SELECT Id FROM Usuarios WHERE Usuario=@user AND Contraseña=@pass AND FechaBaja IS NULL";
            SqlCommand sqlcmd = new(orden, conexion);
            int Id = 0;
            try
            {
                AbrirConex();
                sqlcmd.CommandText = orden;
                sqlcmd.Parameters.AddWithValue("@user", user);
                sqlcmd.Parameters.AddWithValue("@pass", pass);
                sqlcmd.ExecuteNonQuery();
                SqlDataReader reader = sqlcmd.ExecuteReader();

                while (reader.Read())
                {
                    Id = Convert.ToInt32(reader["Id"].ToString());
                }
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
            return Id;
        }

        public Usuarios Post(UsuarioModel model)
        {
            if (model == null)
            {
                throw new Exception("Error al tratar de ejecutar la operación");
            }
            else
            {
                Usuarios user = IniciarObjeto(model);
                SqlCommand sqlcmd = new(orden, conexion);
                try
                {
                    AbrirConex();
                    orden = $@"INSERT INTO Usuarios (Nombre, Email, Usuario, Contraseña) VALUES
                            (@Nombre, @Email, @Usuario, @Contraseña)";

                    sqlcmd.CommandText = orden;
                    sqlcmd.Parameters.AddWithValue("@Nombre", user.Nombre);
                    sqlcmd.Parameters.AddWithValue("@Email", user.Email);
                    sqlcmd.Parameters.AddWithValue("@Usuario", user.Usuario);
                    sqlcmd.Parameters.AddWithValue("@Contraseña", user.Contraseña);

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
                return user;
            }
        }

        public Usuarios Put(int id, UsuarioModel model)
        {
            Usuarios valoruser = GetOne(id);
            if (valoruser == null || model == null)
            {
                throw new Exception("Error al tratar de ejecutar la operación");
            }
            else
            {
                Usuarios user = IniciarObjeto(model);
                SqlCommand sqlcmd = new(orden, conexion);
                try
                {
                    AbrirConex();
                    orden = $@"UPDATE Usuarios SET Nombre=@Nombre, Email=@Email, Usuario=@Usuario, Contraseña=@Contraseña
                                               WHERE Id=@Id";

                    sqlcmd.CommandText = orden;
                    sqlcmd.Parameters.AddWithValue("@Id", id);
                    sqlcmd.Parameters.AddWithValue("@Nombre", user.Nombre);
                    sqlcmd.Parameters.AddWithValue("@Email", user.Email);
                    sqlcmd.Parameters.AddWithValue("@Usuario", user.Usuario);
                    sqlcmd.Parameters.AddWithValue("@Contraseña", user.Contraseña);

                    if (model.FechaBaja == null)
                        sqlcmd.Parameters.AddWithValue("@FechaBaja", null);
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

                return user;
            }
        }
        public int Delete(int id)
        {
            Usuarios valoruser = GetOne(id);
            if (valoruser != null)
            {
                SqlCommand sqlcmd = new(orden, conexion);
                try
                {
                    AbrirConex();
                    orden = $"UPDATE Usuarios SET FechaBaja=@FechaBaja WHERE Id=@Id";

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

