using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPConnect.persistence.Manages;

namespace FPConnect.domain
{
    public class Profesor
    {
        public int id_profesor { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string sexo { get; set; }
        public int id_rol { get; set; }

        private ProfesorManage um;

        public Profesor() { um = new ProfesorManage(); }

        public Profesor (int id_profesor, string nombre,string apellidos, string email, string password, string sexo, int id_rol)
        {
            this.id_profesor = id_profesor;
            this.nombre = nombre;
            this.apellidos = apellidos;
            this.email = email;
            this.password = password;
            this.sexo = sexo;
            this.id_rol = id_rol;
            um = new ProfesorManage();
        }

        

        public Profesor autentificarUsuario(string email, string password) 
        {
           return um.autentificarUsuario (email, password);
        }

    }
}
