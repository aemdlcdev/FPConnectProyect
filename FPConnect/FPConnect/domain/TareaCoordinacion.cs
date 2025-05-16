using System;
using System.Collections.Generic;
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
    }
}
