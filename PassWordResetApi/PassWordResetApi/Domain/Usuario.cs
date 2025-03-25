using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PassWordResetApi.domain
{
    public class Usuario
    {
        public int Id {  get; set; }
        public required string email { get; set; }
        public required string password { get; set; }
       
    }
}
