using System;
using System.ComponentModel.DataAnnotations;

namespace PassWordResetApi.domain
{
    public class Usuario
    {
        [Key]
        public required int id_usuario { get; set; }
        public required string email { get; set; }
        public required string password { get; set; }
        public string? ResetPasswordToken { get; set; }
        public DateTime? ResetPasswordTokenExpiry { get; set; }
    }
}
