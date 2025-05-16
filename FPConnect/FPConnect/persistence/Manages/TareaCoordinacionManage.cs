using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPConnect.domain;
using FPConnect.persistence;

namespace FPConnect.persistence.Manages
{
    public class TareaCoordinacionManage
    {
        private DBBroker db;

        public TareaCoordinacionManage()
        {
            db = DBBroker.ObtenerAgente();
        }

        #region Operaciones CRUD básicas

        // Insertar una tarea
        public bool InsertarTarea(TareaCoordinacion tarea)
        {
            try
            {
                string query = @"INSERT INTO fpc.tareascoordinacion 
                               (id_familia, id_empresa, titulo, descripcion, fecha_creacion, estado) 
                               VALUES 
                               (@id_familia, @id_empresa, @titulo, @descripcion, @fecha_creacion, @estado);";

                var parametros = new Dictionary<string, object>
                {
                    { "@id_familia", tarea.id_familia },
                    { "@id_empresa", tarea.id_empresa },
                    { "@titulo", tarea.titulo },
                    { "@descripcion", tarea.descripcion },
                    { "@fecha_creacion", tarea.fecha_creacion.ToString("yyyy-MM-dd") },
                    { "@estado", tarea.estado }
                };

                int resultado = db.Modificar(query, parametros);

                if (resultado > 0)
                {
                    // Obtener el ID de la tarea recién insertada
                    tarea.id_tarea = db.LeerUltimoIdInsertado();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al insertar tarea: {ex.Message}");
                return false;
            }
        }

        // Actualizar una tarea
        public bool ActualizarTarea(TareaCoordinacion tarea)
        {
            try
            {
                string query = @"UPDATE fpc.tareascoordinacion 
                               SET id_familia = @id_familia, 
                                   id_empresa = @id_empresa, 
                                   titulo = @titulo, 
                                   descripcion = @descripcion, 
                                   estado = @estado
                               WHERE id_tarea = @id_tarea;";

                var parametros = new Dictionary<string, object>
                {
                    { "@id_tarea", tarea.id_tarea },
                    { "@id_familia", tarea.id_familia },
                    { "@id_empresa", tarea.id_empresa },
                    { "@titulo", tarea.titulo },
                    { "@descripcion", tarea.descripcion },
                    { "@estado", tarea.estado }
                };

                int resultado = db.Modificar(query, parametros);
                return resultado > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar tarea: {ex.Message}");
                return false;
            }
        }

        // Actualizar solo el estado de una tarea
        public bool ActualizarEstadoTarea(int id_tarea, int nuevo_estado)
        {
            try
            {
                string query = "UPDATE fpc.tareascoordinacion SET estado = @estado WHERE id_tarea = @id_tarea;";

                var parametros = new Dictionary<string, object>
                {
                    { "@id_tarea", id_tarea },
                    { "@estado", nuevo_estado }
                };

                int resultado = db.Modificar(query, parametros);
                return resultado > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar estado de tarea: {ex.Message}");
                return false;
            }
        }

        // Eliminar una tarea
        public bool EliminarTarea(int id_tarea)
        {
            try
            {
                string query = "DELETE FROM fpc.tareascoordinacion WHERE id_tarea = @id_tarea;";

                var parametros = new Dictionary<string, object>
                {
                    { "@id_tarea", id_tarea }
                };

                int resultado = db.Modificar(query, parametros);
                return resultado > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar tarea: {ex.Message}");
                return false;
            }
        }

        #endregion

        #region Consultas

        // Leer todas las tareas con nombre de empresa y familia
        public ObservableCollection<TareaCoordinacion> LeerTareas()
        {
            ObservableCollection<TareaCoordinacion> tareas = new ObservableCollection<TareaCoordinacion>();

            try
            {
                string query = @"SELECT t.id_tarea, t.id_familia, t.id_empresa, t.titulo, 
                               t.descripcion, t.fecha_creacion, t.estado, 
                               f.nombre AS nombre_familia, e.nombre AS nombre_empresa
                               FROM fpc.tareascoordinacion t
                               INNER JOIN fpc.familiasprofesionales f ON t.id_familia = f.id_familia
                               INNER JOIN fpc.empresas e ON t.id_empresa = e.id_empresa
                               ORDER BY t.fecha_creacion DESC;";

                var resultado = db.LeerSinParametros(query);

                foreach (ObservableCollection<object> fila in resultado)
                {
                    TareaCoordinacion tarea = new TareaCoordinacion(
                        int.Parse(fila[0].ToString()),      // id_tarea
                        int.Parse(fila[1].ToString()),      // id_familia
                        int.Parse(fila[2].ToString()),      // id_empresa
                        fila[3].ToString(),                 // titulo
                        fila[4].ToString(),                 // descripcion
                        DateTime.Parse(fila[5].ToString()), // fecha_creacion
                        int.Parse(fila[6].ToString()),      // estado
                        fila[7].ToString(),                 // nombre_familia
                        fila[8].ToString()                  // nombre_empresa
                    );

                    tareas.Add(tarea);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer tareas: {ex.Message}");
            }

            return tareas;
        }

        // Leer tarea por ID
        public TareaCoordinacion LeerTareaPorId(int id_tarea)
        {
            try
            {
                string query = @"SELECT t.id_tarea, t.id_familia, t.id_empresa, t.titulo, 
                               t.descripcion, t.fecha_creacion, t.estado, 
                               f.nombre AS nombre_familia, e.nombre AS nombre_empresa
                               FROM fpc.tareascoordinacion t
                               INNER JOIN fpc.familiasprofesionales f ON t.id_familia = f.id_familia
                               INNER JOIN fpc.empresas e ON t.id_empresa = e.id_empresa
                               WHERE t.id_tarea = @id_tarea;";

                var parametros = new Dictionary<string, object>
                {
                    { "@id_tarea", id_tarea }
                };

                var resultado = db.LeerConParametros(query, parametros);

                if (resultado.Count > 0)
                {
                    var fila = resultado[0] as ObservableCollection<object>;

                    return new TareaCoordinacion(
                        int.Parse(fila[0].ToString()),      // id_tarea
                        int.Parse(fila[1].ToString()),      // id_familia
                        int.Parse(fila[2].ToString()),      // id_empresa
                        fila[3].ToString(),                 // titulo
                        fila[4].ToString(),                 // descripcion
                        DateTime.Parse(fila[5].ToString()), // fecha_creacion
                        int.Parse(fila[6].ToString()),      // estado
                        fila[7].ToString(),                 // nombre_familia
                        fila[8].ToString()                  // nombre_empresa
                    );
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer tarea por ID: {ex.Message}");
            }

            return null;
        }

        // Leer tareas por familia
        public ObservableCollection<TareaCoordinacion> LeerTareasPorFamilia(int id_familia)
        {
            ObservableCollection<TareaCoordinacion> tareas = new ObservableCollection<TareaCoordinacion>();

            try
            {
                string query = @"SELECT t.id_tarea, t.id_familia, t.id_empresa, t.titulo, 
                               t.descripcion, t.fecha_creacion, t.estado, 
                               f.nombre AS nombre_familia, e.nombre AS nombre_empresa
                               FROM fpc.tareascoordinacion t
                               INNER JOIN fpc.familiasprofesionales f ON t.id_familia = f.id_familia
                               INNER JOIN fpc.empresas e ON t.id_empresa = e.id_empresa
                               WHERE t.id_familia = @id_familia
                               ORDER BY t.fecha_creacion DESC;";

                var parametros = new Dictionary<string, object>
                {
                    { "@id_familia", id_familia }
                };

                var resultado = db.LeerConParametros(query, parametros);

                foreach (ObservableCollection<object> fila in resultado)
                {
                    TareaCoordinacion tarea = new TareaCoordinacion(
                        int.Parse(fila[0].ToString()),      // id_tarea
                        int.Parse(fila[1].ToString()),      // id_familia
                        int.Parse(fila[2].ToString()),      // id_empresa
                        fila[3].ToString(),                 // titulo
                        fila[4].ToString(),                 // descripcion
                        DateTime.Parse(fila[5].ToString()), // fecha_creacion
                        int.Parse(fila[6].ToString()),      // estado
                        fila[7].ToString(),                 // nombre_familia
                        fila[8].ToString()                  // nombre_empresa
                    );

                    tareas.Add(tarea);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer tareas por familia: {ex.Message}");
            }

            return tareas;
        }

        // Leer tareas por empresa
        public ObservableCollection<TareaCoordinacion> LeerTareasPorEmpresa(int id_empresa)
        {
            ObservableCollection<TareaCoordinacion> tareas = new ObservableCollection<TareaCoordinacion>();

            try
            {
                string query = @"SELECT t.id_tarea, t.id_familia, t.id_empresa, t.titulo, 
                               t.descripcion, t.fecha_creacion, t.estado, 
                               f.nombre AS nombre_familia, e.nombre AS nombre_empresa
                               FROM fpc.tareascoordinacion t
                               INNER JOIN fpc.familiasprofesionales f ON t.id_familia = f.id_familia
                               INNER JOIN fpc.empresas e ON t.id_empresa = e.id_empresa
                               WHERE t.id_empresa = @id_empresa
                               ORDER BY t.fecha_creacion DESC;";

                var parametros = new Dictionary<string, object>
                {
                    { "@id_empresa", id_empresa }
                };

                var resultado = db.LeerConParametros(query, parametros);

                foreach (ObservableCollection<object> fila in resultado)
                {
                    TareaCoordinacion tarea = new TareaCoordinacion(
                        int.Parse(fila[0].ToString()),      // id_tarea
                        int.Parse(fila[1].ToString()),      // id_familia
                        int.Parse(fila[2].ToString()),      // id_empresa
                        fila[3].ToString(),                 // titulo
                        fila[4].ToString(),                 // descripcion
                        DateTime.Parse(fila[5].ToString()), // fecha_creacion
                        int.Parse(fila[6].ToString()),      // estado
                        fila[7].ToString(),                 // nombre_familia
                        fila[8].ToString()                  // nombre_empresa
                    );

                    tareas.Add(tarea);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer tareas por empresa: {ex.Message}");
            }

            return tareas;
        }

        // Leer tareas por estado
        public ObservableCollection<TareaCoordinacion> LeerTareasPorEstado(int estado)
        {
            ObservableCollection<TareaCoordinacion> tareas = new ObservableCollection<TareaCoordinacion>();

            try
            {
                string query = @"SELECT t.id_tarea, t.id_familia, t.id_empresa, t.titulo, 
                               t.descripcion, t.fecha_creacion, t.estado, 
                               f.nombre AS nombre_familia, e.nombre AS nombre_empresa
                               FROM fpc.tareascoordinacion t
                               INNER JOIN fpc.familiasprofesionales f ON t.id_familia = f.id_familia
                               INNER JOIN fpc.empresas e ON t.id_empresa = e.id_empresa
                               WHERE t.estado = @estado
                               ORDER BY t.fecha_creacion DESC;";

                var parametros = new Dictionary<string, object>
                {
                    { "@estado", estado }
                };

                var resultado = db.LeerConParametros(query, parametros);

                foreach (ObservableCollection<object> fila in resultado)
                {
                    TareaCoordinacion tarea = new TareaCoordinacion(
                        int.Parse(fila[0].ToString()),      // id_tarea
                        int.Parse(fila[1].ToString()),      // id_familia
                        int.Parse(fila[2].ToString()),      // id_empresa
                        fila[3].ToString(),                 // titulo
                        fila[4].ToString(),                 // descripcion
                        DateTime.Parse(fila[5].ToString()), // fecha_creacion
                        int.Parse(fila[6].ToString()),      // estado
                        fila[7].ToString(),                 // nombre_familia
                        fila[8].ToString()                  // nombre_empresa
                    );

                    tareas.Add(tarea);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer tareas por estado: {ex.Message}");
            }

            return tareas;
        }

        // Leer tareas por familia y estado
        public ObservableCollection<TareaCoordinacion> LeerTareasPorFamiliaYEstado(int id_familia, int estado)
        {
            ObservableCollection<TareaCoordinacion> tareas = new ObservableCollection<TareaCoordinacion>();

            try
            {
                string query = @"SELECT t.id_tarea, t.id_familia, t.id_empresa, t.titulo, 
                               t.descripcion, t.fecha_creacion, t.estado, 
                               f.nombre AS nombre_familia, e.nombre AS nombre_empresa
                               FROM fpc.tareascoordinacion t
                               INNER JOIN fpc.familiasprofesionales f ON t.id_familia = f.id_familia
                               INNER JOIN fpc.empresas e ON t.id_empresa = e.id_empresa
                               WHERE t.id_familia = @id_familia AND t.estado = @estado
                               ORDER BY t.fecha_creacion DESC;";

                var parametros = new Dictionary<string, object>
                {
                    { "@id_familia", id_familia },
                    { "@estado", estado }
                };

                var resultado = db.LeerConParametros(query, parametros);

                foreach (ObservableCollection<object> fila in resultado)
                {
                    TareaCoordinacion tarea = new TareaCoordinacion(
                        int.Parse(fila[0].ToString()),      // id_tarea
                        int.Parse(fila[1].ToString()),      // id_familia
                        int.Parse(fila[2].ToString()),      // id_empresa
                        fila[3].ToString(),                 // titulo
                        fila[4].ToString(),                 // descripcion
                        DateTime.Parse(fila[5].ToString()), // fecha_creacion
                        int.Parse(fila[6].ToString()),      // estado
                        fila[7].ToString(),                 // nombre_familia
                        fila[8].ToString()                  // nombre_empresa
                    );

                    tareas.Add(tarea);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer tareas por familia y estado: {ex.Message}");
            }

            return tareas;
        }

        #endregion
    }
}