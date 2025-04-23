using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPConnect.persistence.Manages;

namespace FPConnect.domain
{
    internal class Turno
    {
        public int id_turno { get; set; }
        public string nombre { get; set; }
        private TurnoManage tm;
        public Turno() { tm = new TurnoManage(); }

        public Turno(int id_turno, string nombre)
        {
            this.id_turno = id_turno;
            this.nombre = nombre;
            tm = new TurnoManage();
        }

        public ObservableCollection<Turno> LeerTurnos()
        {
            return tm.LeerTurnos();
        }

        public override string ToString()
        {
            return nombre + " [" + id_turno + "]";
        }

    }
}
