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

    public decimal Latitud { get; set; }
    public decimal Longitud { get; set; }

    public decimal TamanoHectareas { get; set; }
    public string TipoSuperficie { get; set; }

    public string TieneRio { get; set; }
    public string Nacientes { get; set; }
    public int? CantidadNacientes { get; set; }

    public string TipoVegetacion { get; set; }
    public string UsoSuelo { get; set; }

    public string Estado { get; set; } = "Pendiente";

    public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public List<IFormFile> Fotografias { get; set; }
    }
}
