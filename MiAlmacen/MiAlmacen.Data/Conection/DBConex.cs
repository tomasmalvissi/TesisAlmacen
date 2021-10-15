using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAlmacen.Data.Conection
{
    public class DBConex
    {
        public SqlConnection conexion;
        //LOCAL
        //readonly string cadenaconex = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog = Almacen; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        //AZURE
        readonly string cadenaconex = "Server=tcp:tesis-server.database.windows.net,1433;Initial Catalog=Tesis-DB;Persist Security Info=False;User ID=TomyMauri;Password=TesisCorrea2021;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public DBConex()
        {
            conexion = new SqlConnection(cadenaconex);
        }

        public void AbrirConex() 
        {
            try
            {
                if (conexion.State == ConnectionState.Broken || conexion.State == ConnectionState.Closed)
                    conexion.Open();
            }
            catch (Exception e)
            {
                throw new Exception("Error al tratar de abrir la conexión", e);
            }
        }

        public void CerrarConex()
        {
            try
            {
                if (conexion.State == ConnectionState.Open)
                    conexion.Close();
            }
            catch (Exception e)
            {
                throw new Exception("Error al tratar de cerrar la conexión", e);
            }
        }
    }
}
