using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
namespace Entities_DTOs
{
    public class Usuario : BaseDTO
    {
        public string Correo { get; set; }
        public string Cedula { get; set; }
        public string Nombre { get; set; } 
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Estado { get; set; }

        public string Contrasena { get; set; }
        public string ConfirmarContrasena { get; set; }

        public int Rol { get; set; }

       
        

    }
}