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
    public class ArticuloRepository : DBConex
    {
        string orden;
        private static Articulos IniciarObjeto(ArticuloModel model) 
        {
            Articulos art = new();
            art.Id = model.Id;
            art.Nombre = model.Nombre;
            art.Codigo_Art = model.Codigo_Art;
            art.Precio_Unit = model.Precio_Unit;
            art.Precio_Mayor = model.Precio_Mayor;
            art.Stock_Act = model.Stock_Act;
            art.Ultima_Modif = model.Ultima_Modif;

            return art;
        }

        public List<Articulos> GetAll() 
        {
            orden = $"SELECT * FROM Articulos ORDER BY Nombre ASC";
            List <Articulos> articulos = new();

            SqlCommand sqlcmd = new(orden, conexion);
            try
            {
                AbrirConex();
                sqlcmd.CommandText = orden;
                SqlDataReader reader = sqlcmd.ExecuteReader();

                while (reader.Read())
                {
                    Articulos art = new();
                    art.Id = Convert.ToInt32(reader["Id"].ToString());
                    art.Nombre = reader["Nombre"].ToString();
                    art.Codigo_Art = Convert.ToInt32(reader["Codigo_Art"].ToString());
                    art.Precio_Unit = Convert.ToSingle(reader["Precio_Unit"].ToString());
                    art.Precio_Mayor = Convert.ToSingle(reader["Precio_Mayor"].ToString());
                    art.Stock_Act = Convert.ToInt32(reader["Stock_Act"].ToString());
                    art.Ultima_Modif = Convert.ToDateTime(reader["Ultima_Modif"].ToString());
                    art.FechaBaja = string.IsNullOrEmpty(reader["FechaBaja"].ToString()) ? null : Convert.ToDateTime(reader["FechaBaja"]);
                    articulos.Add(art);
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
            return articulos;
        }

        public Articulos GetOne(int id) 
        {
            orden = $@"SELECT * FROM Articulos WHERE Id ={id}";

            SqlCommand sqlcmd = new SqlCommand(orden, conexion);
            Articulos articulo = new();
            try
            {
                AbrirConex();
                sqlcmd.CommandText = orden;
                SqlDataReader reader = sqlcmd.ExecuteReader();

                while (reader.Read())
                {
                    articulo.Id = Convert.ToInt32(reader["Id"].ToString());
                    articulo.Nombre = reader["Nombre"].ToString();
                    articulo.Codigo_Art = Convert.ToInt32(reader["Codigo_Art"].ToString());
                    articulo.Precio_Unit = Convert.ToSingle(reader["Precio_Unit"].ToString());
                    articulo.Precio_Mayor = Convert.ToSingle(reader["Precio_Mayor"].ToString());
                    articulo.Stock_Act = Convert.ToInt32(reader["Stock_Act"].ToString());
                    articulo.Ultima_Modif = Convert.ToDateTime(reader["Ultima_Modif"].ToString());
                    articulo.FechaBaja = string.IsNullOrEmpty(reader["FechaBaja"].ToString()) ? null : Convert.ToDateTime(reader["FechaBaja"]);
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
            return articulo;
        }

        public Articulos Post(ArticuloModel model) 
        {
            if (model == null)
            {
                throw new Exception("Error al tratar de ejecutar la operación");
            }
            else
            {
                Articulos art = IniciarObjeto(model);
                SqlCommand sqlcmd = new(orden, conexion);
                try
                {
                    AbrirConex();
                    orden = $@"INSERT INTO Articulos (Nombre, Codigo_Art, Precio_Unit, Precio_Mayor, Stock_Act, Ultima_Modif) 
                            VALUES (@Nombre, @Codigo_Art, @Precio_Unit, @Precio_Mayor, @Stock_Act, @Ultima_Modif)";

                    sqlcmd.CommandText = orden;
                    sqlcmd.Parameters.AddWithValue("@Nombre", art.Nombre);
                    sqlcmd.Parameters.AddWithValue("@Codigo_Art", art.Codigo_Art);
                    sqlcmd.Parameters.AddWithValue("@Precio_Unit", art.Precio_Unit);
                    sqlcmd.Parameters.AddWithValue("@Precio_Mayor", art.Precio_Mayor);
                    sqlcmd.Parameters.AddWithValue("@Stock_Act", art.Stock_Act);
                    sqlcmd.Parameters.AddWithValue("@Ultima_Modif", art.Ultima_Modif);


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



                return art;
            }
        }
        public Articulos Put(int id, ArticuloModel model)
        {
            var articulo = GetOne(id);
            if (articulo == null || model == null)
            {
                throw new Exception("Error al tratar de ejecutar la operación");
            }
            else
            {
                Articulos art = IniciarObjeto(model);
                SqlCommand sqlcmd = new(orden, conexion);
                try
                {
                    AbrirConex();
                    orden = $@"UPDATE Articulos SET Nombre=@Nombre, Codigo_Art=@Codigo_Art, Precio_Unit=@Precio_Unit, Precio_Mayor=@Precio_Mayor, Stock_Act=@Stock_Act, Ultima_Modif=@Ultima_Modif
                                                WHERE Id=@Id";

                    sqlcmd.CommandText = orden;
                    sqlcmd.Parameters.AddWithValue("@Id", id);
                    sqlcmd.Parameters.AddWithValue("@Nombre", art.Nombre);
                    sqlcmd.Parameters.AddWithValue("@Codigo_Art", art.Codigo_Art);
                    sqlcmd.Parameters.AddWithValue("@Precio_Unit", art.Precio_Unit);
                    sqlcmd.Parameters.AddWithValue("@Precio_Mayor", art.Precio_Mayor);
                    sqlcmd.Parameters.AddWithValue("@Stock_Act", art.Stock_Act);
                    sqlcmd.Parameters.AddWithValue("@Ultima_Modif", art.Ultima_Modif);

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
                return art;
            }
        }
        public int Delete(int id)
        {
            var valorart = GetOne(id);
            if (valorart != null)
            {
                SqlCommand sqlcmd = new(orden, conexion);
                try
                {
                    AbrirConex();
                    orden = $"UPDATE Articulos SET FechaBaja=@FechaBaja WHERE Id=@Id";

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
