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

        public bool InsertarAlumnoConEmpresa(Alumno alumno, int id_empresa)
        {
            db = DBBroker.ObtenerAgente();
            bool resultado = false;

            try
            {
                // 1. Insertar el alumno
                string consultaAlumno = @"INSERT INTO fpc.Alumnos (nombre, apellidos, email, first_char, bgColor, activo, id_curso, id_convocatoria, id_fase) 
                            VALUES (@nombre, @apellidos, @email, @first_char, @bgColor, @activo, @id_curso, @id_convocatoria, @id_fase)";

                Dictionary<string, object> parametrosAlumno = new Dictionary<string, object>
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

                int filasAfectadas = db.Modificar(consultaAlumno, parametrosAlumno);

                if (filasAfectadas > 0)
                {
                    // 2. Obtener el ID del alumno recién insertado
                    int id_alumno = db.LeerUltimoIdInsertado();

                    // 3. Obtener los años académicos del curso
                    string consultaCurso = @"SELECT anio_inicio, anio_fin FROM fpc.Cursos WHERE id_curso = @id_curso";
                    Dictionary<string, object> parametrosCurso = new Dictionary<string, object>
                    {
                        { "@id_curso", alumno.id_curso }
                    };

                    var resultadoCurso = db.LeerConParametros(consultaCurso, parametrosCurso);

                    if (resultadoCurso.Count > 0)
                    {
                        var filaCurso = resultadoCurso[0] as ObservableCollection<object>;
                        int anio_inicio = Convert.ToInt32(filaCurso[0]);
                        int anio_fin = Convert.ToInt32(filaCurso[1]);

                        // 4. Insertar en el historial de cursos
                        string consultaHistorico = @"INSERT INTO fpc.HistoricoAlumnoCurso 
                                        (id_alumno, id_curso, anio_academico_inicio, anio_academico_fin) 
                                        VALUES (@id_alumno, @id_curso, @anio_inicio, @anio_fin)";

                        Dictionary<string, object> parametrosHistorico = new Dictionary<string, object>
                        {
                            { "@id_alumno", id_alumno },
                            { "@id_curso", alumno.id_curso },
                            { "@anio_inicio", anio_inicio },
                            { "@anio_fin", anio_fin }
                        };

                        int filasHistorico = db.Modificar(consultaHistorico, parametrosHistorico);

                        // 5. Insertar en la tabla de asignación de empresas
                        string consultaAsignacion = @"INSERT INTO fpc.AsignacionEmpresas 
                                        (id_alumno, id_empresa, id_fase, fecha_asignacion) 
                                        VALUES (@id_alumno, @id_empresa, @id_fase, @fecha_asignacion)";

                        Dictionary<string, object> parametrosAsignacion = new Dictionary<string, object>
                        {
                            { "@id_alumno", id_alumno },
                            { "@id_empresa", id_empresa },
                            { "@id_fase", alumno.id_fase },
                            { "@fecha_asignacion", DateTime.Now.Date }
                        };

                        int filasAsignacion = db.Modificar(consultaAsignacion, parametrosAsignacion);

                        // La operación es exitosa si se insertaron correctamente el historial y la asignación
                        resultado = filasHistorico > 0 && filasAsignacion > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al insertar alumno con empresa: {ex.Message}");
                resultado = false;
            }

            return resultado;
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
        /// Actualiza la fase de asignación para un alumno en la tabla AsignacionEmpresas
        /// </summary>
        /// <param name="id_alumno">ID del alumno</param>
        /// <param name="id_fase">Nuevo ID de fase</param>
        /// <returns>True si la actualización fue exitosa, false en caso contrario</returns>
        public bool ActualizarFaseAsignacionEmpresa(int id_alumno, int id_fase)
        {
            db = DBBroker.ObtenerAgente();
            bool resultado = false;

            try
            {
                // Verificamos si existe una asignación para este alumno
                string consultaExiste = @"SELECT COUNT(*) 
                                FROM fpc.AsignacionEmpresas 
                                WHERE id_alumno = @id_alumno";

                Dictionary<string, object> paramsExiste = new Dictionary<string, object>
                {
                    { "@id_alumno", id_alumno }
                };

                var resultadoExiste = db.LeerConParametros(consultaExiste, paramsExiste);
                int cantidadAsignaciones = 0;

                if (resultadoExiste.Count > 0)
                {
                    var fila = resultadoExiste[0] as ObservableCollection<object>;
                    cantidadAsignaciones = Convert.ToInt32(fila[0]);
                }

                if (cantidadAsignaciones > 0)
                {
                    // Actualizamos la fase y la fecha de asignación
                    string consultaUpdate = @"UPDATE fpc.AsignacionEmpresas 
                                    SET id_fase = @id_fase,
                                        fecha_asignacion = CURDATE()
                                    WHERE id_alumno = @id_alumno";

                    Dictionary<string, object> parametros = new Dictionary<string, object>
                    {
                        { "@id_alumno", id_alumno },
                        { "@id_fase", id_fase }
                    };

                    int filasModificadas = db.Modificar(consultaUpdate, parametros);
                    resultado = filasModificadas > 0;

                    // También actualizamos la fase en la tabla Alumnos para mantener consistencia
                    string consultaAlumno = @"UPDATE fpc.Alumnos 
                                    SET id_fase = @id_fase
                                    WHERE id_alumno = @id_alumno";

                    db.Modificar(consultaAlumno, parametros);

                    Console.WriteLine($"Se actualizaron {filasModificadas} asignaciones para el alumno {id_alumno}");
                }
                else
                {
                    Console.WriteLine($"El alumno {id_alumno} no tiene asignaciones de empresa");
                    resultado = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar fase de asignación: {ex.Message}");
                resultado = false;
            }

            return resultado;
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
        /// Obtiene el número de alumnos asociados a un centro específico
        /// </summary>
        /// <param name="id_centro">ID del centro</param>
        /// <returns>Número de alumnos</returns>
        public int ContarAlumnosPorCentro(int id_centro)
        {
            int numeroAlumnos = 0;

            try
            {
                db = DBBroker.ObtenerAgente();

                string query = @"SELECT COUNT(DISTINCT a.id_alumno) 
                        FROM fpc.Alumnos a
                        INNER JOIN fpc.Cursos c ON a.id_curso = c.id_curso
                        INNER JOIN fpc.Perfiles p ON c.id_perfil = p.id_perfil
                        INNER JOIN fpc.Grados g ON p.id_grado = g.id_grado
                        WHERE g.id_centro = @id_centro
                        AND a.activo = 1";

                var parametros = new Dictionary<string, object>
                {
                    { "@id_centro", id_centro }
                };

                var resultado = db.LeerConParametros(query, parametros);

                if (resultado.Count > 0)
                {
                    var fila = resultado[0] as ObservableCollection<object>;
                    numeroAlumnos = Convert.ToInt32(fila[0]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al contar alumnos por centro: {ex.Message}");
            }

            return numeroAlumnos;
        }

        /// <summary>
        /// Obtiene la información completa de un alumno, incluyendo su historial académico y asignación de empresa
        /// </summary>
        /// <param name="id_alumno">ID del alumno a consultar</param>
        /// <returns>Objeto Alumno con datos de empresa e historial</returns>
        public Alumno LeerAlumnoConEmpresaHistorico(int id_alumno)
        {
            Alumno alumno = null;
            db = DBBroker.ObtenerAgente();

            try
            {
                string consulta = @"SELECT a.id_alumno, a.nombre, a.apellidos, a.email, a.first_char, 
                          a.bgColor, a.activo, a.id_curso, a.id_convocatoria, a.id_fase,
                          c.nivel AS nombre_curso, c.anio_inicio, c.anio_fin,
                          h.anio_academico_inicio, h.anio_academico_fin,
                          e.id_empresa, e.nombre AS nombre_empresa, ae.fecha_asignacion
                          FROM fpc.Alumnos a
                          INNER JOIN fpc.Cursos c ON a.id_curso = c.id_curso
                          LEFT JOIN fpc.HistoricoAlumnoCurso h ON a.id_alumno = h.id_alumno
                          LEFT JOIN fpc.AsignacionEmpresas ae ON a.id_alumno = ae.id_alumno
                          LEFT JOIN fpc.Empresas e ON ae.id_empresa = e.id_empresa
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

                    // Guarda información adicional que puedes necesitar
                    alumno.InfoAdicional = new Dictionary<string, object>();

                    // Información del curso y años académicos
                    alumno.InfoAdicional["nombre_curso"] = fila[10]?.ToString();
                    alumno.InfoAdicional["anio_inicio_curso"] = fila[11]?.ToString();
                    alumno.InfoAdicional["anio_fin_curso"] = fila[12]?.ToString();

                    // Información del historial académico
                    alumno.InfoAdicional["anio_academico_inicio"] = fila[13]?.ToString();
                    alumno.InfoAdicional["anio_academico_fin"] = fila[14]?.ToString();

                    // Información de la empresa asignada
                    alumno.InfoAdicional["id_empresa"] = fila[15]?.ToString();
                    alumno.InfoAdicional["nombre_empresa"] = fila[16]?.ToString();
                    alumno.InfoAdicional["fecha_asignacion"] = fila[17]?.ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer alumno con empresa e historial: {ex.Message}");
            }

            return alumno;
        }

        /// <summary>
        /// Obtiene todos los alumnos con sus asignaciones de empresa
        /// </summary>
        /// <returns>Colección de alumnos con información de empresa</returns>
        public ObservableCollection<Alumno> LeerAlumnosConEmpresa()
        {
            ObservableCollection<Alumno> alumnos = new ObservableCollection<Alumno>();
            db = DBBroker.ObtenerAgente();

            try
            {
                string consulta = @"SELECT a.id_alumno, a.nombre, a.apellidos, a.email, a.first_char, 
                          a.bgColor, a.activo, a.id_curso, a.id_convocatoria, a.id_fase,
                          e.id_empresa, e.nombre AS nombre_empresa, ae.fecha_asignacion
                          FROM fpc.Alumnos a
                          LEFT JOIN fpc.AsignacionEmpresas ae ON a.id_alumno = ae.id_alumno
                          LEFT JOIN fpc.Empresas e ON ae.id_empresa = e.id_empresa
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

                    // Información de la empresa asignada
                    alumno.InfoAdicional = new Dictionary<string, object>();
                    alumno.InfoAdicional["id_empresa"] = fila[10]?.ToString();
                    alumno.InfoAdicional["nombre_empresa"] = fila[11]?.ToString();
                    alumno.InfoAdicional["fecha_asignacion"] = fila[12]?.ToString();

                    alumnos.Add(alumno);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer alumnos con empresa: {ex.Message}");
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
        /// Obtiene alumnos por ID de curso, convocatoria y fase, incluyendo información de empresa asignada
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
              c.nivel AS nombre_curso,
              e.id_empresa, e.nombre AS nombre_empresa
              FROM fpc.Alumnos a
              INNER JOIN fpc.Cursos c ON a.id_curso = c.id_curso
              LEFT JOIN fpc.AsignacionEmpresas ae ON a.id_alumno = ae.id_alumno AND ae.id_fase = a.id_fase
              LEFT JOIN fpc.Empresas e ON ae.id_empresa = e.id_empresa
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

                // Initialize InfoAdicional dictionary if not already done in constructor
                if (alumno.InfoAdicional == null)
                    alumno.InfoAdicional = new Dictionary<string, object>();

                // Add company information to the InfoAdicional dictionary
                alumno.InfoAdicional["id_empresa"] = fila[11]?.ToString();
                alumno.InfoAdicional["nombre_empresa"] = fila[12]?.ToString();

                alumnos.Add(alumno);
            }

            return alumnos;
        }

        /// <summary>
        /// Obtiene alumnos por ID de curso y fase, incluyendo información de empresa asignada
        /// </summary>
        /// <param name="id_curso">ID del curso</param>
        /// <param name="id_fase">ID de la fase</param>
        /// <returns>Colección observable de objetos Alumno que cumplen con los criterios</returns>
        /// <summary>
        /// Obtiene alumnos por ID de curso y fase, incluyendo información de empresa asignada
        /// </summary>
        /// <param name="id_curso">ID del curso</param>
        /// <param name="id_fase">ID de la fase</param>
        /// <returns>Colección observable de objetos Alumno que cumplen con los criterios</returns>
        public ObservableCollection<Alumno> ObtenerAlumnosPorCursoYFase(int id_curso, int id_fase)
        {
            ObservableCollection<Alumno> alumnos = new ObservableCollection<Alumno>();
            db = DBBroker.ObtenerAgente();

            string consulta = @"SELECT a.id_alumno, a.nombre, a.apellidos, a.email, a.first_char, 
              a.bgColor, a.activo, a.id_curso, a.id_convocatoria, a.id_fase,
              c.nivel AS nombre_curso,
              e.id_empresa, e.nombre AS nombre_empresa
              FROM fpc.Alumnos a
              INNER JOIN fpc.Cursos c ON a.id_curso = c.id_curso
              LEFT JOIN fpc.AsignacionEmpresas ae ON a.id_alumno = ae.id_alumno AND ae.id_fase = a.id_fase
              LEFT JOIN fpc.Empresas e ON ae.id_empresa = e.id_empresa
              WHERE a.id_curso = @id_curso 
              AND a.id_fase = @id_fase 
              AND a.activo = 1";

            Dictionary<string, object> parametros = new Dictionary<string, object>
            {
                { "@id_curso", id_curso },
                { "@id_fase", id_fase }
            };

            var resultado = db.LeerConParametros(consulta, parametros);

            Console.WriteLine($"Filas devueltas: {resultado.Count}");

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

                // Initialize InfoAdicional dictionary if not already done in constructor
                if (alumno.InfoAdicional == null)
                    alumno.InfoAdicional = new Dictionary<string, object>();

                // Add company information to the InfoAdicional dictionary with null checks
                alumno.InfoAdicional["id_empresa"] = fila.Count > 11 && fila[11] != null ? fila[11].ToString() : null;
                alumno.InfoAdicional["nombre_empresa"] = fila.Count > 12 && fila[12] != null ? fila[12].ToString() : null;

                Console.WriteLine($"Alumno: {alumno.nombre}, Empresa: {alumno.InfoAdicional["nombre_empresa"] ?? "No asignada"}");

                alumnos.Add(alumno);
            }

            return alumnos;
        }

        /// <summary>
        /// Obtiene el historial académico de alumnos filtrado por perfil y año de inicio
        /// </summary>
        /// <param name="id_perfil">ID del perfil profesional</param>
        /// <param name="anio_inicio">Año académico de inicio</param>
        /// <returns>Colección de alumnos con su información histórica</returns>
        public ObservableCollection<Alumno> ObtenerHistoricoAlumnosPorPerfilYAnio(int id_perfil, int anio_inicio)
        {
            ObservableCollection<Alumno> alumnos = new ObservableCollection<Alumno>();
            db = DBBroker.ObtenerAgente();

            string consulta = @"SELECT a.id_alumno, a.nombre, a.apellidos, a.email, a.first_char, 
                       a.bgColor, a.activo, a.id_curso, a.id_convocatoria, a.id_fase,
                       c.nivel AS nombre_curso, c.anio_inicio, c.anio_fin,
                       h.id_historico, h.anio_academico_inicio, h.anio_academico_fin,
                       p.id_perfil, p.nombre AS nombre_perfil,
                       e.id_empresa, e.nombre AS nombre_empresa
                       FROM fpc.HistoricoAlumnoCurso h
                       INNER JOIN fpc.Alumnos a ON h.id_alumno = a.id_alumno
                       INNER JOIN fpc.Cursos c ON h.id_curso = c.id_curso
                       INNER JOIN fpc.Perfiles p ON c.id_perfil = p.id_perfil
                       LEFT JOIN fpc.AsignacionEmpresas ae ON a.id_alumno = ae.id_alumno
                       LEFT JOIN fpc.Empresas e ON ae.id_empresa = e.id_empresa
                       WHERE p.id_perfil = @id_perfil 
                       AND h.anio_academico_inicio = @anio_inicio";

            Dictionary<string, object> parametros = new Dictionary<string, object>
            {
                { "@id_perfil", id_perfil },
                { "@anio_inicio", anio_inicio }
            };

            var resultado = db.LeerConParametros(consulta, parametros);

            Console.WriteLine($"Registros encontrados: {resultado.Count}");

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

                // Initialize InfoAdicional dictionary if not already done in constructor
                if (alumno.InfoAdicional == null)
                    alumno.InfoAdicional = new Dictionary<string, object>();

                // Añadir información del curso
                alumno.InfoAdicional["nombre_curso"] = fila[10]?.ToString();
                alumno.InfoAdicional["anio_inicio_curso"] = fila[11]?.ToString();
                alumno.InfoAdicional["anio_fin_curso"] = fila[12]?.ToString();

                // Añadir información del historial académico
                alumno.InfoAdicional["id_historico"] = fila[13]?.ToString();
                alumno.InfoAdicional["anio_academico_inicio"] = fila[14]?.ToString();
                alumno.InfoAdicional["anio_academico_fin"] = fila[15]?.ToString();

                // Añadir información del perfil
                alumno.InfoAdicional["id_perfil"] = fila[16]?.ToString();
                alumno.InfoAdicional["nombre_perfil"] = fila[17]?.ToString();

                // Añadir información de empresa si existe
                alumno.InfoAdicional["id_empresa"] = fila[18]?.ToString();
                alumno.InfoAdicional["nombre_empresa"] = fila[19]?.ToString();

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