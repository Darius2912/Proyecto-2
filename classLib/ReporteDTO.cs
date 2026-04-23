using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities_DTOs
{
    public class ReporteDTO : BaseDTO
    {
        public string NombreFinca { get; set; }

        public DateTime Fecha { get; set; }

        public string Estado { get; set; }

        public string Observaciones { get; set; }
    }
}
