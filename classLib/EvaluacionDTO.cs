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

        public DateTime FechaEvaluacion { get; set; }

        public decimal TamanoHectareas { get; set; }
        public string TipoSuperficie { get; set; }
        public bool TieneRio { get; set; }
        public bool Nacientes { get; set; }
        public int? CantidadNacientes { get; set; }
        public string TipoVegetacion { get; set; }
        public string UsoSuelo { get; set; }
    }
}