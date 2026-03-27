using AppCore;
using Entities_DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioRolController : ControllerBase
    {
        private readonly UsuarioRolManager _usuarioRolManager;

        public UsuarioRolController(UsuarioRolManager usuarioRolManager)
        {
            _usuarioRolManager = usuarioRolManager;
        }

        [HttpPost("AsignarRol")]
        public IActionResult AsignarRol(UsuarioRol usuarioRol)
        {
            try
            {
                _usuarioRolManager.AsignarRol(usuarioRol);
                return Ok(new { message = "Rol asignado correctamente", usuarioRol });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("QuitarRol")]
        public IActionResult QuitarRol(UsuarioRol usuarioRol)
        {
            try
            {
                _usuarioRolManager.QuitarRol(usuarioRol);
                return Ok(new { message = "Rol quitado correctamente", usuarioRol });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("RetrieveAll")]
        public IActionResult RetrieveAll()
        {
            try
            {
                var asignaciones = _usuarioRolManager.RetrieveAll();
                return Ok(asignaciones);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("RetrieveById/{id}")]
        public IActionResult RetrieveById(int id)
        {
            try
            {
                var usuarioRol = _usuarioRolManager.RetrieveById(id);
                return Ok(usuarioRol);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("ObtenerRolesPorUsuario/{idUsuario}")]
        public IActionResult ObtenerRolesPorUsuario(int idUsuario)
        {
            try
            {
                var roles = _usuarioRolManager.ObtenerRolesPorUsuario(idUsuario);
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
