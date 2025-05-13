using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPConnect.persistence.Manages;

namespace FPConnect.domain
{
    public class FamiliaProfesional
    {
        public int id_familia { get; set; }
        public int id_centro { get; set; }
        public string nombre { get; set; }
        public int activo { get; set; } 

        private FamiliaProfesionalManage fpm;

        public FamiliaProfesional() { fpm = new FamiliaProfesionalManage(); }

        public FamiliaProfesional(int id_familia, int id_centro, string nombre, int activo)
        {
            this.id_familia = id_familia;
            this.id_centro = id_centro;
            this.nombre = nombre;
            this.activo = activo;
            this.fpm = new FamiliaProfesionalManage();
            this.activo = activo;
        }

        public FamiliaProfesional(int id_centro, string nombre,int activo)
        {
            this.id_centro = id_centro;
            this.nombre = nombre;
            this.activo = activo;
            this.fpm = new FamiliaProfesionalManage();
        }
        public FamiliaProfesional(int id_centro, string nombre)
        {
            this.id_centro = id_centro;
            this.nombre = nombre;
            this.fpm = new FamiliaProfesionalManage();
        }

        public ObservableCollection<FamiliaProfesional> LeerFamiliasCentro(int id_centro) 
        {
            return fpm.LeerFamiliasCentro(id_centro);
        }

        public ObservableCollection<FamiliaProfesional> LeerFamiliasPorCentroYGrado(int id_centro,int id_grado)
        {
            return fpm.LeerFamiliasPorCentroYGrado(id_centro, id_grado);
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
            fpm.EliminadoLogicoFamilia(id_familia);
        }

        public override string ToString()
        {
            return this.nombre + " " + "["+this.id_familia+"]";
        }

    }
}
