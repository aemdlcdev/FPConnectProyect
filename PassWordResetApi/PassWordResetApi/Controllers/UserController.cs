using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PassWordResetApi.domain;
using PassWordResetApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PassWordResetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET api/user
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> Get()
        {
            return await _context.Usuarios.ToListAsync(); // Devuelve la lista de usuarios
        }

        // GET api/user/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> Get(int id)
        {
            var user = await _context.Usuarios.FindAsync(id);
            if (user == null)
            {
                return NotFound("Usuario no encontrado");
            }
            return Ok(user); // Devuelve un solo usuario por ID
        }

        // PUT api/user/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Usuario updatedUser)
        {
            if (id != updatedUser.Id)
            {
                return BadRequest("ID del usuario no coincide");
            }

            _context.Entry(updatedUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Usuarios.Any(e => e.Id == id))
                {
                    return NotFound("Usuario no encontrado");
                }
                else
                {
                    throw;
                }
            }

            return NoContent(); // Respuesta exitosa sin contenido
        }
    }
}
