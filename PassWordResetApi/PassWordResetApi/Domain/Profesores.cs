using System;
using System.ComponentModel.DataAnnotations;

namespace PassWordResetApi.domain
{
    public class Profesores
    {
        [Key]
        public required int id_profesor { get; set; }
        public required string nombre { get; set; }
        public required string apellidos { get; set; }
        public required string email { get; set; }
        public required string password { get; set; }
        public string? reset_password_token { get; set; }
        public DateTime? reset_password_token_expiry { get; set; }
        public required int id_rol { get; set; }
    }
}
