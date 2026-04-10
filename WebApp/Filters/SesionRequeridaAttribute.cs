using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class SesionRequeridaAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var idUsuario = context.HttpContext.Session.GetInt32("IdUsuario");

        if (idUsuario == null)
        {
            context.Result = new RedirectToActionResult("Login", "Acceso", null);
        }
    }
}