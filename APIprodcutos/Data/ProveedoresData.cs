using APIprodcutos.Data;
using APIprodcutos.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace APIproductos.Data
{
    // Esta clase maneja las operaciones de base de datos para los proveedores.
    public class ProveedoresData
    {
        // Lista todos los proveedores de la base de datos.
        public static List<proveedores> Listar()
        {
            List<proveedores> lista = new List<proveedores>();
            string query = "SELECT id_proveedor, descripcion FROM proveedor";
            try
            {
                using (SqlConnection con = new SqlConnection(ConexionDB.cn))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                lista.Add(new proveedores()
                                {
                                    IdProveedor = Convert.ToInt32(dr["id_proveedor"]),
                                    Descripcion = dr["descripcion"].ToString()
                                });
                            }
                        }
                    }
                }
                return lista;
            }
            catch (Exception ex)
            {
                // Maneja excepciones y puede ser logueado o manejado según sea necesario.
                Console.WriteLine(ex.Message);
                return lista; // Puede optar por retornar una lista vacía o null dependiendo de la lógica de negocio.
            }
        }

        // Inserta un nuevo proveedor en la base de datos.
        public static bool Insertar(proveedores proveedor)
        {
            string query = "INSERT INTO proveedor (descripcion) VALUES (@descripcion)";
            try
            {
                using (SqlConnection con = new SqlConnection(ConexionDB.cn))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@descripcion", proveedor.Descripcion);
                        con.Open();
                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        // Modifica los datos de un proveedor existente.
        public static bool Modificar(proveedores proveedor)
        {
            string query = "UPDATE proveedor SET descripcion = @descripcion WHERE id_proveedor = @idProveedor";
            try
            {
                using (SqlConnection con = new SqlConnection(ConexionDB.cn))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@descripcion", proveedor.Descripcion);
                        cmd.Parameters.AddWithValue("@idProveedor", proveedor.IdProveedor);
                        con.Open();
                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        // Elimina un proveedor de la base de datos basándose en su ID.
        public static bool Eliminar(int idProveedor)
        {
            string query = "DELETE FROM proveedor WHERE id_proveedor = @idProveedor";
            try
            {
                using (SqlConnection con = new SqlConnection(ConexionDB.cn))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@idProveedor", idProveedor);
                        con.Open();
                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
