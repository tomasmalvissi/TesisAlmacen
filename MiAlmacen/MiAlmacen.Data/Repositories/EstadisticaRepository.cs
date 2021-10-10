using MiAlmacen.Data.Conection;
using MiAlmacen.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAlmacen.Data.Repositories
{
    public class EstadisticaRepository : DBConex
    {
        string orden;

        public List<Periodo> GetVentasPeriodo()
        {
            orden = @"SELECT DATENAME(MONTH, v.Fecha) AS Mes,
                        SUM(v.Total) AS Monto
                        FROM Ventas v
                        WHERE YEAR(v.Fecha) = YEAR(GETDATE()) AND FechaBaja IS NULL
                        GROUP BY DATENAME(MONTH, v.Fecha)
                        ORDER BY Mes ASC";

            List<Periodo> periodos = new();

            SqlCommand sqlcmd = new(orden, conexion);
            try
            {
                AbrirConex();
                sqlcmd.CommandText = orden;
                SqlDataReader reader = sqlcmd.ExecuteReader();

                while (reader.Read())
                {
                    Periodo periodo = new();
                    periodo.Mes = reader["Mes"].ToString();
                    periodo.Monto = Convert.ToDecimal(reader["Monto"].ToString());
                    periodos.Add(periodo);
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
            return periodos;
        }

        public List<Periodo> GetComprasPeriodo()
        {
            orden = @"SELECT DATENAME(MONTH, c.Fecha) AS Mes,
                        SUM(c.Total) AS Monto
                        FROM Compras c
                        WHERE YEAR(c.Fecha) = YEAR(GETDATE()) AND FechaBaja IS NULL
                        GROUP BY DATENAME(MONTH, c.Fecha)
                        ORDER BY Mes ASC";

            List<Periodo> periodos = new();

            SqlCommand sqlcmd = new(orden, conexion);
            try
            {
                AbrirConex();
                sqlcmd.CommandText = orden;
                SqlDataReader reader = sqlcmd.ExecuteReader();

                while (reader.Read())
                {
                    Periodo periodo = new();
                    periodo.Mes = reader["Mes"].ToString();
                    periodo.Monto = Convert.ToDecimal(reader["Monto"].ToString());
                    periodos.Add(periodo);
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
            return periodos;
        }

        public List<Top> GetTopProductos()
        {
            orden = @"SELECT TOP 5 a.Nombre AS Producto, SUM(Articulo_Id) CantidadVendido
                    FROM DetalleVentas dv
                    INNER JOIN Articulos a on Articulo_Id = dv.Articulo_Id
                    GROUP BY a.Nombre
                    ORDER BY CantidadVendido DESC;";

            List<Top> tops = new();

            SqlCommand sqlcmd = new(orden, conexion);
            try
            {
                AbrirConex();
                sqlcmd.CommandText = orden;
                SqlDataReader reader = sqlcmd.ExecuteReader();

                while (reader.Read())
                {
                    Top top = new();
                    top.Clave = Convert.ToString(reader["Producto"].ToString());
                    top.Valor = Convert.ToInt32(reader["CantidadVendido"].ToString());
                    tops.Add(top);
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
            return tops;
        }

        public List<Top> GetTopClientes()
        {
            orden = @"SELECT TOP 5 c.Nombre AS Cliente, SUM(Cliente_Id) CantidadVentas
                    FROM Ventas v
                    INNER JOIN Clientes c on c.Id = v.Cliente_Id
                    GROUP BY c.Nombre
                    ORDER BY CantidadVentas DESC;";

            List<Top> tops = new();

            SqlCommand sqlcmd = new(orden, conexion);
            try
            {
                AbrirConex();
                sqlcmd.CommandText = orden;
                SqlDataReader reader = sqlcmd.ExecuteReader();

                while (reader.Read())
                {
                    Top top = new();
                    top.Clave = Convert.ToString(reader["Cliente"].ToString());
                    top.Valor = Convert.ToInt32(reader["CantidadVentas"].ToString());
                    tops.Add(top);
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
            return tops;
        }
    }
}