using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities_DTOs
{
    public class UsuarioRol : BaseDTO
    {
        public int IdUsuarioRol { get; set; }

        //Claves foráneas
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; } = null!;

        public int IdRol { get; set; }
        public Rol Rol { get; set; } = null!;
}

}
