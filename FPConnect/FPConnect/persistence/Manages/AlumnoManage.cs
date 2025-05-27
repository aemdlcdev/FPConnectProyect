using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;
using FPConnect.domain;
using FPConnect.persistence;

namespace FPConnect.persistence.Manages
{
    public class AlumnoManage
    {
        private ObservableCollection<Alumno> listaAlumnos { get; set; }
        private DBBroker db;

        public AlumnoManage()
        {
            listaAlumnos = new ObservableCollection<Alumno>();
        }

        /// <summary>
        /// Inserta un nuevo alumno en la base de datos
        /// </summary>
        /// <param name="alumno">Objeto Alumno a insertar</param>
        /// <returns>True si la inserción fue exitosa, false en caso contrario</returns>
        public bool InsertarAlumno(Alumno alumno)
        {
            db = DBBroker.ObtenerAgente();

            string consulta = @"INSERT INTO fpc.Alumnos (nombre, apellidos, email, first_char, bgColor, activo, id_curso, id_convocatoria, id_fase) 
                              VALUES (@nombre, @apellidos, @email, @first_char, @bgColor, @activo, @id_curso, @id_convocatoria, @id_fase)";

            Dictionary<string, object> parametros = new Dictionary<string, object>
            {
                { "@nombre", alumno.nombre },
                { "@apellidos", alumno.apellidos },
                { "@email", alumno.email },
                { "@first_char", alumno.first_char },
                { "@bgColor", alumno.bgColor.ToString() },
                { "@activo", alumno.activo },
                { "@id_curso", alumno.id_curso },
                { "@id_convocatoria", alumno.id_convocatoria },
                { "@id_fase", alumno.id_fase }
            };

            int resultado = db.Modificar(consulta, parametros);
            return resultado > 0;
        }

        /// <summary>
        /// Actualiza un alumno existente en la base de datos
        /// </summary>
        /// <param name="alumno">Objeto Alumno con la información actualizada</param>
        /// <returns>True si la actualización fue exitosa, false en caso contrario</returns>
        public bool ActualizarAlumno(Alumno alumno)
        {
            db = DBBroker.ObtenerAgente();

            string consulta = @"UPDATE fpc.alumnos 
                              SET nombre = @nombre, 
                                  apellidos = @apellidos, 
                                  email = @email, 
                                  first_char = @first_char, 
                                  bgColor = @bgColor, 
                                  activo = @activo, 
                                  id_curso = @id_curso, 
                                  id_convocatoria = @id_convocatoria, 
                                  id_fase = @id_fase 
                              WHERE id_alumno = @id_alumno";

            Dictionary<string, object> parametros = new Dictionary<string, object>
            {
                { "@id_alumno", alumno.id_alumno },
                { "@nombre", alumno.nombre },
                { "@apellidos", alumno.apellidos },
                { "@email", alumno.email },
                { "@first_char", alumno.first_char },
                { "@bgColor", alumno.bgColor.ToString() },
                { "@activo", alumno.activo },
                { "@id_curso", alumno.id_curso },
                { "@id_convocatoria", alumno.id_convocatoria },
                { "@id_fase", alumno.id_fase }
            };

            int resultado = db.Modificar(consulta, parametros);
            return resultado > 0;
        }

        /// <summary>
        /// Realiza un eliminado lógico de un alumno en la base de datos
        /// </summary>
        /// <param name="id_alumno">ID del alumno a eliminar lógicamente</param>
        /// <returns>True si la operación fue exitosa, false en caso contrario</returns>
        public bool EliminadoLogicoAlumno(int id_alumno)
        {
            db = DBBroker.ObtenerAgente();

            string consulta = "UPDATE fpc.Alumnos SET activo = 2 WHERE id_alumno = @id_alumno";

            Dictionary<string, object> parametros = new Dictionary<string, object>
            {
                { "@id_alumno", id_alumno }
            };

            int resultado = db.Modificar(consulta, parametros);
            return resultado > 0;
        }

