using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPConnect.persistence.Manages;

namespace FPConnect.domain
{
    internal class Rol
    {
        public int id_rol { get; set; }
        public int id_centro { get; set; }  
        public string nombre { get; set; }
        private RolManage rm;
        public Rol() { rm = new RolManage(); }
        public Rol(int id_rol, int id_centro, string nombre)
        {
            this.id_rol = id_rol;
            this.id_centro = id_centro;
            this.nombre = nombre;
            rm = new RolManage();   
        }

        public ObservableCollection<Rol> LeerRolesPorCentro(int id_centro)
        {
            return rm.LeerRolesPorCentro(id_centro);    
        }
        
        public override string ToString()
        {
            return this.nombre + " " + "[" + this.id_rol + "]";   
        }

    }
}
