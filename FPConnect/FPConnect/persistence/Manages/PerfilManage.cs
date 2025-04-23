using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPConnect.domain;

namespace FPConnect.persistence.Manages
{
    internal class PerfilManage
    {
        private ObservableCollection<Perfil> listaPerfiles { get; set; }
        private DBBroker db = DBBroker.ObtenerAgente();

        public PerfilManage()
        {
            listaPerfiles = new ObservableCollection<Perfil>();
        }

        public ObservableCollection<Perfil> LeerPerfilCentro(int id_familia, int id_grado)
        {
            Perfil perfil = null;
            db = DBBroker.ObtenerAgente();

            var resultado = db.LeerConParametros("SELECT id_perfil,id_familia,nombre FROM fpc.perfiles WHERE id_familia = @id_familia AND id_grado = @id_grado ;", new Dictionary<string, object> { 
                { "@id_familia", id_familia },
                { "@id_grado", id_grado }
            });
            foreach (ObservableCollection<Object> c in resultado)
            {
                perfil = new Perfil(
                    int.Parse(c[0].ToString()), //id
                    int.Parse(c[1].ToString()), //id_familia
                    c[2].ToString()); //nombre
                this.listaPerfiles.Add(perfil);
                Console.WriteLine(perfil);
            }
            return listaPerfiles;
        }

    }
}