        /// <summary>
        /// Obtiene todos los alumnos activos de la base de datos
        /// </summary>
        /// <returns>Colección observable de objetos Alumno</returns>
        public ObservableCollection<Alumno> LeerAlumnos()
        {
            ObservableCollection<Alumno> alumnos = new ObservableCollection<Alumno>();
            db = DBBroker.ObtenerAgente();

            string consulta = @"SELECT a.id_alumno, a.nombre, a.apellidos, a.email, a.first_char, 
                              a.bgColor, a.activo, a.id_curso, a.id_convocatoria, a.id_fase,
                              c.nivel AS nombre_curso
                              FROM fpc.Alumnos a
                              INNER JOIN fpc.Cursos c ON a.id_curso = c.id_curso
                              WHERE a.activo = 1";

            var resultado = db.LeerSinParametros(consulta);

            foreach (ObservableCollection<object> fila in resultado)
            {
                Alumno alumno = new Alumno(
                    int.Parse(fila[0].ToString()),      // id_alumno
                    fila[1].ToString(),                 // nombre
                    fila[2].ToString(),                 // apellidos
                    fila[3].ToString(),                 // email
                    fila[4].ToString(),                 // first_char
                    fila[5].ToString(),                 // bgColor
                    int.Parse(fila[6].ToString()),      // activo
                    int.Parse(fila[7].ToString()),      // id_curso
                    int.Parse(fila[8].ToString()),      // id_convocatoria
                    int.Parse(fila[9].ToString())       // id_fase
                );

                alumnos.Add(alumno);
            }

            return alumnos;
        }

        /// <summary>
        /// Obtiene alumnos por ID de curso
        /// </summary>
        /// <param name="id_curso">ID del curso</param>
        /// <returns>Colección observable de objetos Alumno en el curso especificado</returns>
        public ObservableCollection<Alumno> LeerAlumnosPorCurso(int id_curso)
        {
            ObservableCollection<Alumno> alumnos = new ObservableCollection<Alumno>();
            db = DBBroker.ObtenerAgente();

            string consulta = @"SELECT a.id_alumno, a.nombre, a.apellidos, a.email, a.first_char, 
                              a.bgColor, a.activo, a.id_curso, a.id_convocatoria, a.id_fase,
                              c.nivel AS nombre_curso
                              FROM fpc.Alumnos a
                              INNER JOIN fpc.Cursos c ON a.id_curso = c.id_curso
                              WHERE a.id_curso = @id_curso AND a.activo = 1";

            Dictionary<string, object> parametros = new Dictionary<string, object>
            {
                { "@id_curso", id_curso }
            };

            var resultado = db.LeerConParametros(consulta, parametros);

            foreach (ObservableCollection<object> fila in resultado)
            {
                Alumno alumno = new Alumno(
                    int.Parse(fila[0].ToString()),      // id_alumno
                    fila[1].ToString(),                 // nombre
                    fila[2].ToString(),                 // apellidos
                    fila[3].ToString(),                 // email
                    fila[4].ToString(),                 // first_char
                    fila[5].ToString(),                 // bgColor
                    int.Parse(fila[6].ToString()),      // activo
                    int.Parse(fila[7].ToString()),      // id_curso
                    int.Parse(fila[8].ToString()),      // id_convocatoria
                    int.Parse(fila[9].ToString())       // id_fase
                );

                alumnos.Add(alumno);
            }

            return alumnos;
        }

        /// <summary>
        /// Obtiene alumnos por ID de curso, convocatoria y fase
        /// </summary>
        /// <param name="id_curso">ID del curso</param>
        /// <param name="id_convocatoria">ID de la convocatoria</param>
        /// <param name="id_fase">ID de la fase</param>
        /// <returns>Colección observable de objetos Alumno que cumplen con los criterios</returns>
        public ObservableCollection<Alumno> ObtenerAlumnosPorCursoConvocatoriaYFase(int id_curso, int id_convocatoria, int id_fase)
        {
            ObservableCollection<Alumno> alumnos = new ObservableCollection<Alumno>();
            db = DBBroker.ObtenerAgente();

            string consulta = @"SELECT a.id_alumno, a.nombre, a.apellidos, a.email, a.first_char, 
                      a.bgColor, a.activo, a.id_curso, a.id_convocatoria, a.id_fase,
                      c.nivel AS nombre_curso
                      FROM fpc.Alumnos a
                      INNER JOIN fpc.Cursos c ON a.id_curso = c.id_curso
                      WHERE a.id_curso = @id_curso 
                      AND a.id_convocatoria = @id_convocatoria 
                      AND a.id_fase = @id_fase 
                      AND a.activo = 1";

            Dictionary<string, object> parametros = new Dictionary<string, object>
            {
                { "@id_curso", id_curso },
                { "@id_convocatoria", id_convocatoria },
                { "@id_fase", id_fase }
            };

            var resultado = db.LeerConParametros(consulta, parametros);

            foreach (ObservableCollection<object> fila in resultado)
            {
                Alumno alumno = new Alumno(
                    int.Parse(fila[0].ToString()),      // id_alumno
                    fila[1].ToString(),                 // nombre
                    fila[2].ToString(),                 // apellidos
                    fila[3].ToString(),                 // email
                    fila[4].ToString(),                 // first_char
                    fila[5].ToString(),                 // bgColor
                    int.Parse(fila[6].ToString()),      // activo
                    int.Parse(fila[7].ToString()),      // id_curso
                    int.Parse(fila[8].ToString()),      // id_convocatoria
                    int.Parse(fila[9].ToString())       // id_fase
                );

                alumnos.Add(alumno);
            }

            return alumnos;
        }

