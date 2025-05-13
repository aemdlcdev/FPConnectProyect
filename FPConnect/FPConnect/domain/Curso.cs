using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPConnect.persistence.Manages;

namespace FPConnect.domain
{
    internal class Curso
    {
        public int id_curso { get; set; }
        public int id_perfil { get; set; }
        public string nivel { get; set; }
        public int anio_inicio { get; set; }
        public int anio_fin { get; set; }
        public int activo { get; set; } // 1 activo, 2 inactivo

        private CursosManage cursosManage;
        public Curso() { cursosManage = new CursosManage(); }
        public Curso(int id_curso, int id_perfil,string nivel, int anio_inicio, int anio_fin, int activo)
        {
            this.id_curso = id_curso;
            this.id_perfil = id_perfil;      
            this.nivel = nivel;
            this.anio_inicio = anio_inicio;
            this.anio_fin = anio_fin;
            this.activo = activo;
            cursosManage = new CursosManage();    
        }
        public Curso(int id_perfil, string nivel, int anio_inicio, int anio_fin, int activo)
        {
            this.id_perfil = id_perfil;
            this.nivel = nivel;
            this.anio_inicio = anio_inicio;
            this.anio_fin = anio_fin;
            this.activo = activo;
            cursosManage = new CursosManage();
        }


        public ObservableCollection<Curso> LeerCursosPorGrado(int id_centro, int id_perfil, int id_familia)
        {
            return cursosManage.LeerCursosFiltrado(id_centro,id_perfil,id_familia);
        }

        public override string ToString()
        {
            return id_perfil + " [" + id_curso + "]";
        }

    }
}
