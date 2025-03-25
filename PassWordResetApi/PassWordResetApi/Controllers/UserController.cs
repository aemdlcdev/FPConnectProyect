using MailKit;
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
        private readonly EmailService _emailService;

        public UserController(ApplicationDbContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        // GET api/user
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> Get()
        {
            return await _context.Usuario.ToListAsync(); // devuelve todos los usuarios de la base de datos (fcp.usuario)
        }

        // GET api/user/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> Get(int id)
        {
            var user = await _context.Usuario.FindAsync(id);
            if (user == null)
            {
                return NotFound("Usuario no encontrado");
            }
            return Ok(user); // devuvelve un usuario por ID
        }

        // PUT api/user/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Usuario updatedUser)
        {
            if (id != updatedUser.id_usuario)
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
                if (!_context.Usuario.Any(e => e.id_usuario == id))
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

        // POST api/user/reset-password
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            var user = await _context.Usuario.FirstOrDefaultAsync(u => u.email == request.Email);
            if (user == null)
            {
                return NotFound("Usuario no encontrado");
            }

            // Genero un token de restablecimiento de contraseña
            var resetToken = Guid.NewGuid().ToString();
            user.ResetPasswordToken = resetToken;
            user.ResetPasswordTokenExpiry = DateTime.UtcNow.AddHours(1); // Le pongo validez de una hora

            await _context.SaveChangesAsync();

            // Envía el correo electrónico con el enlace de restablecimiento de contraseña
            var resetLink = $"https://fpconnect-resetpassword.netlify.app/index.html?token={resetToken}";
            var message = $"Haga clic en el siguiente enlace para restablecer su contraseña: {resetLink}";
            await _emailService.SendEmailAsync(request.Email, "Restablecimiento de contraseña", message);

            return Ok("Correo electrónico de restablecimiento de contraseña enviado");
        }

        // POST api/user/confirm-reset-password
        [HttpPost("confirm-reset-password")]
        public async Task<IActionResult> ConfirmResetPassword([FromBody] ConfirmResetPasswordRequest request)
        {
            var user = await _context.Usuario.FirstOrDefaultAsync(u => u.ResetPasswordToken == request.Token);
            if (user == null || user.ResetPasswordTokenExpiry < DateTime.UtcNow)
            {
                return BadRequest("Token inválido o expirado");
            }

            // Restablece la contraseña del usuario
            user.password = request.NewPassword;
            user.ResetPasswordToken = null;
            user.ResetPasswordTokenExpiry = null;

            await _context.SaveChangesAsync();

            return Ok("Contraseña restablecida con éxito");
        }
    }

    public class ResetPasswordRequest
    {
        public string Email { get; set; }
    }

    public class ConfirmResetPasswordRequest
    {
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }
}







