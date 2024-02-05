using System.Collections.Generic;
using System.Web.Http;
using WebApiCarros.Data; // Asume el uso de un espacio de nombres para acceso a datos
using APIprodcutos.Models; // Asume el uso de un espacio de nombres para los modelos

// Define una clase de controlador que hereda de ApiController
public class ZonaController : ApiController
{
    // Método HTTP GET para obtener todas las zonas
    [HttpGet]
    [Route("api/Zona")]
    public List<Zonas> Get()
    {
        // Llama al método Listar() de ZonaData para obtener una lista de todas las zonas
        return ZonaData.Listar();
    }

    // Método HTTP POST para crear una nueva zona
    [HttpPost]
    [Route("api/Zona")]
    public bool Post([FromBody] Zonas zona)
    {
        // Llama al método Insertar() de ZonaData para insertar una nueva zona en la base de datos
        return ZonaData.Insertar(zona);
    }

    // Método HTTP PUT para actualizar una zona existente
    [HttpPut]
    [Route("api/Zona")]
    public bool Put([FromBody] Zonas zona)
    {
        // Verifica que el IdZona de la zona a modificar sea válido (diferente de 0)
        if (zona.IdZona == 0)
        {
            // Si el IdZona no es válido, retorna false o podría lanzarse una excepción
            return false; // O lanzar una excepción si el ID no es válido.
        }
        // Si el IdZona es válido, llama al método Modificar() de ZonaData para actualizar la zona
        return ZonaData.Modificar(zona);
    }

    // Método HTTP DELETE para eliminar una zona por su ID
    [HttpDelete]
    [Route("api/Zona/{id}")]
    public bool Delete(int id)
    {
        // Llama al método Eliminar() de ZonaData para eliminar la zona con el ID especificado
        return ZonaData.Eliminar(id);
    }
}
