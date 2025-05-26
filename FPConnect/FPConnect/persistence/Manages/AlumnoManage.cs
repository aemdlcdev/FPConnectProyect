using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Media;
using FPConnect.domain;
using FPConnect.HelperClasses;

namespace FPConnect.persistence.Manages
{
    class AlumnoManage
    {
        private ObservableCollection<Alumno> listaAlumnos { get; set; }
        private DBBroker db;

        public AlumnoManage()
        {
            listaAlumnos = new ObservableCollection<Alumno>();
        }

        // Método para insertar un alumno
        public bool InsertarAlumno(Alumno alumno)
        {
            db = DBBroker.ObtenerAgente();

            string query = @"INSERT INTO fpc.alumnos 
                          (nombre, apellidos,email, first_char, bgColor, activo, 
                          id_curso, id_convocatoria, id_fase) 
                          VALUES 
                          (@nombre, @apellidos, @first_char, @bgColor, @activo, 
                          @id_curso, @id_convocatoria, @id_fase);";

            var parametros = new Dictionary<string, object>
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

            int resultado = db.Modificar(query, parametros);
            return resultado > 0;
        }

        // Método para actualizar un alumno
        public bool ActualizarAlumno(Alumno alumno)
        {
            db = DBBroker.ObtenerAgente();

            string query = @"UPDATE fpc.alumnos 
                          SET nombre = @nombre, 
                              apellidos = @apellidos,                              
                              email = @email,  
                              first_char = @first_char, 
                              bgColor = @bgColor, 
                              activo = @activo, 
                              id_curso = @id_curso, 
                              id_convocatoria = @id_convocatoria, 
                              id_fase = @id_fase 
                          WHERE id_alumno = @id_alumno;";

            var parametros = new Dictionary<string, object>
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

            int resultado = db.Modificar(query, parametros);
            return resultado > 0;
        }

        // Método para eliminado lógico
        public bool EliminadoLogicoAlumno(int id_alumno)
        {
            db = DBBroker.ObtenerAgente();

            string query = "UPDATE fpc.alumnos SET activo = 2 WHERE id_alumno = @id_alumno;";

            var parametros = new Dictionary<string, object>
            {
                { "@id_alumno", id_alumno }
            };

            int resultado = db.Modificar(query, parametros);
            return resultado > 0;
        }

