using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPConnect.domain;
using FPConnect.HelperClasses;

namespace FPConnect.persistence.Manages
{

    class UsuarioManage
    {
        private DataTable dataTable { get; set; }
        private ObservableCollection<Usuario> listaUsuarios { get; set; }
        private DBBroker dbBroker = DBBroker.ObtenerAgente();

        public UsuarioManage()
        {
            dataTable = new DataTable();
            listaUsuarios = new ObservableCollection<Usuario>();
        }

        

        public Usuario autentificarUsuario(string correo, string password)
        {
            var db = DBBroker.ObtenerAgente();
            string passwordEncrypted = Seguridad.EncriptarContraseña(password);

            string query = "SELECT id_usuario, email, password FROM fpc.usuario WHERE email = @email AND password = @password LIMIT 1;";
            var parametros = new Dictionary<string, object>
            {
                { "@email", correo }, 
                { "@password", passwordEncrypted }
            };

            var resultado = db.Leer(query, parametros);

            if (resultado.Count > 0)
            {
                
                var fila = resultado[0] as ObservableCollection<object>; 

                
                Usuario usuario = new Usuario
                {
                    idUsuario = Convert.ToInt32(fila[0]), 
                    email = fila[1].ToString(), 
                    password = fila[2].ToString() 
                };

                Console.WriteLine("Inicio de sesión exitoso.");
                return usuario; // Devolvemos el objeto Usuario
            }
            else
            {
                Console.WriteLine("Credenciales incorrectas.");
                return null; // Usuario no encontrado
            }
        }

    }
}
