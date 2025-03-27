using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPConnect.persistence.Manages;

namespace FPConnect.domain
{
    class Profesor
    {
        public int idUsuario {  get; set; }
        public string email { get; set; }
        public string password { get; set; }

        private ProfesorManage um;

        public Profesor() { um = new ProfesorManage(); }

        public Profesor (int id,string email, string password)
        {
            this.idUsuario = id;
            this.email = email;
            this.password = password;
            um = new ProfesorManage();
        }

        

        public Profesor autentificarUsuario(string email, string password) 
        {
           return um.autentificarUsuario (email, password);
        }

    }
}
