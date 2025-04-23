using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPConnect.persistence.Manages;

namespace FPConnect.domain
{
    internal class Perfil
    {
        public int id_perfil { get; set; }
        public int id_familia { get; set; }
        public string nombre { get; set; }

        private PerfilManage pm;

        public Perfil() { pm = new PerfilManage(); }

        public Perfil(int id_perfil, int id_familia, string nombre)
        {
            this.id_perfil = id_perfil;
            this.id_familia = id_familia;
            this.nombre = nombre;
            pm = new PerfilManage();
        }

        public ObservableCollection<Perfil> LeerPerfilCentro(int id_centro, int id_grado)
        {
            return pm.LeerPerfilCentro(id_centro, id_grado);
        }

        public override string ToString()
        {
            return nombre + " [" + id_perfil + "]";
        }

    }
}
