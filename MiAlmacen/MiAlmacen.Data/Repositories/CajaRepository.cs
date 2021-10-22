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
    public class CajaRepository : DBConex
    {
        string orden;
        private static Caja IniciarObjeto(CajaModel model)
        {
            Caja ca = new();
            ca.Fecha = model.Fecha;
            ca.Empleado_Id = model.Empleado_Id;
            ca.Apertura = model.Apertura;
            ca.Otros = model.Otros;
            ca.Cierre = model.Cierre;
            ca.FechaCierre = model.FechaCierre;
            ca.Actual = model.Actual;
            Usuarios emp = new();
            emp.Id = model.Empleado.Id;
            emp.Nombre = model.Empleado.Nombre;
            emp.Email = model.Empleado.Email;
            emp.Usuario = model.Empleado.Usuario;
            emp.Contraseña = model.Empleado.Contraseña;
            ca.Empleado = emp;
            return ca;
        }

        public List<Caja> GetAll()
        {
            orden = $"SELECT * FROM Caja ORDER BY Fecha DESC";
            List<Caja> cajas = new();

            SqlCommand sqlcmd = new(orden, conexion);
            try
            {
                AbrirConex();
                sqlcmd.CommandText = orden;
                SqlDataReader reader = sqlcmd.ExecuteReader();

                while (reader.Read())
                {
                    Caja ca = new();
                    ca.Id = Convert.ToInt32(reader["Id"].ToString());
                    ca.Fecha = Convert.ToDateTime(reader["Fecha"].ToString());
                    ca.Empleado_Id = Convert.ToInt32(reader["Empleado_Id"].ToString());
                    ca.Apertura = Convert.ToDecimal(reader["Apertura"].ToString());
                    ca.Actual = Convert.ToDecimal(reader["Actual"].ToString());
                    ca.Otros = Convert.ToDecimal(reader["Otros"].ToString());
                    ca.Cierre = Convert.ToDecimal(reader["Cierre"].ToString());
                    ca.FechaCierre = !string.IsNullOrEmpty(reader["FechaCierre"].ToString()) ? Convert.ToDateTime(reader["FechaCierre"].ToString()) : null;

                    Usuarios usuario = new();
                    UsuarioRepository usuarioRepository = new();
                    usuario = usuarioRepository.GetOne(Convert.ToInt32(reader["Empleado_Id"].ToString()));
                    ca.Empleado = usuario;

                    cajas.Add(ca);
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
            return cajas;
        }

        public Caja GetLast()
        {
            orden = @$"SELECT TOP 1 * FROM Caja
                        ORDER BY Fecha DESC";
            SqlCommand sqlcmd = new(orden, conexion);
            Caja caja = new();
            try
            {
                AbrirConex();
                sqlcmd.CommandText = orden;
                SqlDataReader reader = sqlcmd.ExecuteReader();

                while (reader.Read())
                {
                    caja.Id = Convert.ToInt32(reader["Id"].ToString());
                    caja.Fecha = Convert.ToDateTime(reader["Fecha"].ToString());
                    caja.Empleado_Id = Convert.ToInt32(reader["Empleado_Id"].ToString());
                    caja.Apertura = Convert.ToDecimal(reader["Apertura"].ToString());
                    caja.Actual = Convert.ToDecimal(reader["Actual"].ToString());
                    caja.Otros = Convert.ToDecimal(reader["Otros"].ToString());
                    caja.Cierre = Convert.ToDecimal(reader["Cierre"].ToString());
                    caja.FechaCierre = !string.IsNullOrEmpty(reader["FechaCierre"].ToString()) ? Convert.ToDateTime(reader["FechaCierre"].ToString()) : null;

                    Usuarios usuario = new();
                    UsuarioRepository usuarioRepository = new();
                    usuario = usuarioRepository.GetOne(Convert.ToInt32(reader["Empleado_Id"].ToString()));
                    caja.Empleado = usuario;
                }

                CerrarConex();
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
            return caja;
        }

        public Caja GetOne(int id)
        {
            orden = $"SELECT * FROM Caja WHERE Id ={id}";
            SqlCommand sqlcmd = new(orden, conexion);
            Caja caja = new();
            try
            {
                AbrirConex();
                sqlcmd.CommandText = orden;
                SqlDataReader reader = sqlcmd.ExecuteReader();

                while (reader.Read())
                {
                    caja.Id = Convert.ToInt32(reader["Id"].ToString());
                    caja.Fecha = Convert.ToDateTime(reader["Fecha"].ToString());
                    caja.Empleado_Id = Convert.ToInt32(reader["Empleado_Id"].ToString());
                    caja.Apertura = Convert.ToDecimal(reader["Apertura"].ToString());
                    caja.Actual = Convert.ToDecimal(reader["Actual"].ToString());
                    caja.Otros = Convert.ToDecimal(reader["Otros"].ToString());
                    caja.Cierre = Convert.ToDecimal(reader["Cierre"].ToString());
                    caja.FechaCierre = !string.IsNullOrEmpty(reader["FechaCierre"].ToString()) ? Convert.ToDateTime(reader["FechaCierre"].ToString()) : null;

                    Usuarios usuario = new();
                    UsuarioRepository usuarioRepository = new();
                    usuario = usuarioRepository.GetOne(Convert.ToInt32(reader["Empleado_Id"].ToString()));
                    caja.Empleado = usuario;
                }

                CerrarConex();
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
            return caja;
        }

        public Caja Post(CajaModel model)
        {
            if (model == null)
            {
                throw new Exception("Error al tratar de ejecutar la operación");
            }
            else
            {
                AbrirConex();

                SqlTransaction transaction;
                transaction = conexion.BeginTransaction();
                SqlCommand sqlcmd = new(orden, conexion, transaction);
                Caja caja = IniciarObjeto(model);

                try
                {
                    sqlcmd.Connection = conexion;
                    sqlcmd.Transaction = transaction;

                    orden = @"INSERT INTO Caja (Fecha, Empleado_Id, Apertura, Cierre, Actual, Otros)
                            VALUES (@Fecha, @Empleado_Id, @Apertura, @Cierre, @Actual, @Otros) ";

                    sqlcmd.CommandText = orden;
                    sqlcmd.Parameters.AddWithValue("@Fecha", DateTime.Now);
                    sqlcmd.Parameters.AddWithValue("@Empleado_Id", caja.Empleado_Id);
                    sqlcmd.Parameters.AddWithValue("@Apertura", caja.Apertura);
                    sqlcmd.Parameters.AddWithValue("@Actual", caja.Apertura); //El monto de apertura es el monto actual al momento de crear
                    sqlcmd.Parameters.AddWithValue("@Otros", 0);
                    sqlcmd.Parameters.AddWithValue("@Cierre", 0);

                    sqlcmd.ExecuteNonQuery();
                    sqlcmd.Parameters.Clear();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();

                    throw new Exception("Error al tratar de ejecutar la operación " + e.Message);
                }
                finally
                {
                    CerrarConex();
                    sqlcmd.Dispose();
                }

                return caja;
            }
        }

        public Caja Put(int id, CajaModel model)
        {
            Caja ca = GetOne(id);
            if (ca == null || model == null)
            {
                throw new Exception("Error al tratar de ejecutar la operación");
            }
            else
            {
                Caja caja = IniciarObjeto(model);
                SqlCommand sqlcmd = new(orden, conexion);
                try
                {
                    AbrirConex();
                    orden = $@"UPDATE Caja 
                               SET Cierre=@Cierre, FechaCierre=@FechaCierre WHERE Id=@Id";

                    sqlcmd.CommandText = orden;
                    sqlcmd.Parameters.AddWithValue("@Id", id);
                    sqlcmd.Parameters.AddWithValue("@Cierre", caja.Cierre);
                    sqlcmd.Parameters.AddWithValue("@FechaCierre", DateTime.Now);

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
                return caja;
            }
        }

        public Ingreso IngresosXfp()
        {
            orden = @"SELECT ISNULL((SELECT SUM(fv.Importe)
                    FROM Ventas v
                    INNER JOIN FormasPagoVentas fv ON fv.Venta_Id = v.Id
                    INNER JOIN FormasPago fpv ON fv.FormaPago_Id = fpv.Id and fpv.Descripcion LIKE 'EFECTIVO'
                    WHERE v.FechaBaja IS NULL AND DAY(v.Fecha) = DAY(GetDate())), 0 ) 
                    - ISNULL((SELECT SUM(fc.Importe)
                    FROM Compras c 
                    INNER JOIN FormasPagoCompras fc ON fc.Compra_Id = c.Id
                    INNER JOIN FormasPago fpc ON fc.FormaPago_Id = fpc.Id and fpc.Descripcion LIKE 'EFECTIVO'
                    WHERE c.FechaBaja IS NULL AND DAY(c.Fecha) = DAY(GetDate())), 0 ) 
                    AS EFT,

                    ISNULL((SELECT SUM(fv.Importe)
                    FROM Ventas v
                    INNER JOIN FormasPagoVentas fv ON fv.Venta_Id = v.Id
                    INNER JOIN FormasPago fpv ON fv.FormaPago_Id = fpv.Id and fpv.Descripcion LIKE '%DEBITO'
                    WHERE v.FechaBaja IS NULL AND DAY(v.Fecha) = DAY(GetDate())), 0 ) 
                    - ISNULL((SELECT SUM(fc.Importe)
                    FROM Compras c 
                    INNER JOIN FormasPagoCompras fc ON fc.Compra_Id = c.Id
                    INNER JOIN FormasPago fpc ON fc.FormaPago_Id = fpc.Id and fpc.Descripcion LIKE '%DEBITO'
                    WHERE c.FechaBaja IS NULL AND DAY(c.Fecha) = DAY(GetDate())), 0 ) 
                    AS TD,

                    ISNULL((SELECT SUM(fv.Importe)
                    FROM Ventas v
                    INNER JOIN FormasPagoVentas fv ON fv.Venta_Id = v.Id
                    INNER JOIN FormasPago fpv ON fv.FormaPago_Id = fpv.Id and fpv.Descripcion LIKE '%CREDITO'
                    WHERE v.FechaBaja IS NULL AND DAY(v.Fecha) = DAY(GetDate())), 0 ) 
                    - ISNULL((SELECT SUM(fc.Importe)
                    FROM Compras c 
                    INNER JOIN FormasPagoCompras fc ON fc.Compra_Id = c.Id
                    INNER JOIN FormasPago fpc ON fc.FormaPago_Id = fpc.Id and fpc.Descripcion LIKE '%CREDITO'
                    WHERE c.FechaBaja IS NULL AND DAY(c.Fecha) = DAY(GetDate())), 0 ) 
                    AS TC,

                    ISNULL((SELECT SUM(fv.Importe)
                    FROM Ventas v
                    INNER JOIN FormasPagoVentas fv ON fv.Venta_Id = v.Id
                    INNER JOIN FormasPago fpv ON fv.FormaPago_Id = fpv.Id and fpv.Descripcion LIKE 'TRANSFERENCIA%'
                    WHERE v.FechaBaja IS NULL AND DAY(v.Fecha) = DAY(GetDate())), 0 ) 
                    - ISNULL((SELECT SUM(fc.Importe)
                    FROM Compras c 
                    INNER JOIN FormasPagoCompras fc ON fc.Compra_Id = c.Id
                    INNER JOIN FormasPago fpc ON fc.FormaPago_Id = fpc.Id and fpc.Descripcion LIKE 'TRANSFERENCIA%'
                    WHERE c.FechaBaja IS NULL AND DAY(c.Fecha) = DAY(GetDate())), 0 ) 
                    AS TB,

                    ISNULL((SELECT SUM(fv.Importe)
                    FROM Ventas v
                    INNER JOIN FormasPagoVentas fv ON fv.Venta_Id = v.Id
                    INNER JOIN FormasPago fpv ON fv.FormaPago_Id = fpv.Id and fpv.Descripcion LIKE 'CHEQUE'
                    WHERE v.FechaBaja IS NULL AND DAY(v.Fecha) = DAY(GetDate())), 0 ) 
                    - ISNULL((SELECT SUM(fc.Importe)
                    FROM Compras c 
                    INNER JOIN FormasPagoCompras fc ON fc.Compra_Id = c.Id
                    INNER JOIN FormasPago fpc ON fc.FormaPago_Id = fpc.Id and fpc.Descripcion LIKE 'CHEQUE'
                    WHERE c.FechaBaja IS NULL AND DAY(c.Fecha) = DAY(GetDate())), 0 ) 
                    AS CH,

                    ISNULL((SELECT SUM(v.Saldo)
                    FROM Ventas v
                    WHERE v.FechaBaja IS NULL AND DAY(v.Fecha) = DAY(GetDate())), 0 ) 
                    AS CC";

            Ingreso ingreso = new();

            SqlCommand sqlcmd = new(orden, conexion);
            try
            {
                AbrirConex();
                sqlcmd.CommandText = orden;
                SqlDataReader reader = sqlcmd.ExecuteReader();

                while (reader.Read())
                {
                    ingreso.Efectivo = Convert.ToDecimal(reader["EFT"].ToString());
                    ingreso.TarjetaDebito = Convert.ToDecimal(reader["TD"].ToString());
                    ingreso.TarjetaCredito = Convert.ToDecimal(reader["TC"].ToString());
                    ingreso.Cheque = Convert.ToDecimal(reader["CH"].ToString());
                    ingreso.Transferencia = Convert.ToDecimal(reader["TB"].ToString());
                    ingreso.CuentaCorriente = Convert.ToDecimal(reader["CC"].ToString());
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
            return ingreso;
        }
    }
}