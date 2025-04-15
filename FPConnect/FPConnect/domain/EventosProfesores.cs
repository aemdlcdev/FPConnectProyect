using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPConnect.persistence.Manages;

namespace FPConnect.domain
{
    internal class EventosProfesores
    {
        public int id_evento { get; set; }
        public int id_profesor { get; set; }
        public string nombre { get; set; }
        public DateTime fecha { get; set; }
        public string hora { get; set; }
        public int id_estado { get; set; }
        private EventosProfesoresManage epm;

        public EventosProfesores() { epm = new EventosProfesoresManage(); }

        public EventosProfesores(int id_evento, int id_profesor, string nombre, DateTime fecha, string hora, int id_estado)
        {
            this.id_evento = id_evento;
            this.id_profesor = id_profesor;
            this.nombre = nombre;
            this.fecha = fecha;
            this.hora = hora;
            this.id_estado = id_estado;
            epm = new EventosProfesoresManage();
        }

        public EventosProfesores(int id_profesor, string nombre, DateTime fecha, string hora, int id_estado)
        {
            this.id_profesor = id_profesor;
            this.nombre = nombre;
            this.fecha = fecha;
            this.hora = hora;
            this.id_estado = id_estado;
            epm = new EventosProfesoresManage();
        }

        public ObservableCollection<EventosProfesores> LeerEventosPorIdProfesor(int id_profesor)
        {        
            return epm.LeerEventosPorIdProfesor(id_profesor);
        }

        public int InsertarEvento(EventosProfesores evento)
        {
            return epm.AgregarEvento(evento);
        }

        public void ModificarEvento(EventosProfesores evento)
        {
            epm.ModificarEvento(evento);
        }

        public void EliminarEvento(int id_evento)
        {
            epm.EliminarEvento(id_evento);
        }

    }
}
