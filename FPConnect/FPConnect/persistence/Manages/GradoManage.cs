using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPConnect.domain;

namespace FPConnect.persistence.Manages
{
    internal class GradoManage
    {
        private ObservableCollection<Grado> listaGrados { get; set; }
        private DBBroker db = DBBroker.ObtenerAgente();

        public GradoManage()
        {
            listaGrados = new ObservableCollection<Grado>();
        }


        public ObservableCollection<Grado> LeerGrados()
        {
            Grado grado = null;
            db = DBBroker.ObtenerAgente();

            var resultado = db.LeerSinParametros("SELECT * FROM fpc.grados;");
            foreach (ObservableCollection<Object> c in resultado)
            {
                grado = new Grado(
                    int.Parse(c[0].ToString()), //id
                    c[1].ToString()); //nombre
                this.listaGrados.Add(grado);
            }
            return listaGrados;
        }
    }
}
