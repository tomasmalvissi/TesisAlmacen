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
            ca.Actual = model.Actual;
            ca.Otros = model.Otros;
            ca.CtaCorriente = model.CtaCorriente;
            ca.Cierre = model.Cierre;
            ca.FechaCierre = model.FechaCierre;
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
                    ca.CtaCorriente = Convert.ToDecimal(reader["CtaCorriente"].ToString());
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

        public Caja GetLast(int id)
        {
            orden = @$"SELECT TOP 1 * FROM Caja
                        WHERE Empleado_Id = @Id
                        ORDER BY Fecha DESC";

            SqlCommand sqlcmd = new(orden, conexion);
            Caja caja = new();
            try
            {
                AbrirConex();
                sqlcmd.CommandText = orden;
                sqlcmd.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = sqlcmd.ExecuteReader();

                while (reader.Read())
                {
                    caja.Id = Convert.ToInt32(reader["Id"].ToString());
                    caja.Fecha = Convert.ToDateTime(reader["Fecha"].ToString());
                    caja.Empleado_Id = Convert.ToInt32(reader["Empleado_Id"].ToString());
                    caja.Apertura = Convert.ToDecimal(reader["Apertura"].ToString());
                    caja.Actual = Convert.ToDecimal(reader["Actual"].ToString());
                    caja.Otros = Convert.ToDecimal(reader["Otros"].ToString());
                    caja.CtaCorriente = Convert.ToDecimal(reader["CtaCorriente"].ToString());
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
                    caja.CtaCorriente = Convert.ToDecimal(reader["CtaCorriente"].ToString());
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
                caja.Fecha = DateTime.Now;
                caja.Actual = caja.Apertura; //El monto de apertura es el monto actual al momento de crear
                caja.Otros = 0;
                caja.CtaCorriente = 0;
                caja.Cierre = 0;

                try
                {
                    sqlcmd.Connection = conexion;
                    sqlcmd.Transaction = transaction;

                    orden = @"INSERT INTO Caja (Fecha, Empleado_Id, Apertura, Actual, CtaCorriente, Otros, Cierre)
                            VALUES (@Fecha, @Empleado_Id, @Apertura, @Actual, @CtaCorriente, @Otros, @Cierre) ";

                    sqlcmd.CommandText = orden;
                    sqlcmd.Parameters.AddWithValue("@Fecha", caja.Fecha);
                    sqlcmd.Parameters.AddWithValue("@Empleado_Id", caja.Empleado_Id);
                    sqlcmd.Parameters.AddWithValue("@Apertura", caja.Apertura);
                    sqlcmd.Parameters.AddWithValue("@Actual", caja.Actual);
                    sqlcmd.Parameters.AddWithValue("@Otros", caja.Otros);
                    sqlcmd.Parameters.AddWithValue("@CtaCorriente", caja.CtaCorriente);
                    sqlcmd.Parameters.AddWithValue("@Cierre", caja.Cierre);

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
                    sqlcmd.Parameters.AddWithValue("@Cierre", caja.Actual);
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
                    INNER JOIN (SELECT TOP 1 Empleado_Id FROM Caja) cj ON cj.Empleado_Id = v.Empleado_Id
                    WHERE v.FechaBaja IS NULL AND DAY(v.Fecha) = DAY(GetDate())), 0 ) 
                    - ISNULL((SELECT SUM(fc.Importe)
                    FROM Compras c 
                    INNER JOIN FormasPagoCompras fc ON fc.Compra_Id = c.Id
                    INNER JOIN FormasPago fpc ON fc.FormaPago_Id = fpc.Id and fpc.Descripcion LIKE 'EFECTIVO'
                    INNER JOIN (SELECT TOP 1 Empleado_Id FROM Caja) cj ON cj.Empleado_Id = c.Empleado_Id
                    WHERE c.FechaBaja IS NULL AND DAY(c.Fecha) = DAY(GetDate())), 0 ) 
                    AS EFT,

                    ISNULL((SELECT SUM(fv.Importe)
                    FROM Ventas v
                    INNER JOIN FormasPagoVentas fv ON fv.Venta_Id = v.Id
                    INNER JOIN FormasPago fpv ON fv.FormaPago_Id = fpv.Id and fpv.Descripcion LIKE '%DEBITO'
					INNER JOIN (SELECT TOP 1 Empleado_Id FROM Caja) cj ON cj.Empleado_Id = v.Empleado_Id
                    WHERE v.FechaBaja IS NULL AND DAY(v.Fecha) = DAY(GetDate())), 0 ) 
                    - ISNULL((SELECT SUM(fc.Importe)
                    FROM Compras c 
                    INNER JOIN FormasPagoCompras fc ON fc.Compra_Id = c.Id
                    INNER JOIN FormasPago fpc ON fc.FormaPago_Id = fpc.Id and fpc.Descripcion LIKE '%DEBITO'
					INNER JOIN (SELECT TOP 1 Empleado_Id FROM Caja) cj ON cj.Empleado_Id = c.Empleado_Id
                    WHERE c.FechaBaja IS NULL AND DAY(c.Fecha) = DAY(GetDate())), 0 ) 
                    AS TD,

                    ISNULL((SELECT SUM(fv.Importe)
                    FROM Ventas v
                    INNER JOIN FormasPagoVentas fv ON fv.Venta_Id = v.Id
                    INNER JOIN FormasPago fpv ON fv.FormaPago_Id = fpv.Id and fpv.Descripcion LIKE '%CREDITO'
					INNER JOIN (SELECT TOP 1 Empleado_Id FROM Caja) cj ON cj.Empleado_Id = v.Empleado_Id
                    WHERE v.FechaBaja IS NULL AND DAY(v.Fecha) = DAY(GetDate())), 0 ) 
                    - ISNULL((SELECT SUM(fc.Importe)
                    FROM Compras c 
                    INNER JOIN FormasPagoCompras fc ON fc.Compra_Id = c.Id
                    INNER JOIN FormasPago fpc ON fc.FormaPago_Id = fpc.Id and fpc.Descripcion LIKE '%CREDITO'
					INNER JOIN (SELECT TOP 1 Empleado_Id FROM Caja) cj ON cj.Empleado_Id = c.Empleado_Id
                    WHERE c.FechaBaja IS NULL AND DAY(c.Fecha) = DAY(GetDate())), 0 ) 
                    AS TC,

                    ISNULL((SELECT SUM(fv.Importe)
                    FROM Ventas v
                    INNER JOIN FormasPagoVentas fv ON fv.Venta_Id = v.Id
                    INNER JOIN FormasPago fpv ON fv.FormaPago_Id = fpv.Id and fpv.Descripcion LIKE 'TRANSFERENCIA%'
					INNER JOIN (SELECT TOP 1 Empleado_Id FROM Caja) cj ON cj.Empleado_Id = v.Empleado_Id
                    WHERE v.FechaBaja IS NULL AND DAY(v.Fecha) = DAY(GetDate())), 0 ) 
                    - ISNULL((SELECT SUM(fc.Importe)
                    FROM Compras c 
                    INNER JOIN FormasPagoCompras fc ON fc.Compra_Id = c.Id
                    INNER JOIN FormasPago fpc ON fc.FormaPago_Id = fpc.Id and fpc.Descripcion LIKE 'TRANSFERENCIA%'
					INNER JOIN (SELECT TOP 1 Empleado_Id FROM Caja) cj ON cj.Empleado_Id = c.Empleado_Id
                    WHERE c.FechaBaja IS NULL AND DAY(c.Fecha) = DAY(GetDate())), 0 ) 
                    AS TB,

                    ISNULL((SELECT SUM(fv.Importe)
                    FROM Ventas v
                    INNER JOIN FormasPagoVentas fv ON fv.Venta_Id = v.Id
                    INNER JOIN FormasPago fpv ON fv.FormaPago_Id = fpv.Id and fpv.Descripcion LIKE 'CHEQUE'
					INNER JOIN (SELECT TOP 1 Empleado_Id FROM Caja) cj ON cj.Empleado_Id = v.Empleado_Id
                    WHERE v.FechaBaja IS NULL AND DAY(v.Fecha) = DAY(GetDate())), 0 ) 
                    - ISNULL((SELECT SUM(fc.Importe)
                    FROM Compras c 
                    INNER JOIN FormasPagoCompras fc ON fc.Compra_Id = c.Id
                    INNER JOIN FormasPago fpc ON fc.FormaPago_Id = fpc.Id and fpc.Descripcion LIKE 'CHEQUE'
					INNER JOIN (SELECT TOP 1 Empleado_Id FROM Caja) cj ON cj.Empleado_Id = c.Empleado_Id
                    WHERE c.FechaBaja IS NULL AND DAY(c.Fecha) = DAY(GetDate())), 0 ) 
                    AS CH";

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