using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities_DTOs
{
    public class UsuarioResponseDto
    {
        public int IdUsuario { get; set; }
        public string Cedula { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string? Apellido { get; set; }
        public string Correo { get; set; } = string.Empty;
        public string? Telefono { get; set; }
        public string Estado { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; }

        public string Rol { get; set; } = string.Empty; // solo nombre
    }
}
