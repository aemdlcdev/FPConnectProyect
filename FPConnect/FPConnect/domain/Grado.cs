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
        public int id_centro { get; set; }
        public string nombre { get; set; }
        private GradoManage gm;
        public Grado() { gm = new GradoManage(); }
        public Grado(int id_grado,int id_centro, string nombre)
        {
            this.id_grado = id_grado;
            this.id_centro = id_centro;
            this.nombre = nombre;
            gm = new GradoManage();
        }

        public ObservableCollection<Grado> LeerGradosPorCentro(int id_centro)
        {
            return gm.LeerGradosPorCentro(id_centro);
        }

        public override string ToString()
        {
            return nombre +  " ["+ id_grado + "]";
        }

    }
}
