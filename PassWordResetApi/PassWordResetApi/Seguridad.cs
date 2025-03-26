using System.Security.Cryptography;
using System.Text;

namespace PassWordResetApi
{
    public class Seguridad
    {
        public static string EncriptarContraseña(string contraseña)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Convierto la cadena en un array de bytes
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(contraseña));

                // Convierto el array de bytes en una cadena
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
        public static void GetPassword(string password)
        {
            string contraseñaEncriptada = EncriptarContraseña(password);
            Console.WriteLine($"Contraseña encriptada: {contraseñaEncriptada}");
        }
    }
}
