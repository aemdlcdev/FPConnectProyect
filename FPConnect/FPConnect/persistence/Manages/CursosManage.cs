using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPConnect.domain;

namespace FPConnect.persistence.Manages
{
    public class CursosManage
    {
        private DBBroker db;

        // Método para insertar un curso
        public bool InsertarCurso(Curso curso)
        {
            db = DBBroker.ObtenerAgente();

            string query = @"INSERT INTO fpc.cursos 
                           (id_perfil, nivel, anio_inicio, anio_fin, activo) 
                           VALUES 
                           (@id_perfil, @nivel, @anio_inicio, @anio_fin, @activo);";

            var parametros = new Dictionary<string, object>
            {
                { "@id_perfil", curso.id_perfil },
                { "@nivel", curso.nivel },
                { "@anio_inicio", curso.anio_inicio },
                { "@anio_fin", curso.anio_fin },
                { "@activo", curso.activo }
            };

            int resultado = db.Modificar(query, parametros);
            return resultado > 0;
        }

        // Método para actualizar un curso
        public bool ActualizarCurso(Curso curso)
        {
            db = DBBroker.ObtenerAgente();

            string query = @"UPDATE fpc.cursos 
                           SET id_perfil = @id_perfil, 
                               nivel = @nivel, 
                               anio_inicio = @anio_inicio, 
                               anio_fin = @anio_fin, 
                               activo = @activo 
                           WHERE id_curso = @id_curso;";

            var parametros = new Dictionary<string, object>
            {
                { "@id_curso", curso.id_curso },
                { "@id_perfil", curso.id_perfil },
                { "@nivel", curso.nivel },
                { "@anio_inicio", curso.anio_inicio },
                { "@anio_fin", curso.anio_fin },
                { "@activo", curso.activo }
            };

            int resultado = db.Modificar(query, parametros);
            return resultado > 0;
        }

        // Método para eliminado físico
        public bool EliminarCurso(int id_curso)
        {
            db = DBBroker.ObtenerAgente();

            // IMPORTANTE: Verificar que no haya relaciones antes de eliminar
            // Este curso podría estar relacionado con profesores y alumnos

            string query = "DELETE FROM fpc.cursos WHERE id_curso = @id_curso;";

            var parametros = new Dictionary<string, object>
            {
                { "@id_curso", id_curso }
            };

            int resultado = db.Modificar(query, parametros);
            return resultado > 0;
        }

        // Método para eliminado lógico
        public bool EliminadoLogicoCurso(int id_curso)
        {
            db = DBBroker.ObtenerAgente();

            string query = "UPDATE fpc.cursos SET activo = 2 WHERE id_curso = @id_curso;";

            var parametros = new Dictionary<string, object>
            {
                { "@id_curso", id_curso }
            };

            int resultado = db.Modificar(query, parametros);
            return resultado > 0;
        }

        // Método para leer todos los cursos activos
        public ObservableCollection<Curso> LeerCursos()
        {
            ObservableCollection<Curso> cursos = new ObservableCollection<Curso>();
            db = DBBroker.ObtenerAgente();

            string query = @"SELECT c.id_curso, c.id_perfil, c.nivel, c.anio_inicio, c.anio_fin, c.activo, p.nombre AS nombre_perfil
                           FROM fpc.cursos c
                           INNER JOIN fpc.perfiles p ON c.id_perfil = p.id_perfil
                           WHERE c.activo = 1
                           ORDER BY c.anio_inicio DESC, p.nombre, c.nivel;";

            var resultado = db.LeerSinParametros(query);

            foreach (ObservableCollection<object> fila in resultado)
            {
                Curso curso = new Curso(
                    int.Parse(fila[0].ToString()),      // id_curso
                    int.Parse(fila[1].ToString()),      // id_perfil
                    fila[2].ToString(),                 // nivel
                    int.Parse(fila[3].ToString()),      // anio_inicio
                    int.Parse(fila[4].ToString()),      // anio_fin
                    int.Parse(fila[5].ToString()),      // activo
                    fila[6].ToString()                  // nombre_perfil
                );

                cursos.Add(curso);
            }

            return cursos;
        }

