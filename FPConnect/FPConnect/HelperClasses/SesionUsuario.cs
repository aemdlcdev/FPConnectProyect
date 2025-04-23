using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPConnect.HelperClasses
{
    public static class SesionUsuario
    {
        public static int id_profesor { get; set; }
        public static string NombreUsuario { get; set; }
        public static int IdRol { get; set; }
        public static int IdCentro { get; set; }
        public static int IdFamilia { get; set; }
        public static int IdGrado { get; set; }
        public static int IdPerfil { get; set; }
        public static int IdCurso { get; set; }
        public static int IdTurno { get; set; }
        public static string sexo { get; set; }
        public static bool EstaAutenticado => !string.IsNullOrEmpty(NombreUsuario);

    }

}
