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

        public int PropiedadId { get; set; }

        public string Ubicacion { get; set; }

        public decimal TamanoHectareas { get; set; }

        public List<string> Fotos { get; set; } = new List<string>();
    }
}
