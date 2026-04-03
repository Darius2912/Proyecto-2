// Filters/RolRequeridoAttribute.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class RolRequeridoAttribute : ActionFilterAttribute
{
    private readonly int _rol;

    public RolRequeridoAttribute(int rol)
    {
        _rol = rol;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var rol = context.HttpContext.Session.GetInt32("Rol");

        if (rol == null || rol != _rol)
        {
            // Si tiene sesión pero rol incorrecto → manda al Index
            context.Result = new RedirectToActionResult("Index", "Home", null);
        }
    }
}