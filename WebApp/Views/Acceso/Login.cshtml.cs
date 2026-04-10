using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class LoginModel : PageModel
{
    [BindProperty]
    public string Email { get; set; }

    [BindProperty]
    public string Password { get; set; }

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        // Aquí validas el usuario contra tu base de datos
        if (Email == "admin@ejemplo.com" && Password == "1234")
        {
            // Redirigir al dashboard o página principal
            return RedirectToPage("/Index");
        }

        ModelState.AddModelError(string.Empty, "Credenciales inválidas");
        return Page();
    }
}
