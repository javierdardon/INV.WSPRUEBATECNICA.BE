using APIprodcutos.Data;
using APIprodcutos.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace WebApiCarros.Data
{
    // Clase para operaciones de datos relacionadas con 'Zona'
    public class ZonaData
    {
        // Método para obtener una lista de todas las zonas desde la base de datos
        public static List<Zonas> Listar()
        {
            var lista = new List<Zonas>(); // Lista para almacenar los objetos de zona recuperados
            string query = "SELECT id_zona, descripcion FROM Zona"; // Consulta SQL para seleccionar todas las zonas

            try
            {
                using (SqlConnection con = new SqlConnection(ConexionDB.cn))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open(); // Abre la conexión a la base de datos
                        using (SqlDataReader dr = cmd.ExecuteReader()) // Ejecuta la consulta
                        {
                            while (dr.Read()) // Mientras haya filas para leer
                            {
                                // Crea un objeto Zonas y lo añade a la lista
                                lista.Add(new Zonas()
                                {
                                    IdZona = Convert.ToInt32(dr["id_zona"]),
                                    Descripcion = dr["descripcion"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex) // Captura cualquier excepción que ocurra durante la operación
            {
                throw new ApplicationException("Error al listar las zonas: " + ex.Message);
            }

            return lista; // Devuelve la lista de zonas
        }

        // Método para insertar una nueva zona en la base de datos
        public static bool Insertar(Zonas zona)
        {
            string query = "INSERT INTO Zona (descripcion) VALUES (@descripcion)"; // Consulta SQL para insertar una nueva zona
            try
            {
                using (SqlConnection con = new SqlConnection(ConexionDB.cn))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@descripcion", zona.Descripcion); // Asigna el valor al parámetro de la consulta
                        con.Open(); // Abre la conexión a la base de datos
                        return cmd.ExecuteNonQuery() > 0; // Ejecuta la consulta y verifica si se insertó alguna fila
                    }
                }
            }
            catch (Exception ex) // Captura cualquier excepción que ocurra durante la operación
            {
                throw new ApplicationException("Error al insertar la zona: " + ex.Message);
            }
        }

        // Método para modificar una zona existente en la base de datos
        public static bool Modificar(Zonas zona)
        {
            string query = "UPDATE Zona SET descripcion = @descripcion WHERE id_zona = @idZona"; // Consulta SQL para actualizar una zona
            try
            {
                using (SqlConnection con = new SqlConnection(ConexionDB.cn))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@descripcion", zona.Descripcion); // Asigna los valores a los parámetros de la consulta
                        cmd.Parameters.AddWithValue("@idZona", zona.IdZona);
                        con.Open(); // Abre la conexión a la base de datos
                        return cmd.ExecuteNonQuery() > 0; // Ejecuta la consulta y verifica si se modificó alguna fila
                    }
                }
            }
            catch (Exception ex) // Captura cualquier excepción que ocurra durante la operación
            {
                throw new ApplicationException("Error al modificar la zona: " + ex.Message);
            }
        }

        // Método para eliminar una zona de la base de datos por su ID
        public static bool Eliminar(int idZona)
        {
            string query = "DELETE FROM Zona WHERE id_zona = @idZona"; // Consulta SQL para eliminar una zona
            try
            {
                using (SqlConnection con = new SqlConnection(ConexionDB.cn))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@idZona", idZona); // Asigna el valor al parámetro de la consulta
                        con.Open(); // Abre la conexión a la base de datos
                        return cmd.ExecuteNonQuery() > 0; // Ejecuta la consulta y verifica si se eliminó alguna fila
                    }
                }
            }
            catch (Exception ex) // Captura cualquier excepción que ocurra durante la operación
            {
                throw new ApplicationException("Error al eliminar la zona: " + ex.Message);
            }
        }
    }
}