        /// <summary>
        /// Obtiene un alumno por su ID
        /// </summary>
        /// <param name="id_alumno">ID del alumno a recuperar</param>
        /// <returns>Objeto Alumno si se encuentra, null en caso contrario</returns>
        public Alumno LeerAlumnoPorId(int id_alumno)
        {
            Alumno alumno = null;
            db = DBBroker.ObtenerAgente();

            string consulta = @"SELECT a.id_alumno, a.nombre, a.apellidos, a.email, a.first_char, 
                              a.bgColor, a.activo, a.id_curso, a.id_convocatoria, a.id_fase,
                              c.nivel AS nombre_curso
                              FROM fpc.Alumnos a
                              INNER JOIN fpc.Cursos c ON a.id_curso = c.id_curso
                              WHERE a.id_alumno = @id_alumno";

            Dictionary<string, object> parametros = new Dictionary<string, object>
            {
                { "@id_alumno", id_alumno }
            };

            var resultado = db.LeerConParametros(consulta, parametros);

            if (resultado.Count > 0)
            {
                var fila = resultado[0] as ObservableCollection<object>;

                alumno = new Alumno(
                    int.Parse(fila[0].ToString()),      // id_alumno
                    fila[1].ToString(),                 // nombre
                    fila[2].ToString(),                 // apellidos
                    fila[3].ToString(),                 // email
                    fila[4].ToString(),                 // first_char
                    fila[5].ToString(),                 // bgColor
                    int.Parse(fila[6].ToString()),      // activo
                    int.Parse(fila[7].ToString()),      // id_curso
                    int.Parse(fila[8].ToString()),      // id_convocatoria
                    int.Parse(fila[9].ToString())       // id_fase
                );
            }

            return alumno;
        }

        #region HistoricoAlumnoCurso Operations

        /// <summary>
        /// Clase para representar un registro del historial académico de un alumno
        /// </summary>
        public class HistoricoAlumno
        {
            public int Id_historico { get; set; }
            public int Id_alumno { get; set; }
            public int Id_curso { get; set; }
            public string Nivel { get; set; }
            public string Nombre_perfil { get; set; }
            public string Nombre_grado { get; set; }
            public string Nombre_familia { get; set; }
            public int Anio_academico_inicio { get; set; }
            public int Anio_academico_fin { get; set; }

            public HistoricoAlumno(int id_historico, int id_alumno, int id_curso, string nivel,
                                  string nombre_perfil, string nombre_grado, string nombre_familia,
                                  int anio_inicio, int anio_fin)
            {
                Id_historico = id_historico;
                Id_alumno = id_alumno;
                Id_curso = id_curso;
                Nivel = nivel;
                Nombre_perfil = nombre_perfil;
                Nombre_grado = nombre_grado;
                Nombre_familia = nombre_familia;
                Anio_academico_inicio = anio_inicio;
                Anio_academico_fin = anio_fin;
            }
        }

        /// <summary>
        /// Añade un nuevo registro al historial de cursos de un estudiante
        /// </summary>
        /// <param name="id_alumno">ID del estudiante</param>
        /// <param name="id_curso">ID del curso</param>
        /// <param name="anio_inicio">Año de inicio</param>
        /// <param name="anio_fin">Año de finalización</param>
        /// <returns>True si la operación fue exitosa, false en caso contrario</returns>
        public bool AgregarHistoricoAlumno(int id_alumno, int id_curso, int anio_inicio, int anio_fin)
        {
            db = DBBroker.ObtenerAgente();

            string consulta = @"INSERT INTO fpc.HistoricoAlumnoCurso (id_alumno, id_curso, anio_academico_inicio, anio_academico_fin) 
                              VALUES (@id_alumno, @id_curso, @anio_inicio, @anio_fin)";

            Dictionary<string, object> parametros = new Dictionary<string, object>
            {
                { "@id_alumno", id_alumno },
                { "@id_curso", id_curso },
                { "@anio_inicio", anio_inicio },
                { "@anio_fin", anio_fin }
            };

            int resultado = db.Modificar(consulta, parametros);
            return resultado > 0;
        }

