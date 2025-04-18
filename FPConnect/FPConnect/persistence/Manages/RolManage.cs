using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPConnect.domain;

namespace FPConnect.persistence.Manages
{
    internal class RolManage
    {
        private ObservableCollection<Rol> listaRol { get; set; }
        private DBBroker db = DBBroker.ObtenerAgente();

        public RolManage()
        {
            listaRol = new ObservableCollection<Rol>();
        }

        public ObservableCollection<Rol> LeerRolesPorCentro(int id_centro) 
        {
            
            string query = "SELECT id_rol,id_centro,nombre FROM fpc.roles WHERE id_centro = @id_centro;";
            var parametros = new Dictionary<string, object>
            {
                { "@id_centro", id_centro }
            };
            var resultado = db.LeerConParametros(query, parametros);
            foreach (ObservableCollection<Object> c in resultado)
            {
                Rol rol = new Rol(
                    int.Parse(c[0].ToString()), //id
                    int.Parse(c[1].ToString()), //id_centro
                    c[2].ToString()); //nombre

                this.listaRol.Add(rol);
            }

            return listaRol;

        }

    }
}
