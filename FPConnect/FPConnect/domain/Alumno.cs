using System;
using System.Collections.Generic;
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
        public bool IsSelected { get; set; }
        private AlumnoManage am;
        public Dictionary<string, object> InfoAdicional { get; set; }

        // Constructor por defecto
        public Alumno()
        {
            am = new AlumnoManage();
            InfoAdicional = new Dictionary<string, object>();
        }

        // Constructor completo con id_alumno
        public Alumno(int id_alumno, string nombre, string apellidos,string email, string first_char, string bgColor, int activo, int id_curso, int id_convocatoria, int id_fase)
        {
            this.id_alumno = id_alumno;
            this.nombre = nombre;
            this.apellidos = apellidos;
            this.email = email;
            this.first_char = nombre.Trim().Substring(0, 1).ToUpper(); 
            this.bgColor = (Brush)new BrushConverter().ConvertFromString(bgColor);
            this.activo = activo;
            this.id_curso = id_curso;
            this.id_convocatoria = id_convocatoria;
            this.id_fase = id_fase;
            am = new AlumnoManage();
        }

        public Alumno(int id_alumno, string nombre, string apellidos, string email, string first_char, string bgColor, int activo, int id_curso, int id_convocatoria, int id_fase,bool selected)
        {
            this.id_alumno = id_alumno;
            this.nombre = nombre;
            this.apellidos = apellidos;
            this.email = email;
            this.first_char = nombre.Trim().Substring(0, 1).ToUpper();
            this.bgColor = (Brush)new BrushConverter().ConvertFromString(bgColor);
            this.activo = activo;
            this.id_curso = id_curso;
            this.id_convocatoria = id_convocatoria;
            this.id_fase = id_fase;
            this.IsSelected = selected;
            am = new AlumnoManage();
        }

        // Constructor sin id_alumno (para nuevos alumnos)
        public Alumno(string nombre, string apellidos, string email,string first_char, string bgColor, int activo, int id_curso, int id_convocatoria, int id_fase)
        {
            this.nombre = nombre;
            this.apellidos = apellidos;
            this.email = email;
            this.first_char = nombre.Trim().Substring(0, 1).ToUpper();
            this.bgColor = (Brush)new BrushConverter().ConvertFromString(bgColor);
            this.activo = activo;
            this.id_curso = id_curso;
            this.id_convocatoria = id_convocatoria;
            this.id_fase = id_fase;
            am = new AlumnoManage();
        }

        // Método para insertar un nuevo alumno

        public bool InsertarAlumnoConEmpresa(Alumno alumno, int id_empresa)
        {
            return am.InsertarAlumnoConEmpresa(alumno, id_empresa);
        }
        public bool Insertar(Alumno alumno)
        {
            return am.InsertarAlumno(alumno);
        }

        // Método para actualizar un alumno existente
        public bool Actualizar(Alumno alumno)
        {
            return am.ActualizarAlumno(alumno);
        }

        public bool ActualizarFaseAsignacionEmpresa(int id_alumno, int id_fase) 
        { 
            return am.ActualizarFaseAsignacionEmpresa(id_alumno, id_fase);
        }

        // Método para eliminar lógicamente un alumno
        public bool EliminarLogico(Alumno alumno)
        {
            alumno.activo = 2; // Inactivo
            return am.ActualizarAlumno(alumno);
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

        public ObservableCollection<Alumno> ObtenerAlumnosPorCursoConvocatoriaYFase(int id_curso, int id_convocatoria, int id_fase)
        {
            return am.ObtenerAlumnosPorCursoConvocatoriaYFase(id_curso, id_convocatoria, id_fase);
        }

        public ObservableCollection<Alumno> ObtenerAlumnosPorCursoYFase(int id_curso, int id_fase)
        {
            return am.ObtenerAlumnosPorCursoYFase(id_curso, id_fase);
        }

        public ObservableCollection<Alumno> ObtenerHistoricoAlumnosPorPerfilYAnio(int id_perfil, int anio_inicio) 
        {
            return am.ObtenerHistoricoAlumnosPorPerfilYAnio(id_perfil, anio_inicio);
        }

        public static int ContarAlumnosPorCentro(int id_centro)
        {
            AlumnoManage am = new AlumnoManage();
            return am.ContarAlumnosPorCentro(id_centro);
        }

        // Override de ToString para mostrar información del alumno
        public override string ToString()
        {
            return $"{nombre} {apellidos}";
        }
    }
}