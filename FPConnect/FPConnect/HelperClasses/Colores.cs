using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPConnect.HelperClasses
{
    internal class Colores
    {
        public static string COLOR1 = "#1098AD";
        public static string COLOR2 = "#1E88E5";
        public static string COLOR3 = "#FF8F00";
        public static string COLOR4 = "#0CA678";
        public static string COLOR5 = "#6741D9";
        public static string COLOR6 = "#FF6D00";       

        private static List<String> colores = new List<string>
        {
            "#1098AD",
            "#1E88E5",
            "#FF8F00",
            "#0CA678",
            "#6741D9",
            "#FF6D00"
        };

        public static string GetRandomColor()
        {
            Random random = new Random();
            int index = random.Next(colores.Count);
            Console.WriteLine(colores[index]);
            return colores[index]; 
        }


    }
}
