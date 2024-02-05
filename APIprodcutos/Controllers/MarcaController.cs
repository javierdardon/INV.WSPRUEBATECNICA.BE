using System.Collections.Generic;
using System.Web.Http;

using APIprodcutos.Data;
using APIprodcutos.Models;

public class MarcaController : ApiController
{
    // Listar todas las marcas
    [HttpGet]
    [Route("api/Marca")]
    public List<Marcas> Get()
    {
        return MarcaData.Listar();
    }

    // Insertar una nueva marca
    [HttpPost]
    [Route("api/Marca")]
    public bool Post([FromBody] Marcas marca)
    {
        return MarcaData.Insertar(marca);
    }

    // Actualizar una marca existente
    [HttpPut]
    [Route("api/Marca")]
    public bool Put([FromBody] Marcas marca)
    {
        if (marca.IdMarca == 0)
        {
            return false; //  lanzar una excepción si el ID no es válido.
        }
        return MarcaData.Modificar(marca);
    }

    // Eliminar una marca por su ID
    [HttpDelete]
    [Route("api/Marca/{id}")]
    public bool Delete(int id)
    {
        return MarcaData.Eliminar(id);
    }
}
