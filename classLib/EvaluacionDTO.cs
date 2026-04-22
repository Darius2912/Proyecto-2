using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities_DTOs
{
    public class EvaluacionDTO
    {
        public int PropiedadId { get; set; }
        public string Estado { get; set; }
        public string Observaciones { get; set; }
        public int IdUsuario { get; set; }
    }
}
