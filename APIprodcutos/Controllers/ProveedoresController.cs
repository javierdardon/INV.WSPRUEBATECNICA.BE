using System.Collections.Generic;
using System.Web.Http;
using APIprodcutos.Models;
using APIproductos.Data;

namespace APIproductos.Controllers
{
    // Define el controlador para las operaciones CRUD de los proveedores.
    public class ProveedorController : ApiController
    {
        // Método para obtener todos los proveedores.
        // Accesible mediante una solicitud GET a api/Proveedor.
        [HttpGet]
        [Route("api/Proveedor")]
        public List<proveedores> Get()
        {
            // Llama al método Listar() para obtener una lista de todos los proveedores.
            return ProveedoresData.Listar();
        }

        // Método para crear un nuevo proveedor.
        // Accesible mediante una solicitud POST a api/Proveedor.
        [HttpPost]
        [Route("api/Proveedor")]
        public bool Post([FromBody] proveedores proveedor)
        {
            // Llama al método Insertar() pasando el objeto proveedor obtenido del cuerpo de la solicitud.
            // Retorna true si la inserción es exitosa, de lo contrario retorna false.
            return ProveedoresData.Insertar(proveedor);
        }

        // Método para actualizar un proveedor existente.
        // Accesible mediante una solicitud PUT a api/Proveedor.
        [HttpPut]
        [Route("api/Proveedor")]
        public bool Put([FromBody] proveedores proveedor)
        {
            // Verifica que el ID del proveedor sea válido (mayor a 0).
            if (proveedor.IdProveedor <= 0)
            {
                // Retorna false si el ID no es válido, indicando que no se puede realizar la actualización.
                return false;
            }
            // Si el ID es válido, llama al método Modificar() con el objeto proveedor.
            // Retorna true si la modificación es exitosa, de lo contrario retorna false.
            return ProveedoresData.Modificar(proveedor);
        }

        // Método para eliminar un proveedor específico por su ID.
        // Accesible mediante una solicitud DELETE a api/Proveedor/{id}.
        [HttpDelete]
        [Route("api/Proveedor/{id}")]
        public bool Delete(int id)
        {
            // Llama al método Eliminar() pasando el ID del proveedor a eliminar.
            // Retorna true si la eliminación es exitosa, de lo contrario retorna false.
            return ProveedoresData.Eliminar(id);
        }
    }
}
