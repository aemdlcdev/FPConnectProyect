using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPConnect.domain
{
    class Usuario
    {
        public int idUsuario {  get; set; }
        public string email { get; set; }
        public string password { get; set; }

        public Usuario() { }

        public Usuario (int id,string email, string password)
        {
            this.idUsuario = id;
            this.email = email;
            this.password = password;
        }


    }
}
