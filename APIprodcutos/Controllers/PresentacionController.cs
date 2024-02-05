using System.Collections.Generic;
using System.Web.Http;
using APIprodcutos.Models;
using APIproductos.Data;


public class PresentacionController : ApiController
{
    // Obtener todas las presentaciones
    [HttpGet]
    [Route("api/Presentacion")]
    public List<Presentacion> Get()
    {
        return PresentacionData.Listar();
    }

    // Insertar una nueva presentación
    [HttpPost]
    [Route("api/Presentacion")]
    public bool Post([FromBody] Presentacion presentacion)
    {
        return PresentacionData.Insertar(presentacion);
    }

    // Actualizar una presentación existente
    [HttpPut]
    [Route("api/Presentacion")]
    public bool Put([FromBody] Presentacion presentacion)
    {
        if (presentacion.IdPresentacion == 0)
        {
            return false; // Considera lanzar una excepción si el ID no es válido.
        }
        return PresentacionData.Modificar(presentacion);
    }

    // Eliminar una presentación por su ID
    [HttpDelete]
    [Route("api/Presentacion/{id}")]
    public bool Delete(int id)
    {
        return PresentacionData.Eliminar(id);
    }
}
