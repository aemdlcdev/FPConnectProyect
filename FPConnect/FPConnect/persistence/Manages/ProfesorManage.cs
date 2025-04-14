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

    class ProfesorManage
    {       
        private ObservableCollection<Profesor> listaUsuarios { get; set; }
        private DBBroker db = DBBroker.ObtenerAgente();

        public ProfesorManage()
        {         
            listaUsuarios = new ObservableCollection<Profesor>();
        }

        public ObservableCollection<Profesor> LeerUsuarios()
        {

            Profesor usuario = null;
            db = DBBroker.ObtenerAgente();

            var resultado = db.LeerSinParametros("SELECT id_profesor,id_rol,id_centro,id_familia,nombre,apellidos, email, password,sexo FROM fpc.profesores;");

            foreach (ObservableCollection<Object> c in resultado)
            {
                usuario = new Profesor(
                    int.Parse(c[0].ToString()), //id
                    int.Parse(c[1].ToString()), //id_rol
                    int.Parse(c[2].ToString()), //id_centro
                    int.Parse(c[3].ToString()), //id_familia
                    c[4].ToString(), //nombre
                    c[5].ToString(), //apellidos
                    c[6].ToString(), //email
                    c[7].ToString(), //password
                    c[8].ToString());
                this.listaUsuarios.Add(usuario);
            }
            return listaUsuarios;
        }

        public Profesor autentificarUsuario(string correo, string password)
        {
            db = DBBroker.ObtenerAgente();
            string passwordEncrypted = Seguridad.EncriptarContraseña(password);

            string query = "SELECT id_profesor,id_rol,id_centro,id_familia, nombre,apellidos, email, password,sexo FROM fpc.profesores WHERE email = @email AND password = @password LIMIT 1;";
            var parametros = new Dictionary<string, object>
            {
                { "@email", correo }, 
                { "@password", passwordEncrypted }
            };

            var resultado = db.LeerConParametros(query, parametros);

            if (resultado.Count > 0)
            {
                
                var fila = resultado[0] as ObservableCollection<object>; 

                
                Profesor profesor = new Profesor
                {
                    id_profesor = Convert.ToInt32(fila[0]),
                    id_rol = Convert.ToInt32(fila[1]),
                    id_centro = Convert.ToInt32(fila[2]),
                    id_familia = Convert.ToInt32(fila[3]),
                    nombre = fila[4].ToString(),
                    apellidos = fila[5].ToString(),
                    email = fila[6].ToString(), 
                    password = fila[7].ToString(),
                    sexo = fila[8].ToString(),
                    
                };

                Console.WriteLine("Inicio de sesión exitoso.");
                return profesor;
            }
            else
            {
                Console.WriteLine("Credenciales incorrectas.");
                return null; // usuario no encontrado
            }
        }

    }
}
