using MiAlmacen.Data.Conection;
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
        public void GetSQL(string orden)
        {
            SqlCommand sqlcmd = new SqlCommand(orden, conexion);
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                AbrirConex();
                sqlcmd.ExecuteNonQuery();
                da.SelectCommand = sqlcmd;
                da.Fill(ds);
            }
            catch (Exception e)
            {
                throw new Exception("Error al listar datos", e);
            }
            finally
            {
                CerrarConex();
                sqlcmd.Dispose();
            }
        }
        public void Get()
        {
            orden = "SELECT * FROM Usuarios";
            GetSQL(orden);
        }
    }
}
