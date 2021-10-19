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
    public class CompraRepository : DBConex
    {
        string orden;
        private static Compras IniciarObjeto(CompraModel model)
        {
            UsuarioRepository usuarioRepository = new();
            ProveedorRepository proveedorRepository = new();

            Compras compra = new();
            compra.Empleado_Id = model.Empleado_Id;
            compra.Empleado = usuarioRepository.GetOne(model.Empleado_Id);
            compra.Fecha = model.Fecha;
            compra.Fecha_Baja = model.Fecha_Baja;
            compra.NroRecibo = model.NroRecibo;
            compra.Proveedor = proveedorRepository.GetOne(model.Proveedor_Id);
            compra.Proveedor_Id = model.Proveedor_Id;
            compra.Total = model.Total;

            foreach (var item in model.FormasPago)
            {
                FormaPagoCompra fpagoXcompras = new();
                fpagoXcompras.Id = item.Id;
                fpagoXcompras.Fecha = item.Fecha;
                fpagoXcompras.Importe = item.Importe;

                FormaPago fpago = new();
                fpago.Id = item.FormaPago.Id;
                fpago.Descripcion = item.FormaPago.Descripcion;

                fpagoXcompras.FormaPago = fpago;
                fpagoXcompras.FormaPago_Id = fpago.Id;
                compra.FormasPago.Add(fpagoXcompras);
            }

            foreach (var item in model.Detalle)
            {
                DetalleCompras detcompra = new();
                detcompra.Compra_Id = item.Compra_Id;
                detcompra.Articulo_Id = item.Articulo_Id;
                detcompra.Articulo.Id = item.Articulo.Id;
                detcompra.Articulo.Codigo_Art = item.Articulo.Codigo_Art;
                detcompra.Articulo.Nombre = item.Articulo.Nombre;
                detcompra.Articulo.Precio_Mayor = item.Articulo.Precio_Mayor;
                detcompra.Articulo.Precio_Unit = item.Articulo.Precio_Unit;
                detcompra.Articulo.Stock_Act = item.Articulo.Stock_Act;
                detcompra.Articulo.Ultima_Modif = item.Articulo.Ultima_Modif;
                detcompra.Cantidad = item.Cantidad;
                detcompra.Precio_Mayor = item.Precio_Mayor;
                detcompra.Precio_Unit = item.Precio_Unit;
                detcompra.SubTotal = item.SubTotal;

                compra.Detalle.Add(detcompra);
            }

            return compra;
        }

        public List<Compras> GetAll()
        {
            SqlCommand sqlcmd = new(orden, conexion);

            List<Compras> compras = new();

            try
            {
                orden = @"SELECT * FROM Compras ORDER BY Fecha DESC";

                AbrirConex();
                sqlcmd.CommandText = orden;
                SqlDataReader reader = sqlcmd.ExecuteReader();

                while (reader.Read())
                {
                    Compras compra = new();
                    compra.Id = Convert.ToInt32(reader["Id"].ToString());
                    compra.Fecha = Convert.ToDateTime(reader["Fecha"].ToString());
                    compra.NroRecibo = Convert.ToInt64(reader["NroRecibo"].ToString());
                    compra.Proveedor_Id = Convert.ToInt32(reader["Proveedor_Id"].ToString());
                    compra.Empleado_Id = Convert.ToInt32(reader["Empleado_Id"].ToString());
                    compra.Total = Convert.ToDecimal(reader["Total"].ToString());
                    compra.Fecha_Baja = string.IsNullOrEmpty(reader["FechaBaja"].ToString()) ? null : Convert.ToDateTime(reader["FechaBaja"]);

                    ProveedorRepository proveedorRepository = new();
                    Proveedores proveedor = proveedorRepository.GetOne(Convert.ToInt32(reader["Proveedor_Id"].ToString()));
                    compra.Proveedor = proveedor;
                    UsuarioRepository usuarioRepository = new();
                    Usuarios usuario = usuarioRepository.GetOne(Convert.ToInt32(reader["Empleado_Id"].ToString()));
                    compra.Empleado = usuario;

                    compras.Add(compra);
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
            return compras;
        }

        public Compras GetOne(int id)
        {
            orden = $"SELECT * FROM Compras WHERE Id ={id}";
            SqlCommand sqlcmd = new(orden, conexion);
            Compras compra = new();
            try
            {
                AbrirConex();
                sqlcmd.CommandText = orden;
                SqlDataReader reader = sqlcmd.ExecuteReader();

                while (reader.Read())
                {
                    compra.Id = Convert.ToInt32(reader["Id"].ToString());
                    compra.Fecha = Convert.ToDateTime(reader["Fecha"].ToString());
                    compra.NroRecibo = Convert.ToInt64(reader["NroRecibo"].ToString());
                    compra.Proveedor_Id = Convert.ToInt32(reader["Proveedor_Id"].ToString());
                    compra.Empleado_Id = Convert.ToInt32(reader["Empleado_Id"].ToString());
                    compra.Total = Convert.ToDecimal(reader["Total"].ToString());
                    compra.Fecha_Baja = string.IsNullOrEmpty(reader["FechaBaja"].ToString()) ? null : Convert.ToDateTime(reader["FechaBaja"]);

                    ProveedorRepository proveedorRepository = new();
                    Proveedores proveedor = proveedorRepository.GetOne(Convert.ToInt32(reader["Proveedor_Id"].ToString()));
                    compra.Proveedor = proveedor;
                    UsuarioRepository usuarioRepository = new();
                    Usuarios usuario = usuarioRepository.GetOne(Convert.ToInt32(reader["Empleado_Id"].ToString()));
                    compra.Empleado = usuario;
                    FormaPagoRepository fPagoRepository = new();
                    compra = fPagoRepository.GetAllFormasPagoXCompra(compra);
                }

                CerrarConex();
                compra = GetDetalle(compra);
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
            return compra;
        }

        public Compras GetDetalle(Compras compra)
        {
            SqlCommand sqlcmd = new(orden, conexion);

            try
            {
                orden = @"SELECT * FROM DetalleCompras  
                         WHERE Compra_Id = @Compra_Id";

                AbrirConex();
                sqlcmd.CommandText = orden;
                sqlcmd.Parameters.AddWithValue("@Compra_Id", compra.Id);
                SqlDataReader reader = sqlcmd.ExecuteReader();

                while (reader.Read())
                {
                    DetalleCompras detCompra = new();
                    detCompra.Id = Convert.ToInt32(reader["Id"].ToString());
                    detCompra.Articulo_Id = Convert.ToInt32(reader["Articulo_Id"].ToString());
                    detCompra.Cantidad = Convert.ToInt32(reader["Cantidad"].ToString());
                    detCompra.Precio_Mayor = Convert.ToDecimal(reader["Precio_Mayor"].ToString());
                    detCompra.Precio_Unit = Convert.ToDecimal(reader["Precio_Unit"].ToString());
                    detCompra.SubTotal = string.IsNullOrEmpty(reader["SubTotal"].ToString()) ? 0 : Convert.ToDecimal(reader["SubTotal"].ToString());
                    detCompra.Compra_Id = Convert.ToInt32(reader["Compra_Id"].ToString());

                    ArticuloRepository articuloRepository = new();
                    Articulos articulo = articuloRepository.GetOne(Convert.ToInt32(reader["Articulo_Id"].ToString()));
                    detCompra.Articulo = articulo;
                    compra.Detalle.Add(detCompra);
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
            return compra;
        }

        public Compras Post(CompraModel model)
        {
            if (model == null)
            {
                throw new Exception("Error al tratar de ejecutar la operación");
            }
            else
            {
                AbrirConex();
                Compras compra = IniciarObjeto(model);
                SqlTransaction transaction;
                transaction = conexion.BeginTransaction();
                SqlCommand sqlcmd = new(orden, conexion, transaction);

                try
                {
                    sqlcmd.Connection = conexion;
                    sqlcmd.Transaction = transaction;

                    orden = @"INSERT INTO Compras (NroRecibo, Fecha, Proveedor_Id, Empleado_Id, Total)
                            VALUES (@NroRecibo, @Fecha, @Proveedor_Id, @Empleado_Id, @Total) ";

                    sqlcmd.CommandText = orden;
                    sqlcmd.Parameters.AddWithValue("@NroRecibo", compra.NroRecibo);
                    sqlcmd.Parameters.AddWithValue("@Fecha", compra.Fecha);
                    sqlcmd.Parameters.AddWithValue("@Proveedor_Id", compra.Proveedor_Id);
                    sqlcmd.Parameters.AddWithValue("@Empleado_Id", compra.Empleado_Id);
                    sqlcmd.Parameters.AddWithValue("@Total", compra.Total);

                    sqlcmd.ExecuteNonQuery();
                    sqlcmd.Parameters.Clear();

                    foreach (var fpago in compra.FormasPago)
                    {
                        orden = @"INSERT INTO FormasPagoCompras (Fecha, Importe, FormaPago_Id, Compra_Id)
                            VALUES (@Fecha, @Importe, @FormaPago_Id, (SELECT IDENT_CURRENT ('Compras')))";

                        sqlcmd.CommandText = orden;
                        sqlcmd.Parameters.AddWithValue("@Fecha", fpago.Fecha);
                        sqlcmd.Parameters.AddWithValue("@Importe", fpago.Importe);
                        sqlcmd.Parameters.AddWithValue("@FormaPago_Id", fpago.FormaPago_Id);

                        sqlcmd.ExecuteNonQuery();
                        sqlcmd.Parameters.Clear();

                        if (fpago.FormaPago.Descripcion.Equals("Efectivo"))
                        {
                            orden = @"UPDATE Caja
                                      SET Actual = (SELECT TOP 1 Actual FROM Caja) - @Actual
                                      WHERE Id = (SELECT TOP 1 ID FROM Caja)";

                            sqlcmd.CommandText = orden;
                            sqlcmd.Parameters.AddWithValue("@Actual", fpago.Importe);

                            sqlcmd.ExecuteNonQuery();
                            sqlcmd.Parameters.Clear();
                        }

                    }

                    foreach (var detalle in compra.Detalle)
                    {
                        orden = @"INSERT INTO DetalleCompras (Articulo_Id, Precio_Mayor, Precio_Unit, Cantidad, SubTotal, Compra_Id)
                            VALUES (@Articulo_Id, @Precio_Mayor, @Precio_Unit, @Cantidad, @SubTotal, (SELECT IDENT_CURRENT ('Compras')))";

                        sqlcmd.CommandText = orden;
                        sqlcmd.Parameters.AddWithValue("@Articulo_Id", detalle.Articulo_Id);
                        sqlcmd.Parameters.AddWithValue("@Precio_Mayor", detalle.Precio_Mayor);
                        sqlcmd.Parameters.AddWithValue("@Precio_Unit", detalle.Precio_Unit);
                        sqlcmd.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
                        sqlcmd.Parameters.AddWithValue("@SubTotal", detalle.SubTotal);

                        sqlcmd.ExecuteNonQuery();
                        sqlcmd.Parameters.Clear();

                        orden = $"SELECT Stock_Act FROM Articulos WHERE Id = {detalle.Articulo_Id};";
                        sqlcmd.CommandText = orden;
                        sqlcmd.ExecuteNonQuery();

                        SqlDataReader reader = sqlcmd.ExecuteReader();

                        int stock = 0;
                        if (reader.Read())
                        {
                            stock = Convert.ToInt32(reader["Stock_Act"].ToString());
                            stock += detalle.Cantidad;
                            reader.Close();
                        }

                        orden = $@"UPDATE Articulos 
                                  SET Stock_Act = @Stock_Act, Precio_Mayor = @Precio_Mayor,
                                  Precio_Unit = @Precio_Unit, FechaBaja = NULL, Ultima_Modif = GETDATE() 
                                  WHERE Id = @Id";

                        sqlcmd.CommandText = orden;
                        sqlcmd.Parameters.AddWithValue("@Id", detalle.Articulo_Id);
                        sqlcmd.Parameters.AddWithValue("@Stock_Act", stock);
                        sqlcmd.Parameters.AddWithValue("@Precio_Mayor", detalle.Precio_Mayor);
                        sqlcmd.Parameters.AddWithValue("@Precio_Unit", detalle.Precio_Unit);

                        sqlcmd.ExecuteNonQuery();
                        sqlcmd.Parameters.Clear();
                    }

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

                return compra;
            }
        }

        public int Delete(int id)
        {
            Compras compra = GetOne(id);
            if (compra != null)
            {
                AbrirConex();

                SqlTransaction transaction;
                transaction = conexion.BeginTransaction();
                SqlCommand sqlcmd = new(orden, conexion, transaction);

                try
                {
                    orden = $"UPDATE Compras SET FechaBaja=@FechaBaja WHERE Id=@Id";

                    sqlcmd.CommandText = orden;
                    sqlcmd.Parameters.AddWithValue("@Id", id);
                    sqlcmd.Parameters.AddWithValue("@FechaBaja", DateTime.Now);

                    sqlcmd.ExecuteNonQuery();
                    sqlcmd.Parameters.Clear();

                    foreach (var item in compra.Detalle)
                    {
                        orden = $"SELECT Stock_Act FROM Articulos WHERE Id = {item.Articulo_Id}";
                        sqlcmd.CommandText = orden;
                        sqlcmd.ExecuteNonQuery();

                        SqlDataReader reader = sqlcmd.ExecuteReader();

                        int stock = 0;
                        if (reader.Read())
                        {
                            stock = Convert.ToInt32(reader["Stock_Act"].ToString());
                            stock -= item.Cantidad;
                            reader.Close();
                        }

                        orden = $@"UPDATE Articulos 
                                  SET Stock_Act = @Stock_Act 
                                  WHERE Id = @Id";

                        sqlcmd.CommandText = orden;
                        sqlcmd.Parameters.AddWithValue("@Id", item.Articulo_Id);
                        sqlcmd.Parameters.AddWithValue("@Stock_Act", stock);

                        sqlcmd.ExecuteNonQuery();
                        sqlcmd.Parameters.Clear();

                        foreach (var fpago in compra.FormasPago)
                        {
                            if (fpago.FormaPago.Descripcion.Equals("Efectivo"))
                            {
                                orden = @"UPDATE Caja
                                      SET Actual = (SELECT TOP 1 Actual FROM Caja) + @Actual
                                      WHERE Id = (SELECT TOP 1 ID FROM Caja)";

                                sqlcmd.CommandText = orden;
                                sqlcmd.Parameters.AddWithValue("@Actual", fpago.Importe);

                                sqlcmd.ExecuteNonQuery();
                                sqlcmd.Parameters.Clear();
                            }
                        }
                    }

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
            }
            else
            {
                id = 0;
            }
            return id;
        }
    }
}