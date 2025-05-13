using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPConnect.persistence.Manages;

namespace FPConnect.domain
{
    public class Perfil
    {
        public int id_perfil { get; set; }
        public int id_familia { get; set; }
        public int id_grado { get; set; }

        public string nombre { get; set; }

        public string nombre_familia { get; set; }
        public string nombre_grado { get; set; }

        public int activo { get; set; } // 1 activo, 2 inactivo

        private PerfilManage pm;

        public Perfil() { pm = new PerfilManage(); }

        public Perfil(int id_perfil, int id_familia,int id_grado, string nombre)
        {
            this.id_perfil = id_perfil;
            this.id_familia = id_familia;
            this.id_grado = id_grado;
            this.nombre = nombre;
            pm = new PerfilManage();
        }
        public Perfil(int id_perfil, int id_familia, int id_grado, string nombre,string nombre_familia,string nombre_grado,int activo)
        {
            this.id_perfil = id_perfil;
            this.id_familia = id_familia;
            this.id_grado = id_grado;
            this.nombre = nombre;
            this.nombre_familia = nombre_familia;
            this.nombre_grado = nombre_grado;
            this.activo = activo;
            pm = new PerfilManage();
        }

        public ObservableCollection<Perfil> LeerPerfilesPorCentro(int id_centro)
        {
            return pm.LeerPerfilesPorCentro(id_centro);
        }

        public ObservableCollection<Perfil> LeerPerfilesFiltrados(int id_centro, int id_grado, int id_familia)
        {
            return pm.LeerPerfilesFiltrados(id_centro,id_grado,id_familia);
        }

        public void InsertarPerfil(Perfil perfil)
        {
            pm.InsertarPerfil(perfil);
        }
        
        public void ModificarPerfil(Perfil perfil)
        {
            pm.ModificarPerfil(perfil);
        }

        public void EleminarPerfil(Perfil perfil)
        {
            pm.EliminadoLogicoPerfil(perfil.id_perfil);
        }

        public override string ToString()
        {
            return nombre + " [" + id_perfil + "]";
        }

    }
}
