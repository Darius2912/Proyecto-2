using AppCore;
using Entities_DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioManager _usuarioManager;

        public UsuarioController(UsuarioManager usuarioManager)
        {
            _usuarioManager = usuarioManager;
        }


        [HttpPost("Create")]
        public IActionResult Create(Usuario u)
        {
            try
            {
                var (registrado, mensaje) = _usuarioManager.Create(u);

                if (registrado)
                    return Ok(new { message = mensaje, usuario = u });
                else
                    return BadRequest(new { error = mensaje });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }


        // LOGIN asociado al POST

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginDTO logindto)
        {
            var u = new Usuario
            {
                Correo = logindto.Correo,
                Contrasena = logindto.Contrasena
            };

            var usuario = _usuarioManager.Login(u);

            if (usuario != null)
                return Ok(new { idUsuario = usuario.IdUsuario, rol = usuario.Rol });
            else
                return Unauthorized(new { error = "Usuario no encontrado" });
        }


        [HttpGet("RetrieveAll")]
        public ActionResult RetrieveAll()
        {
            try
            {
                var lstResults = _usuarioManager.RetrieveAll();
                return Ok(lstResults);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("RetrieveById/{id}")]
        public ActionResult RetrieveUsuarioById(int id)
        {
            try
            {
                var uResult = _usuarioManager.RetrieveById(id);
                return Ok(uResult);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("Update")]
        public ActionResult Update(Usuario u)
        {
            try
            {
                _usuarioManager.Update(u);
                return Ok(u);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("Delete")]
        public ActionResult Delete(Usuario u)
        {
            try
            {
                _usuarioManager.Delete(u);
                return Ok(u);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
