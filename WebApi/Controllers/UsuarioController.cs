using AppCore;
using Entities_DTOs;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Create([FromBody] Usuario u)
        {
            try
            {
                if (string.IsNullOrEmpty(u.Estado))
                    u.Estado = "Activo";

                if (u.FechaRegistro == default)
                    u.FechaRegistro = DateTime.Now;

                _usuarioManager.Create(u);
                return Ok(new { message = "Usuario creado y correo enviado", usuario = u });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        // LOGIN asociado al POST
        [HttpPost("Login")]
        public IActionResult Login([FromBody] Usuario loginRequest)
        {
            try
            {
                var usuario = _usuarioManager.Login(loginRequest.Correo, loginRequest.Contrasena);

                if (usuario == null)
                    return Unauthorized("Credenciales inválidas");

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
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
