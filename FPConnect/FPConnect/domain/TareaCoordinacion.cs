using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPConnect.persistence.Manages;

namespace FPConnect.domain
{
    public class TareaCoordinacion
    {
        public int id_tarea { get; set; }
        public int id_familia { get; set; }
        public int id_empresa { get; set; }
        public string titulo { get; set; }
        public string descripcion { get; set; }
        public DateTime fecha_creacion { get; set; }
        public int estado { get; set; }

        // Propiedades calculadas (no están en la BD, se obtienen por JOIN)
        public string nombre_familia { get; set; }
        public string nombre_empresa { get; set; }

        // Gestor para operaciones en la BD
        private TareaCoordinacionManage tm;


        #region Constructores

        public TareaCoordinacion()
        {
            tm = new TareaCoordinacionManage();
            fecha_creacion = DateTime.Now;
            estado = 1; // Por defecto, pendiente (1)
        }

        public TareaCoordinacion(int id_familia, int id_empresa, string titulo,
                            string descripcion)
        {
            this.id_familia = id_familia;
            this.id_empresa = id_empresa;
            this.titulo = titulo;
            this.descripcion = descripcion;
            this.fecha_creacion = DateTime.Now;
            this.estado = 1; // Por defecto, pendiente
            tm = new TareaCoordinacionManage();
        }

        public TareaCoordinacion(int id_familia, int id_empresa, string titulo,
                            string descripcion, int estado)
        {
            this.id_familia = id_familia;
            this.id_empresa = id_empresa;
            this.titulo = titulo;
            this.descripcion = descripcion;
            this.fecha_creacion = DateTime.Now;
            this.estado = estado;
            tm = new TareaCoordinacionManage();
        }

        public TareaCoordinacion(int id_tarea, int id_familia, int id_empresa, string titulo,
                            string descripcion, DateTime fecha_creacion, int estado)
        {
            this.id_tarea = id_tarea;
            this.id_familia = id_familia;
            this.id_empresa = id_empresa;
            this.titulo = titulo;
            this.descripcion = descripcion;
            this.fecha_creacion = fecha_creacion;
            this.estado = estado;
            tm = new TareaCoordinacionManage();
        }

        public TareaCoordinacion(int id_tarea, int id_familia, int id_empresa, string titulo,
                            string descripcion, DateTime fecha_creacion, int estado,
                            string nombre_familia, string nombre_empresa)
        {
            this.id_tarea = id_tarea;
            this.id_familia = id_familia;
            this.id_empresa = id_empresa;
            this.titulo = titulo;
            this.descripcion = descripcion;
            this.fecha_creacion = fecha_creacion;
            this.estado = estado;
            this.nombre_familia = nombre_familia;
            this.nombre_empresa = nombre_empresa;
            tm = new TareaCoordinacionManage();
        }

        #endregion

        #region Métodos CRUD

        // Insertar una nueva tarea
        public bool Insertar(TareaCoordinacion tarea)
        {
            return tm.InsertarTarea(tarea);
        }

        // Actualizar una tarea existente
        public bool Actualizar()
        {
            return tm.ActualizarTarea(this);
        }

        // Eliminar una tarea
        public bool Eliminar(TareaCoordinacion tarea)
        {
            return tm.EliminarTarea(tarea.id_tarea);
        }

        // Cambiar estado de una tarea
        public bool CambiarEstado(int nuevoEstado)
        {
            this.estado = nuevoEstado;
            return tm.ActualizarEstadoTarea(this.id_tarea, nuevoEstado);
        }

        #endregion

        #region Métodos estáticos para consultas

        // Obtener todas las tareas
        public ObservableCollection<TareaCoordinacion> ObtenerTodas()
        {
            TareaCoordinacionManage tm = new TareaCoordinacionManage();
            return tm.LeerTareas();
        }

        // Obtener tareas por familia
        public ObservableCollection<TareaCoordinacion> ObtenerPorFamilia(int id_familia)
        {
            return tm.LeerTareasPorFamilia(id_familia);
        }

        // Obtener tareas por empresa
        public ObservableCollection<TareaCoordinacion> ObtenerPorEmpresa(int id_empresa)
        {
            return tm.LeerTareasPorEmpresa(id_empresa);
        }

        // Obtener tareas por estado
        public ObservableCollection<TareaCoordinacion> ObtenerPorEstado(int estado)
        {
            return tm.LeerTareasPorEstado(estado);
        }

        // Obtener tarea por ID
        public TareaCoordinacion ObtenerPorId(int id_tarea)
        {
            return tm.LeerTareaPorId(id_tarea);
        }

        // Obtener tareas por familia y estado
        public ObservableCollection<TareaCoordinacion> ObtenerPorFamiliaYEstado(int id_familia, int estado)
        {
            return tm.LeerTareasPorFamiliaYEstado(id_familia, estado);
        }

        #endregion

        // Método ToString para representar la tarea en controles
        public override string ToString()
        {
            return $"{titulo} - {nombre_empresa}";
        }

    }
}