        // Método para leer cursos por perfil
        public ObservableCollection<Curso> LeerCursosPorPerfil(int id_perfil)
        {
            ObservableCollection<Curso> cursos = new ObservableCollection<Curso>();
            db = DBBroker.ObtenerAgente();

            string query = @"SELECT c.id_curso, c.id_perfil, c.nivel, c.anio_inicio, c.anio_fin, c.activo, p.nombre AS nombre_perfil
                           FROM fpc.cursos c
                           INNER JOIN fpc.perfiles p ON c.id_perfil = p.id_perfil
                           WHERE c.id_perfil = @id_perfil AND c.activo = 1
                           ORDER BY c.anio_inicio DESC, c.nivel;";

            var parametros = new Dictionary<string, object>
            {
                { "@id_perfil", id_perfil }
            };

            var resultado = db.LeerConParametros(query, parametros);

            foreach (ObservableCollection<object> fila in resultado)
            {
                Curso curso = new Curso(
                    int.Parse(fila[0].ToString()),      // id_curso
                    int.Parse(fila[1].ToString()),      // id_perfil
                    fila[2].ToString(),                 // nivel
                    int.Parse(fila[3].ToString()),      // anio_inicio
                    int.Parse(fila[4].ToString()),      // anio_fin
                    int.Parse(fila[5].ToString()),      // activo
                    fila[6].ToString()                  // nombre_perfil
                );

                cursos.Add(curso);
            }

            return cursos;
        }

        // Método para leer cursos del año académico actual (útil para filtrados)
        public ObservableCollection<Curso> LeerCursosAnioActual()
        {
            ObservableCollection<Curso> cursos = new ObservableCollection<Curso>();
            db = DBBroker.ObtenerAgente();

            int anioActual = DateTime.Now.Year;

            string query = @"SELECT c.id_curso, c.id_perfil, c.nivel, c.anio_inicio, c.anio_fin, c.activo, p.nombre AS nombre_perfil
                           FROM fpc.cursos c
                           INNER JOIN fpc.perfiles p ON c.id_perfil = p.id_perfil
                           WHERE c.anio_inicio = @anio_actual AND c.activo = 1
                           ORDER BY p.nombre, c.nivel;";

            var parametros = new Dictionary<string, object>
            {
                { "@anio_actual", anioActual }
            };

            var resultado = db.LeerConParametros(query, parametros);

            foreach (ObservableCollection<object> fila in resultado)
            {
                Curso curso = new Curso(
                    int.Parse(fila[0].ToString()),      // id_curso
                    int.Parse(fila[1].ToString()),      // id_perfil
                    fila[2].ToString(),                 // nivel
                    int.Parse(fila[3].ToString()),      // anio_inicio
                    int.Parse(fila[4].ToString()),      // anio_fin
                    int.Parse(fila[5].ToString()),      // activo
                    fila[6].ToString()                  // nombre_perfil
                );

                cursos.Add(curso);
            }

            return cursos;
        }

        // Método para leer un curso por ID
        public Curso LeerCursoPorId(int id_curso)
        {
            Curso curso = null;
            db = DBBroker.ObtenerAgente();

            string query = @"SELECT c.id_curso, c.id_perfil, c.nivel, c.anio_inicio, c.anio_fin, c.activo, p.nombre AS nombre_perfil
                           FROM fpc.cursos c
                           INNER JOIN fpc.perfiles p ON c.id_perfil = p.id_perfil
                           WHERE c.id_curso = @id_curso;";

            var parametros = new Dictionary<string, object>
            {
                { "@id_curso", id_curso }
            };

            var resultado = db.LeerConParametros(query, parametros);

            if (resultado.Count > 0)
            {
                var fila = resultado[0] as ObservableCollection<object>;

                curso = new Curso(
                    int.Parse(fila[0].ToString()),      // id_curso
                    int.Parse(fila[1].ToString()),      // id_perfil
                    fila[2].ToString(),                 // nivel
                    int.Parse(fila[3].ToString()),      // anio_inicio
                    int.Parse(fila[4].ToString()),      // anio_fin
                    int.Parse(fila[5].ToString()),      // activo
                    fila[6].ToString()                  // nombre_perfil
                );
            }

            return curso;
        }

