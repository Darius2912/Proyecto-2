namespace WebApp.Models
{
    public class PagoHistorial
    {
        public string Propiedad { get; set; } = string.Empty;
        public string Periodo { get; set; } = string.Empty;
        public decimal Monto { get; set; }
        public string Estado { get; set; } = string.Empty;
    }
 }

