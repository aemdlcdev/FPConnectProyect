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
        private DataTable dataTable { get; set; }
        private ObservableCollection<Profesor> listaUsuarios { get; set; }
        private DBBroker db = DBBroker.ObtenerAgente();

        public ProfesorManage()
        {
            dataTable = new DataTable();
            listaUsuarios = new ObservableCollection<Profesor>();
        }

        public ObservableCollection<Profesor> LeerUsuarios()
        {

            Profesor usuario = null;
            db = DBBroker.ObtenerAgente();

            var resultado = db.LeerSinParametros("SELECT id_profesor,nombre,apellidos, email, password,id_rol FROM fpc.profesores;");

            foreach (ObservableCollection<Object> c in resultado)
            {
                usuario = new Profesor(
                    int.Parse(c[0].ToString()), //id
                    c[1].ToString(), //nombre
                    c[2].ToString(), //apellidos
                    c[3].ToString(), //email
                    c[4].ToString(), //password
                    int.Parse(c[5].ToString())); //rol

                this.listaUsuarios.Add(usuario);
            }
            return listaUsuarios;
        }

        public Profesor autentificarUsuario(string correo, string password)
        {
            db = DBBroker.ObtenerAgente();
            string passwordEncrypted = Seguridad.EncriptarContraseña(password);

            string query = "SELECT id_profesor,nombre,apellidos, email, password,id_rol FROM fpc.profesores WHERE email = @email AND password = @password LIMIT 1;";
            var parametros = new Dictionary<string, object>
            {
                { "@email", correo }, 
                { "@password", passwordEncrypted }
            };

            var resultado = db.Leer(query, parametros);

            if (resultado.Count > 0)
            {
                
                var fila = resultado[0] as ObservableCollection<object>; 

                
                Profesor profesor = new Profesor
                {
                    id_profesor = Convert.ToInt32(fila[0]), 
                    email = fila[1].ToString(), 
                    password = fila[2].ToString() 
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
