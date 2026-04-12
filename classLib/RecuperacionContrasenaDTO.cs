using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities_DTOs
{
    public class RecuperacionContrasenaDTO : BaseDTO
{
        public int Id { get; set; }
        public string Correo { get; set; }
        public string Token { get; set; }
        public DateTime FechaExpira { get; set; }
        public bool Usado { get; set; }

    }
}
