using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPConnect.HelperClasses
{
    public static class SesionUsuario
    {
        public static string NombreUsuario { get; set; }
        public static int IdRol { get; set; }
        public static int IdDepartamento { get; set; }

        public static bool EstaAutenticado => !string.IsNullOrEmpty(NombreUsuario);
    }

}
