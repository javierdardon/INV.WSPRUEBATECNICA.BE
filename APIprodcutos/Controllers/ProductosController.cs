using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using APIprodcutos.Models;
using APIproductos.Data;

namespace APIproductos.Controllers
{
    // Controlador para las acciones de la API relacionadas con los productos.
    public class ProductoController : ApiController
    {
        // GET: api/Producto
        // Método para obtener la lista de todos los productos.
        [HttpGet]
        [Route("api/Producto")]
        public List<Productos> Get()
        {
            // Llamada al método Listar de ProductoData que devuelve todos los productos.
            return ProductoData.Listar();
        }

        // POST: api/Producto
        // Método para insertar un nuevo producto.
        [HttpPost]
        [Route("api/Producto")]
        public bool Post([FromBody] Productos producto)
        {
            // Llamada al método Insertar de ProductoData que añade un nuevo producto.
            return ProductoData.Insertar(producto);
        }

        // PUT: api/Producto
        // Método para actualizar un producto existente.
        [HttpPut]
        [Route("api/Producto")]
        public IHttpActionResult Put([FromBody] Productos producto)
        {
            // Validación para asegurar que el producto no es nulo y tiene un ID válido.
            if (producto == null || producto.IdProducto == 0)
            {
                // Respuesta de error si el producto es nulo o el ID no es válido.
                return Content(HttpStatusCode.BadRequest, "El producto es nulo o el ID no es válido.");
            }

            try
            {
                // Intenta actualizar el producto y guarda el resultado.
                bool resultado = ProductoData.Modificar(producto);

                if (resultado)
                {
                    // Respuesta de éxito si el producto se actualizó correctamente.
                    return Ok("Producto actualizado correctamente.");
                }
                else
                {
                    // Respuesta de error si el producto no se encuentra.
                    return Content(HttpStatusCode.NotFound, "El producto no se pudo actualizar porque no se encontró.");
                }
            }
            catch (ArgumentException ex)
            {
                // Captura el error si una descripción no existe en la base de datos y devuelve un mensaje de error.
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                // Captura cualquier otro error inesperado y devuelve un error del servidor interno.
                return InternalServerError(ex);
            }
        }

        // DELETE: api/Producto/5
        // Método para eliminar un producto por su ID.
        [HttpDelete]
        [Route("api/Producto/{id}")]
        public bool Delete(int id)
        {
            // Llamada al método Eliminar de ProductoData que borra un producto según su ID.
            return ProductoData.Eliminar(id);
        }
    }
}
