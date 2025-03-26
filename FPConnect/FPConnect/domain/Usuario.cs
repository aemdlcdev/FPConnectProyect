using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPConnect.persistence.Manages;

namespace FPConnect.domain
{
    class Usuario
    {
        public int idUsuario {  get; set; }
        public string email { get; set; }
        public string password { get; set; }

        private UsuarioManage um;

        public Usuario() { um = new UsuarioManage(); }

        public Usuario (int id,string email, string password)
        {
            this.idUsuario = id;
            this.email = email;
            this.password = password;
            um = new UsuarioManage();
        }

        

        public Usuario autentificarUsuario(string email, string password) 
        {
           return um.autentificarUsuario (email, password);
        }

    }
}
