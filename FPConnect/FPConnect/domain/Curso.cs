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
        public int id_grado { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public int anio_inicio { get; set; }
        public int anio_fin { get; set; }
        private CursosManage cursosManage;
        public Curso() { cursosManage = new CursosManage(); }
        public Curso(int id_curso, int id_grado, string nombre, string descripcion, int anio_inicio, int anio_fin)
        {
            this.id_curso = id_curso;
            this.id_grado = id_grado;
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.anio_inicio = anio_inicio;
            this.anio_fin = anio_fin;
            cursosManage = new CursosManage();
        }

        public ObservableCollection<Curso> LeerCursosPorGrado(int id_grado)
        {
            return cursosManage.LeerCursosPorGrado(id_grado);
        }

        public override string ToString()
        {
            return nombre + " [" + id_curso + "]";
        }

    }
}
