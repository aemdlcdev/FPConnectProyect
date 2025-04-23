using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPConnect.domain;

namespace FPConnect.persistence.Manages
{
    internal class TurnoManage
    {
        private ObservableCollection<Turno> listaTurnos { get; set; }
        private DBBroker db = DBBroker.ObtenerAgente();

        public TurnoManage()
        {
            listaTurnos = new ObservableCollection<Turno>();
        }

        public ObservableCollection<Turno> LeerTurnos()
        {
            Turno turno = null;
            db = DBBroker.ObtenerAgente();

            var resultado = db.LeerSinParametros("SELECT * FROM fpc.turnos;");
            foreach (ObservableCollection<Object> c in resultado)
            {
                turno = new Turno(
                    int.Parse(c[0].ToString()), //id
                    c[1].ToString()); //nombre
                this.listaTurnos.Add(turno);
            }
            return listaTurnos;
        }

    }
}
