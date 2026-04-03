using System;
using System.Collections.Generic;
using System.Text;

namespace Entities_DTOs
{
 public class Propiedad
{
    public int Id { get; set; }

    public string NombreFinca { get; set; }
    public string Ubicacion { get; set; }

    public decimal Latitud { get; set; }
    public decimal Longitud { get; set; }

    public decimal Tamano { get; set; }
    public string Superficie { get; set; }

    public string Rios { get; set; }
    public string Nacientes { get; set; }
    public int? CantidadNacientes { get; set; }

    public string Vegetacion { get; set; }
    public string UsoSuelo { get; set; }

    public string Estado { get; set; } = "Pendiente";

    public DateTime FechaRegistro { get; set; } = DateTime.Now;

    public string? Imagenes { get; set; }
}
}
