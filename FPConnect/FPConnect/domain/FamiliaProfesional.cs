using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPConnect.persistence.Manages;

namespace FPConnect.domain
{
    internal class FamiliaProfesional
    {
        public int id_familia { get; set; }
        public int id_centro { get; set; }
        public string nombre { get; set; }

        private FamiliaProfesionalManage fpm;

        public FamiliaProfesional() { fpm = new FamiliaProfesionalManage(); }

        public FamiliaProfesional(int id_familia, int id_centro, string nombre)
        {
            this.id_familia = id_familia;
            this.id_centro = id_centro;
            this.nombre = nombre;
            this.fpm = new FamiliaProfesionalManage();
        }

        public ObservableCollection<FamiliaProfesional> LeerFamiliasCentro(int id_centro) 
        {
            return fpm.LeerFamiliasCentro(id_centro);
        }

        public void InsertarFamilia(FamiliaProfesional familia) 
        {
            fpm.InsertarFamilia(familia);
        }

        public void ModificarFamilia(FamiliaProfesional familia)
        {
            fpm.ModificarFamilia(familia);
        }

        public void EliminarFamilia(int id_familia)
        {
            fpm.EliminarFamilia(id_familia);
        }

        public override string ToString()
        {
            return this.nombre + " " + "["+this.id_familia+"]";
        }

    }
}
