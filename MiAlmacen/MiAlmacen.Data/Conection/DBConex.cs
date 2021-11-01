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
        //readonly string cadenaconex = "Server=tesis-server.database.windows.net;Initial Catalog=Tesis-DB;Persist Security Info=False;User ID=TomyMauri;Password=TesisCorrea2021;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        //SOMEE
        readonly string cadenaconex = "workstation id=AlmacenDBTesis.mssql.somee.com;packet size=4096;user id=Almacen2022_SQLLogin_1;pwd=Almacen2021;data source=AlmacenDBTesis.mssql.somee.com;persist security info=False;initial catalog=AlmacenDBTesis";
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
