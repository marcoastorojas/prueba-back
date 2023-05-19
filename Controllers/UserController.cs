using Microsoft.AspNetCore.Mvc;
using prueba_backend.Models;

namespace prueba_backend.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly PruebaContext _dbContext;
    public UserController(PruebaContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("getUsers")]
    public ActionResult<List<User>> Get()
    {
        List<User> users = _dbContext.users.ToList();
        return Ok(users);
    }

    [HttpPost("newuser")]
    public ActionResult<List<User>> Post(User user)
    {
        if (user == null)
        {
            return BadRequest("El usuario no puede ser nulo");
        }

        // Realiza las validaciones necesarias
        if (string.IsNullOrEmpty(user.Nombre))
        {
            return BadRequest("El nombre de usuario es requerido");
        }

      
        // Genera un nuevo ID para el usuario
        user.Id = Guid.NewGuid();

        return Ok("todo bien:" + user);

    }

    [HttpGet("{id}")]
    public IActionResult GetUserById(Guid id)
    {
        var user = _dbContext.users.Find(id);
        if (user == null)
        {
            return NotFound(); // Devuelve una respuesta 404 si no se encuentra el usuario
        }
        return Ok(user); // Devuelve el usuario encontrado en una respuesta 200 OK
    }


    [HttpPost("bulk")]
    public async Task<IActionResult> AddUsers(List<User> users)
    {
        try
        {
            _dbContext.users.AddRange(users);
            await _dbContext.SaveChangesAsync();
            return Ok("Usuarios agregados correctamente");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error al agregar usuarios: {ex.Message}");
        }
    }
    [HttpDelete("eliminar")]
    public async Task<IActionResult> Eliminar()
    {
        try
        {
            var users = _dbContext.users.ToList();
            _dbContext.users.RemoveRange(users);
            await _dbContext.SaveChangesAsync();
            return Ok("Usuarios agregados correctamente");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error al agregar usuarios: {ex.Message}");
        }
    }
}
