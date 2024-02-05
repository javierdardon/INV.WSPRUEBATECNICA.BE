using APIprodcutos.Data;
using APIprodcutos.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace APIproductos.Data
{
    // Clase para manejar las operaciones de base de datos para la entidad Presentacion.
    public class PresentacionData
    {
        // Método para insertar una nueva presentación en la base de datos.
        public static bool Insertar(Presentacion presentacion)
        {
            // Consulta SQL para insertar una nueva presentación.
            string query = "INSERT INTO presentacion (descripcion) VALUES (@descripcion)";
            // Establece conexión con la base de datos usando la cadena de conexión.
            using (SqlConnection con = new SqlConnection(ConexionDB.cn))
            {
                // Crea el comando SQL con la consulta y la conexión.
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Agrega el parámetro @descripcion con el valor de la descripción de la presentación.
                    cmd.Parameters.AddWithValue("@descripcion", presentacion.Descripcion);
                    try
                    {
                        // Abre la conexión a la base de datos y ejecuta el comando.
                        con.Open();
                        cmd.ExecuteNonQuery();
                        // Retorna verdadero si la inserción fue exitosa.
                        return true;
                    }
                    catch (Exception ex)
                    {
                        // En caso de excepción, retorna falso.
                        return false;
                    }
                }
            }
        }

        // Método para listar todas las presentaciones de la base de datos.
        public static List<Presentacion> Listar()
        {
            // Lista para almacenar las presentaciones obtenidas.
            List<Presentacion> lista = new List<Presentacion>();
            // Consulta SQL para seleccionar todas las presentaciones.
            string query = "SELECT id_presentacion, descripcion FROM presentacion";
            using (SqlConnection con = new SqlConnection(ConexionDB.cn))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    try
                    {
                        // Abre la conexión y ejecuta la consulta.
                        con.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            // Lee cada fila del resultado y crea un objeto Presentacion con los datos.
                            while (dr.Read())
                            {
                                Presentacion presentacion = new Presentacion()
                                {
                                    IdPresentacion = Convert.ToInt32(dr["id_presentacion"]),
                                    Descripcion = dr["descripcion"].ToString()
                                };
                                // Añade el objeto Presentacion a la lista.
                                lista.Add(presentacion);
                            }
                        }
                        // Retorna la lista de presentaciones.
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

        // Método para modificar una presentación existente.
        public static bool Modificar(Presentacion presentacion)
        {
            // Consulta SQL para actualizar la descripción de una presentación basada en su ID.
            string query = "UPDATE presentacion SET descripcion = @descripcion WHERE id_presentacion = @idPresentacion";
            using (SqlConnection con = new SqlConnection(ConexionDB.cn))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Establece los valores de los parámetros en el comando SQL.
                    cmd.Parameters.AddWithValue("@descripcion", presentacion.Descripcion);
                    cmd.Parameters.AddWithValue("@idPresentacion", presentacion.IdPresentacion);
                    try
                    {
                        // Ejecuta el comando tras abrir la conexión.
                        con.Open();
                        cmd.ExecuteNonQuery();
                        // Retorna verdadero si la modificación fue exitosa.
                        return true;
                    }
                    catch (Exception ex)
                    {
                        // En caso de excepción, retorna falso.
                        return false;
                    }
                }
            }
        }

        // Método para eliminar una presentación basada en su ID.
        public static bool Eliminar(int idPresentacion)
        {
            // Consulta SQL para eliminar una presentación por su ID.
            string query = "DELETE FROM presentacion WHERE id_presentacion = @idPresentacion";
            using (SqlConnection con = new SqlConnection(ConexionDB.cn))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    // Establece el valor del parámetro @idPresentacion.
                    cmd.Parameters.AddWithValue("@idPresentacion", idPresentacion);
                    try
                    {
                        // Ejecuta el comando para eliminar la presentación.
                        con.Open();
                        cmd.ExecuteNonQuery();
                        // Retorna verdadero si la eliminación fue exitosa.
                        return true;
                    }
                    catch (Exception ex)
                    {
                        // En caso de excepción, retorna falso.
                        return false;
                    }
                }
            }
        }
    }
}