        // Método para leer todos los alumnos activos
        public ObservableCollection<Alumno> LeerAlumnos()
        {
            ObservableCollection<Alumno> alumnos = new ObservableCollection<Alumno>();
            db = DBBroker.ObtenerAgente();

            string query = @"SELECT a.id_alumno, a.nombre, a.apellidos,a.email, a.first_char, a.bgColor, 
                           a.activo, a.id_curso, a.id_convocatoria, a.id_fase,
                           c.nivel AS nombre_curso
                           FROM fpc.alumnos a
                           INNER JOIN fpc.cursos c ON a.id_curso = c.id_curso
                           WHERE a.activo = 1;";

            var resultado = db.LeerSinParametros(query);

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

        // Método para leer alumnos por curso
        public ObservableCollection<Alumno> LeerAlumnosPorCurso(int id_curso)
        {
            ObservableCollection<Alumno> alumnos = new ObservableCollection<Alumno>();
            db = DBBroker.ObtenerAgente();

            string query = @"SELECT a.id_alumno, a.nombre, a.apellidos,a.email, a.first_char, a.bgColor, 
                           a.activo, a.id_curso, a.id_convocatoria, a.id_fase,
                           c.nivel AS nombre_curso
                           FROM fpc.alumnos a
                           INNER JOIN fpc.cursos c ON a.id_curso = c.id_curso
                           WHERE a.id_curso = @id_curso AND a.activo = 1;";

            var parametros = new Dictionary<string, object>
            {
                { "@id_curso", id_curso }
            };

            var resultado = db.LeerConParametros(query, parametros);

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

        // Método para leer alumnos por convocatoria
        public ObservableCollection<Alumno> LeerAlumnosPorConvocatoria(int id_convocatoria)
        {
            ObservableCollection<Alumno> alumnos = new ObservableCollection<Alumno>();
            db = DBBroker.ObtenerAgente();

            string query = @"SELECT a.id_alumno, a.nombre, a.apellidos,a.email, a.first_char, a.bgColor, 
                           a.activo, a.id_curso, a.id_convocatoria, a.id_fase,
                           c.nivel AS nombre_curso
                           FROM fpc.alumnos a
                           INNER JOIN fpc.cursos c ON a.id_curso = c.id_curso
                           WHERE a.id_convocatoria = @id_convocatoria AND a.activo = 1;";

            var parametros = new Dictionary<string, object>
            {
                { "@id_convocatoria", id_convocatoria }
            };

            var resultado = db.LeerConParametros(query, parametros);

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

        // Método para leer alumnos por curso y convocatoria
        public ObservableCollection<Alumno> LeerAlumnosPorCursoYConvocatoria(int id_curso, int id_convocatoria)
        {
            ObservableCollection<Alumno> alumnos = new ObservableCollection<Alumno>();
            db = DBBroker.ObtenerAgente();

            string query = @"SELECT a.id_alumno, a.nombre, a.apellidos,a.email, a.first_char, a.bgColor, 
                   a.activo, a.id_curso, a.id_convocatoria, a.id_fase,
                   c.nivel AS nombre_curso
                   FROM fpc.alumnos a
                   INNER JOIN fpc.cursos c ON a.id_curso = c.id_curso
                   WHERE a.id_curso = @id_curso 
                   AND a.id_convocatoria = @id_convocatoria 
                   AND a.activo = 1;";

            var parametros = new Dictionary<string, object>
            {
                { "@id_curso", id_curso },
                { "@id_convocatoria", id_convocatoria }
            };

            var resultado = db.LeerConParametros(query, parametros);

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

        // Método para leer alumnos por curso, convocatoria y fase
        public ObservableCollection<Alumno> LeerAlumnosPorCursoConvocatoriaYFase(int id_curso, int id_convocatoria, int id_fase)
        {
            ObservableCollection<Alumno> alumnos = new ObservableCollection<Alumno>();
            db = DBBroker.ObtenerAgente();

            string query = @"SELECT a.id_alumno, a.nombre, a.apellidos, a.email, a.first_char, a.bgColor, 
                   a.activo, a.id_curso, a.id_convocatoria, a.id_fase
                   FROM fpc.alumnos a
                   WHERE a.id_curso = @id_curso 
                   AND a.id_convocatoria = @id_convocatoria 
                   AND a.id_fase = @id_fase
                   AND a.activo = 1;";

            var parametros = new Dictionary<string, object>
            {
                { "@id_curso", id_curso },
                { "@id_convocatoria", id_convocatoria },
                { "@id_fase", id_fase }
            };

            var resultado = db.LeerConParametros(query, parametros);

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

        // Método para leer un alumno por ID
        public Alumno LeerAlumnoPorId(int id_alumno)
        {
            Alumno alumno = null;
            db = DBBroker.ObtenerAgente();

            string query = @"SELECT a.id_alumno, a.nombre, a.apellidos,a.email, a.first_char, a.bgColor, 
                           a.activo, a.id_curso, a.id_convocatoria, a.id_fase,
                           c.nivel AS nombre_curso
                           FROM fpc.alumnos a
                           INNER JOIN fpc.cursos c ON a.id_curso = c.id_curso
                           WHERE a.id_alumno = @id_alumno;";

            var parametros = new Dictionary<string, object>
            {
                { "@id_alumno", id_alumno }
            };

            var resultado = db.LeerConParametros(query, parametros);

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

        // Método para leer alumnos por centro (a través del curso)
        public ObservableCollection<Alumno> LeerAlumnosPorCentro(int id_centro)
        {
            ObservableCollection<Alumno> alumnos = new ObservableCollection<Alumno>();
            db = DBBroker.ObtenerAgente();

            string query = @"SELECT a.id_alumno, a.nombre, a.apellidos,a.email, a.first_char, a.bgColor, 
                           a.activo, a.id_curso, a.id_convocatoria, a.id_fase,
                           c.nivel AS nombre_curso
                           FROM fpc.alumnos a
                           INNER JOIN fpc.cursos c ON a.id_curso = c.id_curso
                           INNER JOIN fpc.perfiles p ON c.id_perfil = p.id_perfil
                           INNER JOIN fpc.grados g ON p.id_grado = g.id_grado
                           WHERE g.id_centro = @id_centro AND a.activo = 1;";

            var parametros = new Dictionary<string, object>
            {
                { "@id_centro", id_centro }
            };

            var resultado = db.LeerConParametros(query, parametros);

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

        // Método para registrar un cambio de curso y guardar en el histórico (sin transacciones)
        public bool RegistrarCambioCurso(int id_alumno, int nuevo_id_curso, int anio_inicio, int anio_fin)
        {
            db = DBBroker.ObtenerAgente();
            bool resultado = false;

            try
            {
                // 1. Obtener el curso actual antes del cambio
                string queryCursoActual = "SELECT id_curso FROM fpc.alumnos WHERE id_alumno = @id_alumno;";
                var paramsCursoActual = new Dictionary<string, object>
                {
                    { "@id_alumno", id_alumno }
                };
                var resultadoCursoActual = db.LeerConParametros(queryCursoActual, paramsCursoActual);

                if (resultadoCursoActual.Count == 0)
                {
                    Console.WriteLine("No se encontró el alumno");
                    return false;
                }

                int curso_actual = int.Parse((resultadoCursoActual[0] as ObservableCollection<object>)[0].ToString());

                // 2. Insertar registro en histórico
                string queryInsertHistorico = @"INSERT INTO fpc.historicoalumnocurso 
                                              (id_alumno, id_curso, anio_academico_inicio, anio_academico_fin)
                                              VALUES 
                                              (@id_alumno, @id_curso, @anio_inicio, @anio_fin);";
                var paramsHistorico = new Dictionary<string, object>
                {
                    { "@id_alumno", id_alumno },
                    { "@id_curso", curso_actual },
                    { "@anio_inicio", anio_inicio },
                    { "@anio_fin", anio_fin }
                };

                int resultadoHistorico = db.Modificar(queryInsertHistorico, paramsHistorico);

                if (resultadoHistorico <= 0)
                {
                    Console.WriteLine("Error al insertar en histórico");
                    return false;
                }

                // 3. Actualizar el curso del alumno
                string queryUpdateCurso = "UPDATE fpc.alumnos SET id_curso = @nuevo_id_curso WHERE id_alumno = @id_alumno;";
                var paramsUpdate = new Dictionary<string, object>
                {
                    { "@id_alumno", id_alumno },
                    { "@nuevo_id_curso", nuevo_id_curso }
                };

                int resultadoUpdate = db.Modificar(queryUpdateCurso, paramsUpdate);

                if (resultadoUpdate <= 0)
                {
                    Console.WriteLine("Error al actualizar el curso del alumno");
                    return false;
                }

                resultado = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al registrar cambio de curso: {ex.Message}");
                resultado = false;
            }

            return resultado;
        }

        // Método para obtener el historial académico de un alumno
        public ObservableCollection<dynamic> ObtenerHistorialAcademico(int id_alumno)
        {
            ObservableCollection<dynamic> historico = new ObservableCollection<dynamic>();
            db = DBBroker.ObtenerAgente();

            string query = @"SELECT h.id_historico, h.id_alumno, h.id_curso, h.anio_academico_inicio, 
                            h.anio_academico_fin, c.nivel AS nombre_curso, p.nombre AS nombre_perfil
                            FROM fpc.historicoalumnocurso h
                            JOIN fpc.cursos c ON h.id_curso = c.id_curso
                            JOIN fpc.perfiles p ON c.id_perfil = p.id_perfil
                            WHERE h.id_alumno = @id_alumno
                            ORDER BY h.anio_academico_inicio DESC;";

            var parametros = new Dictionary<string, object>
            {
                { "@id_alumno", id_alumno }
            };

            var resultado = db.LeerConParametros(query, parametros);

            foreach (ObservableCollection<object> fila in resultado)
            {
                // aqui voy a usar una clase anónima ya que tu DBBroker trabaja con tipos dinámicos
                var registro = new
                {
                    id_historico = int.Parse(fila[0].ToString()),
                    id_alumno = int.Parse(fila[1].ToString()),
                    id_curso = int.Parse(fila[2].ToString()),
                    anio_academico_inicio = int.Parse(fila[3].ToString()),
                    anio_academico_fin = int.Parse(fila[4].ToString()),
                    nombre_curso = fila[5].ToString(),
                    nombre_perfil = fila[6].ToString()
                };

                historico.Add(registro);
            }

            return historico;
        }

        // Método para obtener el nombre del curso
        public string ObtenerNombreCurso(int id_curso)
        {
            db = DBBroker.ObtenerAgente();
            string nombreCurso = "No encontrado";

            string query = "SELECT nivel FROM fpc.cursos WHERE id_curso = @id_curso;";

            var parametros = new Dictionary<string, object>
            {
                { "@id_curso", id_curso }
            };

            var resultado = db.LeerConParametros(query, parametros);

            if (resultado.Count > 0)
            {
                var fila = resultado[0] as ObservableCollection<object>;
                nombreCurso = fila[0].ToString();
            }

            return nombreCurso;
        }

        // Método para obtener el nombre de la fase
        public string ObtenerNombreFase(int id_fase)
        {
            db = DBBroker.ObtenerAgente();
            string nombreFase = "No encontrado";

            string query = "SELECT nombre FROM fpc.fasesasignacion WHERE id_fase = @id_fase;";

            var parametros = new Dictionary<string, object>
            {
                { "@id_fase", id_fase }
            };

            var resultado = db.LeerConParametros(query, parametros);

            if (resultado.Count > 0)
            {
                var fila = resultado[0] as ObservableCollection<object>;
                nombreFase = fila[0].ToString();
            }

            return nombreFase;
        }

        // Método para obtener información de la convocatoria
        public dynamic ObtenerInfoConvocatoria(int id_convocatoria)
        {
            db = DBBroker.ObtenerAgente();
            dynamic infoConvocatoria = null;

            string query = @"SELECT c.id_convocatoria, c.fecha_inicio, c.fecha_fin, t.nombre AS tipo_fase
                           FROM fpc.convocatorias c
                           INNER JOIN fpc.tiposfase t ON c.id_tipo_fase = t.id_tipo_fase
                           WHERE c.id_convocatoria = @id_convocatoria;";

            var parametros = new Dictionary<string, object>
            {
                { "@id_convocatoria", id_convocatoria }
            };

            var resultado = db.LeerConParametros(query, parametros);

            if (resultado.Count > 0)
            {
                var fila = resultado[0] as ObservableCollection<object>;

                infoConvocatoria = new
                {
                    id_convocatoria = int.Parse(fila[0].ToString()),
                    fecha_inicio = DateTime.Parse(fila[1].ToString()),
                    fecha_fin = DateTime.Parse(fila[2].ToString()),
                    tipo_fase = fila[3].ToString()
                };
            }

            return infoConvocatoria;
        }

        // Método para obtener información del perfil a través del curso
        public dynamic ObtenerPerfilPorCurso(int id_curso)
        {
            db = DBBroker.ObtenerAgente();
            dynamic infoPerfil = null;

            string query = @"SELECT p.id_perfil, p.nombre AS nombre_perfil, f.nombre AS nombre_familia, g.nombre AS nombre_grado
                           FROM fpc.perfiles p
                           INNER JOIN fpc.cursos c ON p.id_perfil = c.id_perfil
                           INNER JOIN fpc.familiasProfesionales f ON p.id_familia = f.id_familia
                           INNER JOIN fpc.grados g ON p.id_grado = g.id_grado
                           WHERE c.id_curso = @id_curso;";

            var parametros = new Dictionary<string, object>
            {
                { "@id_curso", id_curso }
            };

            var resultado = db.LeerConParametros(query, parametros);

            if (resultado.Count > 0)
            {
                var fila = resultado[0] as ObservableCollection<object>;

                infoPerfil = new
                {
                    id_perfil = int.Parse(fila[0].ToString()),
                    nombre_perfil = fila[1].ToString(),
                    nombre_familia = fila[2].ToString(),
                    nombre_grado = fila[3].ToString()
                };
            }

            return infoPerfil;
        }
    }
}