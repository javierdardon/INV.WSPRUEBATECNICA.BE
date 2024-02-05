using APIprodcutos.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace APIprodcutos.Data
{
    public class MarcaData
    {
        // Método para insertar una nueva marca en la base de datos.
        public static bool Insertar(Marcas marca)
        {
            // Query SQL para insertar una nueva marca.
            string query = "INSERT INTO Marca (descripcion) VALUES (@descripcion)";
            // Se establece conexión con la base de datos.
            using (SqlConnection con = new SqlConnection(ConexionDB.cn))
            {
                // Se crea el comando SQL con la consulta y la conexión.
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Se asigna el valor del parámetro @descripcion.
                    cmd.Parameters.AddWithValue("@descripcion", marca.Descripcion);
                    try
                    {
                        // Se abre la conexión y se ejecuta el comando.
                        con.Open();
                        cmd.ExecuteNonQuery();
                        // Si no hay excepciones, retorna verdadero indicando éxito.
                        return true;
                    }
                    catch (Exception ex)
                    {
                        // En caso de excepción, se captura pero el método retorna falso.
                        return false;
                    }
                }
            }
        }

        // Método para listar todas las marcas existentes en la base de datos.
        public static List<Marcas> Listar()
        {
            // Lista para almacenar los objetos Marcas.
            List<Marcas> lista = new List<Marcas>();
            // Query SQL para seleccionar todas las marcas.
            string query = "SELECT id_marca, descripcion FROM Marca";
            using (SqlConnection con = new SqlConnection(ConexionDB.cn))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    try
                    {
                        // Se abre la conexión y se ejecuta el comando.
                        con.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            // Mientras haya filas para leer, se crea y añade una nueva Marca a la lista.
                            while (dr.Read())
                            {
                                Marcas marca = new Marcas()
                                {
                                    IdMarca = Convert.ToInt32(dr["id_marca"]),
                                    Descripcion = dr["descripcion"].ToString()
                                };
                                lista.Add(marca);
                            }
                        }
                        // Retorna la lista de marcas.
                        return lista;
                    }
                    catch (Exception ex)
                    {
                        // En caso de excepción, retorna la lista (posiblemente vacía).
                        return lista;
                    }
                }
            }
        }

        // Método para modificar una marca existente en la base de datos.
        public static bool Modificar(Marcas marca)
        {
            // Query SQL para actualizar la descripción de una marca específica.
            string query = "UPDATE Marca SET descripcion = @descripcion WHERE id_marca = @idMarca";
            using (SqlConnection con = new SqlConnection(ConexionDB.cn))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Se asignan los valores de los parámetros.
                    cmd.Parameters.AddWithValue("@descripcion", marca.Descripcion);
                    cmd.Parameters.AddWithValue("@idMarca", marca.IdMarca);
                    try
                    {
                        // Se abre la conexión y se ejecuta el comando.
                        con.Open();
                        cmd.ExecuteNonQuery();
                        // Si no hay excepciones, retorna verdadero indicando éxito.
                        return true;
                    }
                    catch (Exception ex)
                    {
                        // En caso de excepción, se captura pero el método retorna falso.
                        return false;
                    }
                }
            }
        }

        // Método para eliminar una marca específica de la base de datos.
        public static bool Eliminar(int idMarca)
        {
            // Query SQL para eliminar una marca por su ID.
            string query = "DELETE FROM Marca WHERE id_marca = @idMarca";
            using (SqlConnection con = new SqlConnection(ConexionDB.cn))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Se asigna el valor del parámetro @idMarca.
                    cmd.Parameters.AddWithValue("@idMarca", idMarca);
                    try
                    {
                        // Se abre la conexión y se ejecuta el comando.
                        con.Open();
                        cmd.ExecuteNonQuery();
                        // Si no hay excepciones, retorna verdadero indicando éxito.
                        return true;
                    }
                    catch (Exception ex)
                    {
                        // En caso de excepción, se captura pero el método retorna falso.
                        return false;
                    }
                }
            }
        }
    }
}