        // Método para leer un curso por centro
        public ObservableCollection<Curso> LeerCursosPorCentro(int id_centro)
        {
            ObservableCollection<Curso> cursos = new ObservableCollection<Curso>();
            db = DBBroker.ObtenerAgente();

            string query = @"SELECT c.id_curso, c.id_perfil, c.nivel, c.anio_inicio, c.anio_fin, c.activo, 
                           p.nombre AS nombre_perfil
                    FROM fpc.cursos c
                    INNER JOIN fpc.perfiles p ON c.id_perfil = p.id_perfil
                    INNER JOIN fpc.grados g ON p.id_grado = g.id_grado
                    INNER JOIN fpc.familiasprofesionales f ON p.id_familia = f.id_familia
                    WHERE g.id_centro = @id_centro 
                      AND f.id_centro = @id_centro
                      AND c.activo = 1
                      AND p.activo = 1
                    ORDER BY c.anio_inicio DESC, p.nombre, c.nivel;";

            var parametros = new Dictionary<string, object>
            {
                { "@id_centro", id_centro }
            };

            var resultado = db.LeerConParametros(query, parametros);

            foreach (ObservableCollection<object> fila in resultado)
            {
                Curso curso = new Curso(
                    int.Parse(fila[0].ToString()),      // id_curso
                    int.Parse(fila[1].ToString()),      // id_perfil
                    fila[2].ToString(),                 // nivel
                    int.Parse(fila[3].ToString()),      // anio_inicio
                    int.Parse(fila[4].ToString()),      // anio_fin
                    int.Parse(fila[5].ToString()),      // activo
                    fila[6].ToString()                  // nombre_perfil
                );

                cursos.Add(curso);
            }

            return cursos;
        }

        public ObservableCollection<Curso> LeerCursosPorFamilia(int id_familia)
        {
            ObservableCollection<Curso> cursos = new ObservableCollection<Curso>();
            db = DBBroker.ObtenerAgente();

            try
            {
                string query = @"SELECT c.id_curso, c.id_perfil, c.nivel, c.anio_inicio, c.anio_fin, c.activo, 
                              p.nombre AS nombre_perfil
                        FROM fpc.cursos c
                        INNER JOIN fpc.perfiles p ON c.id_perfil = p.id_perfil
                        WHERE p.id_familia = @id_familia 
                          AND c.activo = 1
                          AND p.activo = 1
                        ORDER BY c.anio_inicio DESC, c.nivel;";

                var parametros = new Dictionary<string, object>
                {
                    { "@id_familia", id_familia }
                };

                Console.WriteLine($"Buscando cursos para familia con ID: {id_familia}");
                var resultado = db.LeerConParametros(query, parametros);
                Console.WriteLine($"La consulta devolvió {resultado.Count} filas");

                foreach (ObservableCollection<object> fila in resultado)
                {
                    try
                    {
                        Curso curso = new Curso(
                            int.Parse(fila[0].ToString()),      // id_curso
                            int.Parse(fila[1].ToString()),      // id_perfil
                            fila[2].ToString(),                 // nivel
                            int.Parse(fila[3].ToString()),      // anio_inicio
                            int.Parse(fila[4].ToString()),      // anio_fin
                            int.Parse(fila[5].ToString()),      // activo
                            fila[6].ToString()                  // nombre_perfil
                        );

                        cursos.Add(curso);
                        Console.WriteLine($"Curso añadido: {curso.nivel} ({curso.anio_inicio}-{curso.anio_fin})");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error procesando fila: {ex.Message}");
                        // Inspeccionar contenido de la fila para depuración
                        for (int i = 0; i < fila.Count; i++)
                        {
                            Console.WriteLine($"Columna {i}: {fila[i]?.ToString() ?? "NULL"}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error general: {ex.Message}");
            }

            return cursos;
        }

    }
}
