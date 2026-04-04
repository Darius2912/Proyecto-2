using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities_DTOs
{
    public class BaseDTO
{
        public int IdUsuario { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
