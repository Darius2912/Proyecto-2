using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
namespace Entities_DTOs
{
    public class Usuario : BaseDTO
    {
        public int IdUsuario { get; set; }
        public string Cedula { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string? Apellido { get; set; }
        public string Correo { get; set; } = string.Empty;
        public string Contrasena { get; set; } = string.Empty;
        public string? Telefono { get; set; }
        public string? Estado { get; set; }
        public DateTime FechaRegistro { get; set; }

        // 🔹 Relación con Rol
        public int IdRol { get; set; }   // FK simple
        public Rol? Rol { get; set; }    // navegación opcional

        public List<Rol> Roles { get; set; } = new List<Rol>();
    }
}