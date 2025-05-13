using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPConnect.persistence.Manages;

namespace FPConnect.domain
{

    public class Curso
    {
        // Propiedades según la estructura de la base de datos
        public int id_curso { get; set; }
        public int id_perfil { get; set; }
        public string nivel { get; set; }
        public int anio_inicio { get; set; }
        public int anio_fin { get; set; }
        public int activo { get; set; }  // 1 = activo, 0 = inactivo

        // Propiedades adicionales para mostrar información relacionada
        public string nombre_perfil { get; set; }

        // Gestor de datos
        private CursosManage cm;

        // Constructor predeterminado
        public Curso()
        {
            cm = new CursosManage();
            activo = 1; // Por defecto activo
        }

        // Constructor para inserción (sin id_curso)
        public Curso(int id_perfil, string nivel, int anio_inicio, int anio_fin)
        {
            this.id_perfil = id_perfil;
            this.nivel = nivel;
            this.anio_inicio = anio_inicio;
            this.anio_fin = anio_fin;
            this.activo = 1; // Por defecto activo
            cm = new CursosManage();
        }

        // Constructor para inserción con especificación de activo
        public Curso(int id_perfil, string nivel, int anio_inicio, int anio_fin, int activo)
        {
            this.id_perfil = id_perfil;
            this.nivel = nivel;
            this.anio_inicio = anio_inicio;
            this.anio_fin = anio_fin;
            this.activo = activo;
            cm = new CursosManage();
        }

        // Constructor completo para lectura de datos
        public Curso(int id_curso, int id_perfil, string nivel, int anio_inicio, int anio_fin, int activo)
        {
            this.id_curso = id_curso;
            this.id_perfil = id_perfil;
            this.nivel = nivel;
            this.anio_inicio = anio_inicio;
            this.anio_fin = anio_fin;
            this.activo = activo;
            cm = new CursosManage();
        }

        // Constructor completo con nombre de perfil (para JOIN)
        public Curso(int id_curso, int id_perfil, string nivel, int anio_inicio, int anio_fin, int activo, string nombre_perfil)
        {
            this.id_curso = id_curso;
            this.id_perfil = id_perfil;
            this.nivel = nivel;
            this.anio_inicio = anio_inicio;
            this.anio_fin = anio_fin;
            this.activo = activo;
            this.nombre_perfil = nombre_perfil;
            cm = new CursosManage();
        }

        // Métodos CRUD delegados a CursoManage
        public bool Insertar()
        {
            return cm.InsertarCurso(this);
        }

        public bool Actualizar()
        {
            return cm.ActualizarCurso(this);
        }

        public bool Eliminar()
        {
            return cm.EliminarCurso(this.id_curso);
        }

        public bool DesactivarCurso()
        {
            return cm.EliminadoLogicoCurso(this.id_curso);
        }

        public ObservableCollection<Curso> LeerTodos()
        {
            return cm.LeerCursos();
        }

        public ObservableCollection<Curso> LeerPorPerfil(int id_perfil)
        {
            return cm.LeerCursosPorPerfil(id_perfil);
        }

        public ObservableCollection<Curso> LeerPorCentro(int id_centro)
        {
            return cm.LeerCursosPorCentro(id_centro);
        }

        public Curso LeerPorId(int id_curso)
        {
            return cm.LeerCursoPorId(id_curso);
        }

        public ObservableCollection<Curso> LeerPorFamilia(int id_familia)
        {
            return cm.LeerCursosPorFamilia(id_familia);
        }

        // Método para mostrar información completa
        public string InfoCompleta()
        {
            if (nombre_perfil != null)
                return $"{nivel} ({anio_inicio}-{anio_fin}) - {nombre_perfil}";
            else
                return $"{nivel} ({anio_inicio}-{anio_fin})";
        }

        public override string ToString()
        {
            return nivel + " " + nombre_perfil + " " + anio_inicio + "/" + anio_fin + " [" + id_curso+ "]";
        }

    }
}
