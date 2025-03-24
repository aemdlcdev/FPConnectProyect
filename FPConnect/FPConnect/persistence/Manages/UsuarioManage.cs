using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPConnect.domain;

namespace FPConnect.persistence.Manages
{

    class UsuarioManage
    {
        private DataTable dataTable { get; set; }
        private ObservableCollection<Usuario> listaUsuarios { get; set; }
        private DBBroker dbBroker = DBBroker.obtenerAgente();

        public UsuarioManage()
        {
            dataTable = new DataTable();
            listaUsuarios = new ObservableCollection<Usuario>();
        }

        public ObservableCollection<Usuario> LeerUsuarios()
        {
            Usuario usuario = null;

            ObservableCollection<Object> aux = DBBroker.obtenerAgente().leer("SELECT * FROM fpc.usuario");

            foreach (ObservableCollection<Object> c in aux)
            {
                usuario = new Usuario(int.Parse(c[0].ToString()), c[1].ToString(), c[2].ToString());

                this.listaUsuarios.Add(usuario);
            }

            return listaUsuarios;

        }

        public void ModificarUsuario(Usuario usuario)
        {
            string sql = $"UPDATE fpc.usuario SET email = '{usuario.email}', password = '{usuario.password}' WHERE idUsuario = '{usuario.idUsuario}'";
            dbBroker.modificar(sql);

            var usuarioExistente = listaUsuarios.FirstOrDefault(u => u.idUsuario == u.idUsuario);
            if (usuarioExistente != null)
            {
                usuarioExistente.email = usuario.email;
                usuarioExistente.password = usuario.password;
            }
        }

    }
}
