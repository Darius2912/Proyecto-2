using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class IndexModel : PageModel
{
    [BindProperty]
    public string Nombre { get; set; }

    [BindProperty]
    public string Email { get; set; }

    public string Mensaje { get; set; }

    public void OnPost()
    {
        Mensaje = "Usuario registrado: " + Nombre;
    }
}