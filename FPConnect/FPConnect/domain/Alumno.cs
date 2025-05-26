using System;
using System.Collections.ObjectModel;
using System.Windows.Media;
using FPConnect.persistence.Manages;

namespace FPConnect.domain
{
    public class Alumno
    {
        public int id_alumno { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public string email { get; set; }
        public string first_char { get; set; }
        public Brush bgColor { get; set; }
        public int activo { get; set; } // 1 activo, 2 inactivo
        public int id_curso { get; set; }
        public int id_convocatoria { get; set; }
        public int id_fase { get; set; }
        private AlumnoManage am;

        // Constructor por defecto
        public Alumno()
        {
            am = new AlumnoManage();
        }

        // Constructor completo con id_alumno
        public Alumno(int id_alumno, string nombre, string apellidos,string email, string first_char, string bgColor, int activo, int id_curso, int id_convocatoria, int id_fase)
        {
            this.id_alumno = id_alumno;
            this.nombre = nombre;
            this.apellidos = apellidos;
            this.email = email;
            this.first_char = first_char;
            this.bgColor = (Brush)new BrushConverter().ConvertFromString(bgColor);
            this.activo = activo;
            this.id_curso = id_curso;
            this.id_convocatoria = id_convocatoria;
            this.id_fase = id_fase;
            am = new AlumnoManage();
        }

        // Constructor sin id_alumno (para nuevos alumnos)
        public Alumno(string nombre, string apellidos, string email,string first_char, string bgColor, int activo, int id_curso, int id_convocatoria, int id_fase)
        {
            this.nombre = nombre;
            this.apellidos = apellidos;
            this.email = email;
            this.first_char = first_char;
            this.bgColor = (Brush)new BrushConverter().ConvertFromString(bgColor);
            this.activo = activo;
            this.id_curso = id_curso;
            this.id_convocatoria = id_convocatoria;
            this.id_fase = id_fase;
            am = new AlumnoManage();
        }

        // Método para insertar un nuevo alumno
        public bool Insertar(Alumno alumno)
        {
            return am.InsertarAlumno(alumno);
        }

        // Método para actualizar un alumno existente
        public bool Actualizar(Alumno alumno)
        {
            return am.ActualizarAlumno(alumno);
        }

        // Método para eliminar lógicamente un alumno
        public bool EliminarLogico(Alumno alumno)
        {
            this.activo = 2; // Inactivo
            return am.ActualizarAlumno(alumno);
        }

        // Método para cambiar de curso y registrar en histórico
        public bool CambiarCurso(int nuevoCursoId, int anioInicio, int anioFin)
        {
            bool resultado = am.RegistrarCambioCurso(this.id_alumno, nuevoCursoId, anioInicio, anioFin);
            if (resultado)
            {
                this.id_curso = nuevoCursoId;
            }
            return resultado;
        }

        // Método estático para obtener alumnos por curso y convocatoria
        public ObservableCollection<Alumno> ObtenerAlumnosPorCursoYConvocatoria(int id_curso, int id_convocatoria)
        {
            return am.LeerAlumnosPorCursoYConvocatoria(id_curso, id_convocatoria);
        }

        // Método estático para obtener alumnos por curso, convocatoria y fase
        public ObservableCollection<Alumno> ObtenerAlumnosPorCursoConvocatoriaYFase(int id_curso, int id_convocatoria, int id_fase)
        {
            return am.LeerAlumnosPorCursoConvocatoriaYFase(id_curso, id_convocatoria, id_fase);
        }

        // Método para obtener el historial académico del alumno
        public dynamic ObtenerHistorialAcademico()
        {
            return am.ObtenerHistorialAcademico(this.id_alumno);
        }

        // Método para obtener el nombre del curso
        public string ObtenerNombreCurso()
        {
            return am.ObtenerNombreCurso(this.id_curso);
        }

        // Método para obtener el nombre de la fase
        public string ObtenerNombreFase()
        {
            return am.ObtenerNombreFase(this.id_fase);
        }

        // Método para obtener información de la convocatoria
        public dynamic ObtenerInfoConvocatoria()
        {
            return am.ObtenerInfoConvocatoria(this.id_convocatoria);
        }

        // Método para obtener información del perfil a través del curso
        public dynamic ObtenerPerfilCurso()
        {
            return am.ObtenerPerfilPorCurso(this.id_curso);
        }

        // Override de ToString para mostrar información del alumno
        public override string ToString()
        {
            return $"{nombre} {apellidos}";
        }
    }
}