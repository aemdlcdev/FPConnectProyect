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


        public ObservableCollection<Grado> LeerGradosPorCentro(int id_centro)
        {
            Grado grado = null;
            db = DBBroker.ObtenerAgente();

            var resultado = db.LeerConParametros(
                @"SELECT id_grado, id_centro, nombre 
                FROM fpc.grados
                WHERE id_centro = @id_centro;",
                new Dictionary<string, object> {{ "@id_centro", id_centro } }
            );

            foreach (ObservableCollection<Object> c in resultado)
            {
                grado = new Grado(
                    int.Parse(c[0].ToString()), //id_grado
                    int.Parse(c[1].ToString()), //id_centro
                    c[2].ToString()); //nombre
                this.listaGrados.Add(grado);
                //Traza
                Console.WriteLine("Grado: " + grado.ToString());
            }
            return listaGrados;
        }
    }
}
