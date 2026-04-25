using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities_DTOs
{
 public class PropiedadDTO
{
    public int Id { get; set; }

        public int IdUsuario{ get; set; }

    public string NombreFinca { get; set; }
    public string Ubicacion { get; set; }

        public string Provincia { get; set; }
        public string Canton { get; set; }
        public string Distrito { get; set; }

        public double Latitud { get; set; }
    public double Longitud { get; set; }

    public decimal TamanoHectareas { get; set; }
    public int TipoSuperficie { get; set; }

    public string? Observaciones { get; set; }
    public int TieneRio { get; set; }
    public int Nacientes { get; set; }
    public int? CantidadNacientes { get; set; }

    public int TipoVegetacion { get; set; }
    public string UsoSuelo { get; set; }

    public string Estado { get; set; } = "Pendiente";

    public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public List<IFormFile> Fotografias { get; set; }
        public List<string> Fotos { get; set; } = new List<string>();

       
    }
}
