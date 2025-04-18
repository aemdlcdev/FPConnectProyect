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
        public int id_rol { get; set; }
        public int id_centro { get; set; }
        public int id_familia { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string sexo { get; set; }
       


        private ProfesorManage um;

        public Profesor() { um = new ProfesorManage(); }

        public Profesor( int id_rol, int id_centro, int id_familia, string nombre, string apellidos, string email, string password, string sexo)
        {
            this.id_rol = id_rol;
            this.id_centro = id_centro;
            this.id_familia = id_familia;
            this.nombre = nombre;
            this.apellidos = apellidos;
            this.email = email;
            this.password = password;
            this.sexo = sexo;
            um = new ProfesorManage();
        }
        public Profesor (int id_profesor,int id_rol,int id_centro,int id_familia, string nombre,string apellidos, string email, string password, string sexo)
        {
            this.id_profesor = id_profesor;
            this.id_rol = id_rol;
            this.id_centro = id_centro;
            this.id_familia = id_familia;
            this.nombre = nombre;
            this.apellidos = apellidos;
            this.email = email;
            this.password = password;
            this.sexo = sexo;
            um = new ProfesorManage();
        }

        

        public Profesor autentificarUsuario(string email, string password) 
        {
           return um.autentificarUsuario (email, password);
        }

        public void InsertarProfesor(Profesor profesor, int[] grados)
        {
            um.InsertarProfesor(profesor, grados);
        }

    }
}
