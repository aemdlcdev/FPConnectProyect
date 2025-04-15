using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPConnect.domain;

namespace FPConnect.persistence.Manages
{
    internal class EventosProfesoresManage
    {
        private ObservableCollection<EventosProfesores> listaEventos { get; set; }
        private DBBroker db = DBBroker.ObtenerAgente();

        public EventosProfesoresManage()
        {
            listaEventos = new ObservableCollection<EventosProfesores>();
        }

        public ObservableCollection<EventosProfesores> LeerEventosPorIdProfesor(int id_profesor)
        {
            listaEventos.Clear();
            db = DBBroker.ObtenerAgente();
            var resultado = db.LeerConParametros("SELECT id_evento,id_profesor,nombre,fecha,hora,id_estado FROM fpc.eventosprofesores WHERE id_profesor = @id_profesor;", new Dictionary<string, object> { { "@id_profesor", id_profesor } });
            foreach (ObservableCollection<Object> e in resultado)
            {
                // Crear el evento con datos básicos
                EventosProfesores evento = new EventosProfesores(
                    int.Parse(e[0].ToString()), //id_evento
                    int.Parse(e[1].ToString()), //id_profesor
                    e[2].ToString(), //nombre
                    DateTime.Parse(e[3].ToString()), //fecha
                    e[4].ToString(), //hora
                    int.Parse(e[5].ToString())); //id_estado

                listaEventos.Add(evento);
            }
            return listaEventos;

        }
        public int AgregarEvento(EventosProfesores evento)
        {
            db = DBBroker.ObtenerAgente();

            string sql = "INSERT INTO fpc.eventosprofesores (id_profesor, nombre, fecha, hora, id_estado) " +
                         "VALUES (@id_profesor, @nombre, @fecha, @hora, @id_estado);";

            Dictionary<string, object> parametros = new Dictionary<string, object>
            {
                { "@id_profesor", evento.id_profesor },
                { "@nombre", evento.nombre },
                { "@fecha", evento.fecha },
                { "@hora", evento.hora },
                { "@id_estado", evento.id_estado }
            };

            // Ejecutar la inserción
            db.Modificar(sql, parametros);

            // Obtener el ID del evento recién insertado
            int nuevoId = db.LeerUltimoEventoInsertado();

            return nuevoId;
        }

        public void ModificarEvento(EventosProfesores evento)
        {
            db = DBBroker.ObtenerAgente();
            string sql = "UPDATE fpc.eventosprofesores SET nombre=@nombre,fecha=@fecha,hora=@hora,id_estado=@id_estado WHERE id_evento=@id_evento;";
            Dictionary<string, object> parametros = new Dictionary<string, object>
            {
                { "@id_evento", evento.id_evento },
                { "@nombre", evento.nombre },
                { "@fecha", evento.fecha },
                { "@hora", evento.hora },
                { "@id_estado", evento.id_estado }
            };
            db.Modificar(sql, parametros);
        }

        public void EliminarEvento(int id_evento)
        {
            db = DBBroker.ObtenerAgente();
            string sql = "DELETE FROM fpc.eventosprofesores WHERE id_evento=@id_evento;";
            Dictionary<string, object> parametros = new Dictionary<string, object>
            {
                { "@id_evento", id_evento }
            };
            db.Modificar(sql, parametros);
        }

    }
}
