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

                var um = new UsuarioManager();
                var listResult = um.RetrieveAll();
                return Ok(listResult);

               
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("RetrieveAllHistory")]
        public ActionResult RetrieveAllHistory()
        {
            try
            {

                var um = new UsuarioManager();
                var listResult = um.RetrieveAllHistory();
                return Ok(listResult);


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
        public ActionResult Delete([FromBody] Usuario u)
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









        //recuperacion contrasena

        [HttpPost("SolicitarRecuperacion")]
        public IActionResult SolicitarRecuperacion([FromBody] SolicitarRecuperacionDTO dto)
        {
            try
            {
                UsuarioManager um = new UsuarioManager();
                um.SolicitarRecuperacion(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { errors = new { mensaje = new[] { ex.Message } } });
            }
        }

        [HttpPost("RestablecerContrasena")]
        public IActionResult RestablecerContrasena([FromBody] RestablecerContrasenaDTO dto)
        {
            try
            {
                UsuarioManager um = new UsuarioManager();
                um.RestablecerContrasena(dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { errors = new { mensaje = new[] { ex.Message } } });
            }
        }















    }
}