        /// <summary>
        /// Actualiza un registro histórico existente
        /// </summary>
        /// <param name="id_historico">ID del registro</param>
        /// <param name="id_alumno">ID del estudiante</param>
        /// <param name="id_curso">ID del curso</param>
        /// <param name="anio_inicio">Año de inicio</param>
        /// <param name="anio_fin">Año de finalización</param>
        /// <returns>True si la actualización fue exitosa, false en caso contrario</returns>
        public bool ActualizarHistoricoAlumno(int id_historico, int id_alumno, int id_curso, int anio_inicio, int anio_fin)
        {
            db = DBBroker.ObtenerAgente();

            string consulta = @"UPDATE fpc.HistoricoAlumnoCurso 
                              SET id_alumno = @id_alumno, 
                                  id_curso = @id_curso, 
                                  anio_academico_inicio = @anio_inicio, 
                                  anio_academico_fin = @anio_fin 
                              WHERE id_historico = @id_historico";

            Dictionary<string, object> parametros = new Dictionary<string, object>
            {
                { "@id_historico", id_historico },
                { "@id_alumno", id_alumno },
                { "@id_curso", id_curso },
                { "@anio_inicio", anio_inicio },
                { "@anio_fin", anio_fin }
            };

            int resultado = db.Modificar(consulta, parametros);
            return resultado > 0;
        }

        /// <summary>
        /// Elimina un registro histórico
        /// </summary>
        /// <param name="id_historico">ID del registro a eliminar</param>
        /// <returns>True si la eliminación fue exitosa, false en caso contrario</returns>
        public bool EliminarHistoricoAlumno(int id_historico)
        {
            db = DBBroker.ObtenerAgente();

            string consulta = "DELETE FROM fpc.HistoricoAlumnoCurso WHERE id_historico = @id_historico";

            Dictionary<string, object> parametros = new Dictionary<string, object>
            {
                { "@id_historico", id_historico }
            };

            int resultado = db.Modificar(consulta, parametros);
            return resultado > 0;
        }

        /// <summary>
        /// Obtiene todos los registros históricos para un alumno específico
        /// </summary>
        /// <param name="id_alumno">ID del alumno</param>
        /// <returns>Colección observable de objetos HistoricoAlumno</returns>
        public ObservableCollection<HistoricoAlumno> ObtenerHistoricoPorAlumno(int id_alumno)
        {
            ObservableCollection<HistoricoAlumno> historicos = new ObservableCollection<HistoricoAlumno>();
            db = DBBroker.ObtenerAgente();

            string consulta = @"SELECT h.id_historico, h.id_alumno, h.id_curso, c.nivel, p.nombre AS nombre_perfil, 
                              g.nombre AS nombre_grado, f.nombre AS nombre_familia, 
                              h.anio_academico_inicio, h.anio_academico_fin 
                              FROM fpc.HistoricoAlumnoCurso h
                              JOIN fpc.Cursos c ON h.id_curso = c.id_curso
                              JOIN fpc.Perfiles p ON c.id_perfil = p.id_perfil
                              JOIN fpc.Grados g ON p.id_grado = g.id_grado
                              JOIN fpc.FamiliasProfesionales f ON p.id_familia = f.id_familia
                              WHERE h.id_alumno = @id_alumno
                              ORDER BY h.anio_academico_inicio DESC";

            Dictionary<string, object> parametros = new Dictionary<string, object>
            {
                { "@id_alumno", id_alumno }
            };

            var resultado = db.LeerConParametros(consulta, parametros);

            foreach (ObservableCollection<object> fila in resultado)
            {
                HistoricoAlumno historico = new HistoricoAlumno(
                    Convert.ToInt32(fila[0]),  // id_historico
                    Convert.ToInt32(fila[1]),  // id_alumno
                    Convert.ToInt32(fila[2]),  // id_curso
                    fila[3].ToString(),        // nivel
                    fila[4].ToString(),        // nombre_perfil
                    fila[5].ToString(),        // nombre_grado
                    fila[6].ToString(),        // nombre_familia
                    Convert.ToInt32(fila[7]),  // anio_academico_inicio
                    Convert.ToInt32(fila[8])   // anio_academico_fin
                );

                historicos.Add(historico);
            }

            return historicos;
        }

