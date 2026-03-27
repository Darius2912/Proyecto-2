using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities_DTOs
{
    public class Rol : BaseDTO
    {
        public int IdRol { get; set; }
        public string NombreRol { get; set; } = string.Empty;

        public ICollection<UsuarioRol> UsuarioRoles { get; set; } = new List<UsuarioRol>();
    }
}
