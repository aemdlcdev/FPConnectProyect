using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPConnect.persistence.Manages;

namespace FPConnect.domain
{
    internal class Grado
    {
        public int id_grado { get; set; }
        public string nombre { get; set; }
        private GradoManage gm;
        public Grado() { gm = new GradoManage(); }
        public Grado(int id_grado, string nombre)
        {
            this.id_grado = id_grado;
            this.nombre = nombre;
            gm = new GradoManage();
        }

        public ObservableCollection<Grado> LeerGrados()
        {
            return gm.LeerGrados();
        }

        public override string ToString()
        {
            return nombre +  " ["+ id_grado + "]";
        }

    }
}