        /// <summary>
        /// Obtiene todos los estudiantes que estuvieron en un curso específico durante un año académico específico
        /// </summary>
        /// <param name="id_curso">ID del curso</param>
        /// <param name="anio_academico">Año académico</param>
        /// <returns>Colección observable de objetos Alumno</returns>
        public ObservableCollection<Alumno> ObtenerAlumnosPorCursoYAnio(int id_curso, int anio_academico)
        {
            ObservableCollection<Alumno> alumnos = new ObservableCollection<Alumno>();
            db = DBBroker.ObtenerAgente();

            string consulta = @"SELECT a.id_alumno, a.nombre, a.apellidos, a.email, a.first_char, 
                              a.bgColor, a.activo, a.id_curso, a.id_convocatoria, a.id_fase
                              FROM fpc.Alumnos a
                              JOIN fpc.HistoricoAlumnoCurso h ON a.id_alumno = h.id_alumno
                              WHERE h.id_curso = @id_curso 
                              AND @anio_academico BETWEEN h.anio_academico_inicio AND h.anio_academico_fin";

            Dictionary<string, object> parametros = new Dictionary<string, object>
            {
                { "@id_curso", id_curso },
                { "@anio_academico", anio_academico }
            };

            var resultado = db.LeerConParametros(consulta, parametros);

            foreach (ObservableCollection<object> fila in resultado)
            {
                Alumno alumno = new Alumno(
                    int.Parse(fila[0].ToString()),      // id_alumno
                    fila[1].ToString(),                 // nombre
                    fila[2].ToString(),                 // apellidos
                    fila[3].ToString(),                 // email
                    fila[4].ToString(),                 // first_char
                    fila[5].ToString(),                 // bgColor
                    int.Parse(fila[6].ToString()),      // activo
                    int.Parse(fila[7].ToString()),      // id_curso
                    int.Parse(fila[8].ToString()),      // id_convocatoria
                    int.Parse(fila[9].ToString())       // id_fase
                );

                alumnos.Add(alumno);
            }

            return alumnos;
        }

        #endregion

        #region Métodos auxiliares

        /// <summary>
        /// Obtiene el nombre del curso de un alumno
        /// </summary>
        /// <param name="id_curso">ID del curso</param>
        /// <returns>Nombre del curso o "No encontrado" si no existe</returns>
        public string ObtenerNombreCurso(int id_curso)
        {
            db = DBBroker.ObtenerAgente();
            string nombreCurso = "No encontrado";

            string consulta = "SELECT nivel FROM fpc.Cursos WHERE id_curso = @id_curso";

            var parametros = new Dictionary<string, object>
            {
                { "@id_curso", id_curso }
            };

            var resultado = db.LeerConParametros(consulta, parametros);

            if (resultado.Count > 0)
            {
                var fila = resultado[0] as ObservableCollection<object>;
                nombreCurso = fila[0].ToString();
            }

            return nombreCurso;
        }

        /// <summary>
        /// Obtiene el nombre de la convocatoria de un alumno
        /// </summary>
        /// <param name="id_convocatoria">ID de la convocatoria</param>
        /// <returns>Tipo de convocatoria o "No encontrado" si no existe</returns>
        public string ObtenerNombreConvocatoria(int id_convocatoria)
        {
            db = DBBroker.ObtenerAgente();
            string tipoConvocatoria = "No encontrado";

            string consulta = "SELECT tipo FROM fpc.Convocatorias WHERE id_convocatoria = @id_convocatoria";

            var parametros = new Dictionary<string, object>
            {
                { "@id_convocatoria", id_convocatoria }
            };

            var resultado = db.LeerConParametros(consulta, parametros);

            if (resultado.Count > 0)
            {
                var fila = resultado[0] as ObservableCollection<object>;
                tipoConvocatoria = fila[0].ToString();
            }

            return tipoConvocatoria;
        }

        /// <summary>
        /// Obtiene el nombre de la fase de asignación de un alumno
        /// </summary>
        /// <param name="id_fase">ID de la fase</param>
        /// <returns>Nombre de la fase o "No encontrado" si no existe</returns>
        public string ObtenerNombreFase(int id_fase)
        {
            db = DBBroker.ObtenerAgente();
            string nombreFase = "No encontrado";

            string consulta = "SELECT nombre FROM fpc.FasesAsignacion WHERE id_fase = @id_fase";

            var parametros = new Dictionary<string, object>
            {
                { "@id_fase", id_fase }
            };

            var resultado = db.LeerConParametros(consulta, parametros);

            if (resultado.Count > 0)
            {
                var fila = resultado[0] as ObservableCollection<object>;
                nombreFase = fila[0].ToString();
            }

            return nombreFase;
        }

        #endregion
    }
}