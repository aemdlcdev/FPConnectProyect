using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using FPConnect.persistence.Manages;

namespace FPConnect.domain
{
    internal class Centro
    {
        public int id_centro { get; set; }
        public string nombre { get; set; }
        public string direccion { get; set; }
        public string horario { get; set; }
        public string telefono { get; set; }
        public BitmapImage logo { get; set; } 

        private CentroManage cm;

        public Centro()
        {
            cm = new CentroManage();
        }

        // Constructor actualizado sin el parámetro logo
        public Centro(int id_centro, string nombre, string direccion, string horario, string telefono)
        {
            this.id_centro = id_centro;
            this.nombre = nombre;
            this.direccion = direccion;
            this.horario = horario;
            this.telefono = telefono;
            cm = new CentroManage();
        }

        // Mantener el constructor antiguo para compatibilidad, pero convertir el logo si es posible
        public Centro(int id_centro, string nombre, string direccion, string horario, string telefono, BitmapImage logo)
        {
            this.id_centro = id_centro;
            this.nombre = nombre;
            this.direccion = direccion;
            this.horario = horario;
            this.telefono = telefono;
            this.logo = logo;
            

            cm = new CentroManage();
        }

        public Centro LeerCentroPorId(int id)
        {
            return cm.LeerCentroPorId(id);
        }
    }
}
