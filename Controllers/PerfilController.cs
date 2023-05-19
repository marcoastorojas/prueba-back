using Microsoft.AspNetCore.Mvc;
using prueba_backend.Models;

namespace prueba_backend.Controllers;

[ApiController]
[Route("[controller]")]
public class PerfilController : ControllerBase
{
    private readonly PruebaContext _dbContext;
    public PerfilController(PruebaContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("getPerfil")]
    public ActionResult<List<Perfil>> Get()
    {
        List<Perfil> perfiles = _dbContext.perfiles.ToList();
        return Ok(perfiles);
    }

    [HttpPost("newPeril")]
    public ActionResult<List<User>> Post(Perfil perfil)
    {
        if (perfil == null)
        {
            return BadRequest("El perfil no puede ser nulo");
        }

        // Realiza las validaciones necesarias
        if (string.IsNullOrEmpty(perfil.Value))
        {
            return BadRequest("El value de perfil es requerido");
        }
        

        // Genera un nuevo ID para el usuario
        perfil.Id = Guid.NewGuid();

        // Guarda el nuevo usuario en la base de datos
        _dbContext.perfiles.Add(perfil);
        _dbContext.SaveChanges();

        // Devuelve una respuesta HTTP 201 Created junto con el nuevo usuario creado
        return CreatedAtAction(nameof(GetPerfilById), new { id = perfil.Id }, perfil);
    }

    [HttpGet("{id}")]
    public IActionResult GetPerfilById(Guid id)
    {
        var perfil = _dbContext.perfiles.Find(id);
        if (perfil == null)
        {
            return NotFound(); // Devuelve una respuesta 404 si no se encuentra el usuario
        }
        return Ok(perfil); // Devuelve el usuario encontrado en una respuesta 200 OK
    }
}
