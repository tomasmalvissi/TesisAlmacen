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
    public class MovimientoRepository : DBConex
    {
        string orden;

        public List<MovimientosCajaModel> GetAll()
        {
            orden = @"SELECT 'Venta' AS Movimiento, 
                    Id, 
                    Fecha, 
                    (SELECT c.Nombre FROM Clientes c WHERE c.Id = v.Cliente_Id) AS RazonSocial, 
                    (SELECT u.Nombre FROM Usuarios u WHERE u.Id = v.Empleado_Id) AS Empleado, 
                    Total AS Importe
                    FROM Ventas v
                    UNION
                    SELECT 'Compra' AS Movimiento, 
                    Id, 
                    Fecha, 
                    (SELECT p.Nombre FROM Proveedores p WHERE p.Id = cmp.Proveedor_Id) AS RazonSocial,
                    (SELECT u.Nombre FROM Usuarios u WHERE u.Id = cmp.Empleado_Id) AS Empleado, 
                    -Total AS Importe
                    FROM Compras cmp
                    ORDER BY Fecha DESC";

            List<MovimientosCajaModel> movimientos = new();

            SqlCommand sqlcmd = new(orden, conexion);
            try
            {
                AbrirConex();
                sqlcmd.CommandText = orden;
                SqlDataReader reader = sqlcmd.ExecuteReader();

                while (reader.Read())
                {
                    MovimientosCajaModel mov = new();
                    mov.Descripción = reader["Movimiento"].ToString();
                    mov.Id = Convert.ToInt32(reader["Id"].ToString());
                    mov.Fecha = Convert.ToDateTime(reader["Fecha"]);
                    mov.Importe = Convert.ToDecimal(reader["Importe"].ToString());
                    mov.RazonSocial = string.IsNullOrEmpty(reader["RazonSocial"].ToString()) ? null : reader["RazonSocial"].ToString();
                    mov.Empleado = string.IsNullOrEmpty(reader["Empleado"].ToString()) ? null : reader["Empleado"].ToString();

                    movimientos.Add(mov);
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
            return movimientos;
        }
    }
}