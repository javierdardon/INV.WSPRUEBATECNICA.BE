using APIprodcutos.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace APIprodcutos.Data
{
    public class ReportesData
    {

        public static List<Productos> ListarPorProveedor(int idProveedor)
        {
            List<Productos> lista = new List<Productos>();
            string query = @"SELECT p.id_producto, m.descripcion as MarcaDescripcion, pr.descripcion as PresentacionDescripcion, 
                     prov.descripcion as ProveedorDescripcion, z.descripcion as ZonaDescripcion, p.codigo, 
                     p.descripcion_producto, p.precio, p.stock, p.iva, p.peso 
                     FROM Producto p
                     INNER JOIN Marca m ON p.id_marca = m.id_marca
                     INNER JOIN Presentacion pr ON p.id_presentacion = pr.id_presentacion
                     INNER JOIN Proveedor prov ON p.id_proveedor = prov.id_proveedor
                     INNER JOIN Zona z ON p.id_zona = z.id_zona
                     WHERE p.id_proveedor = @idProveedor";
            try
            {
                using (SqlConnection con = new SqlConnection(ConexionDB.cn))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@idProveedor", idProveedor);
                        con.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                lista.Add(new Productos()
                                {
                                    IdProducto = Convert.ToInt32(dr["id_producto"]),
                                    MarcaDescripcion = dr["MarcaDescripcion"].ToString(),
                                    PresentacionDescripcion = dr["PresentacionDescripcion"].ToString(),
                                    ProveedorDescripcion = dr["ProveedorDescripcion"].ToString(),
                                    ZonaDescripcion = dr["ZonaDescripcion"].ToString(),
                                    Codigo = Convert.ToInt32(dr["codigo"]),
                                    DescripcionProducto = dr["descripcion_producto"].ToString(),
                                    Precio = Convert.ToDecimal(dr["precio"]),
                                    Stock = Convert.ToInt32(dr["stock"]),
                                    Iva = Convert.ToInt32(dr["iva"]),
                                    Peso = Convert.ToDecimal(dr["peso"])
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Manejo de excepciones específicas de SQL Server.
                throw new ApplicationException("Error al listar productos por proveedor: " + sqlEx.Message, sqlEx);
            }
            catch (Exception ex)
            {
                // Manejo de excepciones generales.
                throw new ApplicationException("Error inesperado al listar productos por proveedor: " + ex.Message, ex);
            }
            return lista;
        }



        public static List<MarcaZona> ObtenerTopMarcasPorZona()
        {
            // Lista para almacenar el resultado de la consulta.
            List<MarcaZona> lista = new List<MarcaZona>();

            // Consulta SQL para obtener el top 10 de marcas por zona.
            string query = @"SELECT TOP 10 m.id_marca, m.descripcion, COUNT(*) as Cantidad, z.descripcion as Zona
                     FROM producto p
                     INNER JOIN marca m ON p.id_marca = m.id_marca
                     INNER JOIN zona z ON p.id_zona = z.id_zona
                     GROUP BY m.id_marca, m.descripcion, z.descripcion
                     ORDER BY Cantidad DESC";
            try
            {
                // Se establece la conexión con la base de datos.
                using (SqlConnection con = new SqlConnection(ConexionDB.cn))
                {
                    // Se crea el comando SQL a partir de la consulta y la conexión.
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Se abre la conexión a la base de datos.
                        con.Open();

                        // Se ejecuta la consulta y se procesa el resultado.
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            // Se lee cada fila del resultado.
                            while (dr.Read())
                            {
                                // Se añade cada marca con su cantidad y zona a la lista.
                                lista.Add(new MarcaZona()
                                {
                                    IdMarca = Convert.ToInt32(dr["id_marca"]),
                                    Descripcion = dr["descripcion"].ToString(),
                                    Cantidad = Convert.ToInt32(dr["Cantidad"]),
                                    Zona = dr["Zona"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Manejo de excepciones de SQL Server.
                throw new ApplicationException("Error al obtener el top de marcas por zona: " + sqlEx.Message, sqlEx);
            }
            catch (Exception ex)
            {
                // Manejo de excepciones generales.
                throw new ApplicationException("Error inesperado al obtener el top de marcas por zona: " + ex.Message, ex);
            }
            // Se devuelve la lista de marcas con su cantidad y zona.
            return lista;
        }
    }
}