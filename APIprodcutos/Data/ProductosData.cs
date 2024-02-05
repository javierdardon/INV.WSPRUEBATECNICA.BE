using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using APIprodcutos.Data;
using APIprodcutos.Models;

namespace APIproductos.Data
{
    public class ProductoData
    {
        public static List<Productos> Listar()
        {
            // Inicialización de la lista de productos.
            List<Productos> lista = new List<Productos>();
            // Consulta SQL que realiza un INNER JOIN entre las tablas Producto, Marca, Presentacion, Proveedor y Zona.
            string query = @"
                SELECT p.id_producto, p.codigo, p.descripcion_producto, p.precio, p.stock, p.iva, p.peso,
                       m.id_marca, m.descripcion AS marca_descripcion, 
                       pr.id_presentacion, pr.descripcion AS presentacion_descripcion, 
                       pv.id_proveedor, pv.descripcion AS proveedor_descripcion, 
                       z.id_zona, z.descripcion AS zona_descripcion
                FROM Producto p
                INNER JOIN Marca m ON p.id_marca = m.id_marca
                INNER JOIN Presentacion pr ON p.id_presentacion = pr.id_presentacion
                INNER JOIN Proveedor pv ON p.id_proveedor = pv.id_proveedor
                INNER JOIN Zona z ON p.id_zona = z.id_zona";

            try
            {
                // Establecimiento de la conexión con la base de datos.
                using (SqlConnection con = new SqlConnection(ConexionDB.cn))
                {
                    // Creación del comando con la consulta SQL.
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Apertura de la conexión.
                        con.Open();
                        // Ejecución del comando y obtención de los resultados.
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            // Lectura de los resultados fila por fila.
                            while (dr.Read())
                            {
                                // Añadir cada producto a la lista con sus respectivos detalles.
                                lista.Add(new Productos()
                                {
                                    IdProducto = Convert.ToInt32(dr["id_producto"]),
                                    Codigo = Convert.ToInt32(dr["codigo"]),
                                    DescripcionProducto = dr["descripcion_producto"].ToString(),
                                    Precio = Convert.ToDecimal(dr["precio"]),
                                    Stock = Convert.ToInt32(dr["stock"]),
                                    Iva = Convert.ToInt32(dr["iva"]),
                                    Peso = Convert.ToDecimal(dr["peso"]),
                                    IdMarca = Convert.ToInt32(dr["id_marca"]),
                                    MarcaDescripcion = dr["marca_descripcion"].ToString(),
                                    IdPresentacion = Convert.ToInt32(dr["id_presentacion"]),
                                    PresentacionDescripcion = dr["presentacion_descripcion"].ToString(),
                                    IdProveedor = Convert.ToInt32(dr["id_proveedor"]),
                                    ProveedorDescripcion = dr["proveedor_descripcion"].ToString(),
                                    IdZona = Convert.ToInt32(dr["id_zona"]),
                                    ZonaDescripcion = dr["zona_descripcion"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Loguear y manejar las excepciones específicas de SQL.
                
                throw new ApplicationException("Error al obtener la lista de productos: " + sqlEx.Message, sqlEx);
            }
            catch (Exception ex)
            {
                // Loguear y manejar excepciones genéricas no relacionadas con SQL.
                // Estas son excepciones inesperadas que pueden requerir una investigación más profunda.
                throw new ApplicationException("Error inesperado al obtener la lista de productos: " + ex.Message, ex);
            }
            // Retornar la lista completa de productos.
            return lista;
        }



        public static bool Insertar(Productos producto)
        {
            // Definición de la consulta SQL para insertar un nuevo producto.
            string query = @"
                INSERT INTO Producto 
                (id_marca, id_presentacion, id_proveedor, id_zona, codigo, descripcion_producto, precio, stock, iva, peso) 
                VALUES 
                (@idMarca, @idPresentacion, @idProveedor, @idZona, @codigo, @descripcionProducto, @precio, @stock, @iva, @peso)";

            try
            {
                // Utilizando la conexión a la base de datos.
                using (SqlConnection con = new SqlConnection(ConexionDB.cn))
                {
                    // Creación del comando SQL con la consulta y la conexión.
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Asignación de los parámetros del comando con los valores del producto a insertar.
                        cmd.Parameters.AddWithValue("@idMarca", producto.IdMarca);
                        cmd.Parameters.AddWithValue("@idPresentacion", producto.IdPresentacion);
                        cmd.Parameters.AddWithValue("@idProveedor", producto.IdProveedor);
                        cmd.Parameters.AddWithValue("@idZona", producto.IdZona);
                        cmd.Parameters.AddWithValue("@codigo", producto.Codigo);
                        cmd.Parameters.AddWithValue("@descripcionProducto", producto.DescripcionProducto);
                        cmd.Parameters.AddWithValue("@precio", producto.Precio);
                        cmd.Parameters.AddWithValue("@stock", producto.Stock);
                        cmd.Parameters.AddWithValue("@iva", producto.Iva);
                        cmd.Parameters.AddWithValue("@peso", producto.Peso);

                        // Apertura de la conexión a la base de datos.
                        con.Open();

                        // Ejecución del comando y retorno del resultado de la operación.
                        // Si se afecta al menos una fila, el método devuelve verdadero.
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Manejo de excepciones específicas de la base de datos SQL Server.
                // Aquí deberías agregar el manejo de errores, como registrarlos en un archivo de log.
                throw new ApplicationException("Error al insertar el producto: " + sqlEx.Message, sqlEx);
            }
            catch (Exception ex)
            {
                // Manejo de excepciones genéricas.
                // Es útil para capturar errores no relacionados con la base de datos, como errores de red, etc.
                throw new ApplicationException("Error inesperado al insertar el producto: " + ex.Message, ex);
            }
        }


        public static bool Modificar(Productos producto)
        {
            // Definición de la consulta SQL para actualizar un producto existente.
            string query = @"
                UPDATE Producto SET 
                id_marca = @idMarca, 
                id_presentacion = @idPresentacion, 
                id_proveedor = @idProveedor, 
                id_zona = @idZona, 
                codigo = @codigo, 
                descripcion_producto = @descripcionProducto, 
                precio = @precio, 
                stock = @stock, 
                iva = @iva, 
                peso = @peso
                WHERE id_producto = @idProducto";

            try
            {
                // Utilizando la conexión a la base de datos.
                using (SqlConnection con = new SqlConnection(ConexionDB.cn))
                {
                    // Creación del comando SQL con la consulta y la conexión.
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Asignación de los parámetros del comando con los valores del producto a actualizar.
                        cmd.Parameters.AddWithValue("@idMarca", producto.IdMarca);
                        cmd.Parameters.AddWithValue("@idPresentacion", producto.IdPresentacion);
                        cmd.Parameters.AddWithValue("@idProveedor", producto.IdProveedor);
                        cmd.Parameters.AddWithValue("@idZona", producto.IdZona);
                        cmd.Parameters.AddWithValue("@codigo", producto.Codigo);
                        cmd.Parameters.AddWithValue("@descripcionProducto", producto.DescripcionProducto);
                        cmd.Parameters.AddWithValue("@precio", producto.Precio);
                        cmd.Parameters.AddWithValue("@stock", producto.Stock);
                        cmd.Parameters.AddWithValue("@iva", producto.Iva);
                        cmd.Parameters.AddWithValue("@peso", producto.Peso);
                        cmd.Parameters.AddWithValue("@idProducto", producto.IdProducto);

                        // Apertura de la conexión a la base de datos.
                        con.Open();

                        // Ejecución del comando y retorno del resultado de la operación.
                        // Si se afecta al menos una fila, el método devuelve verdadero.
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Manejo de excepciones específicas de la base de datos SQL Server.
                // Aquí deberías agregar el manejo de errores, como registrarlos en un archivo de log.
                throw new ApplicationException("Error al modificar el producto: " + sqlEx.Message, sqlEx);
            }
            catch (Exception ex)
            {
                // Manejo de excepciones genéricas.
                // Es útil para capturar errores no relacionados con la base de datos, como errores de red, etc.
                throw new ApplicationException("Error inesperado al modificar el producto: " + ex.Message, ex);
            }
        }


        public static bool Eliminar(int idProducto)
        {
            // Definición de la consulta SQL para eliminar un producto existente.
            string query = "DELETE FROM Producto WHERE id_producto = @idProducto";

            try
            {
                // Utilizando la conexión a la base de datos.
                using (SqlConnection con = new SqlConnection(ConexionDB.cn))
                {
                    // Creación del comando SQL con la consulta y la conexión.
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        // Asignación del parámetro del comando con el valor del ID del producto a eliminar.
                        cmd.Parameters.AddWithValue("@idProducto", idProducto);

                        // Apertura de la conexión a la base de datos.
                        con.Open();

                        // Ejecución del comando y retorno del resultado de la operación.
                        // Si se afecta al menos una fila, el método devuelve verdadero.
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Manejo de excepciones específicas de la base de datos SQL Server.
                throw new ApplicationException("Error al eliminar el producto: " + sqlEx.Message, sqlEx);
            }
            catch (Exception ex)
            {
                // Manejo de excepciones genéricas.
                throw new ApplicationException("Error inesperado al eliminar el producto: " + ex.Message, ex);
            }
        }
    }
}
