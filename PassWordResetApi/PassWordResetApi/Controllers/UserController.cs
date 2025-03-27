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
        public async Task<ActionResult<IEnumerable<Profesores>>> Get()
        {
            return await _context.Profesores.ToListAsync(); // devuelve todos los usuarios de la base de datos (fcp.usuario)
        }

        // GET api/user/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Profesores>> Get(int id)
        {
            var user = await _context.Profesores.FindAsync(id);
            if (user == null)
            {
                return NotFound("Usuario no encontrado");
            }
            return Ok(user); // devuvelve un usuario por ID
        }

        // PUT api/user/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Profesores updatedUser)
        {
            if (id != updatedUser.id_profesor)
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
                if (!_context.Profesores.Any(e => e.id_profesor == id))
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
            var user = await _context.Profesores.FirstOrDefaultAsync(u => u.email == request.Email);
            Console.WriteLine(user.email);
            if (user == null)
            {
                return NotFound("Usuario no encontrado");
            }

            // Genero un token de restablecimiento de contraseña
            var resetToken = Guid.NewGuid().ToString();
            user.reset_password_token = resetToken;
            user.reset_password_token_expiry = DateTime.UtcNow.AddHours(1); // Le pongo validez de una hora

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
            var user = await _context.Profesores.FirstOrDefaultAsync(u => u.reset_password_token == request.Token);
            if (user == null || user.reset_password_token_expiry < DateTime.UtcNow)
            {
                return BadRequest("Token inválido o expirado");
            }

            // Restablece la contraseña del usuario
            user.password = Seguridad.EncriptarContraseña(request.NewPassword);

            user.reset_password_token = null;
            user.reset_password_token_expiry = null;

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







