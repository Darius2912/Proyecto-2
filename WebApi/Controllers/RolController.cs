using AppCore;
using Entities_DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly RolManager _rolManager;

        public RolController(RolManager rolManager)
        {
            _rolManager = rolManager;
        }

        [HttpPost("Create")]
        public IActionResult Create(Rol rol)
        {
            try
            {
                _rolManager.Create(rol);
                return Ok(new { message = "Rol creado correctamente", rol });
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
                var roles = _rolManager.RetrieveAll();
                return Ok(roles);
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
                var rol = _rolManager.RetrieveById(id);
                return Ok(rol);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("Update")]
        public IActionResult Update(Rol rol)
        {
            try
            {
                _rolManager.Update(rol);
                return Ok(rol);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(Rol rol)
        {
            try
            {
                _rolManager.Delete(rol);
                return Ok(new { message = "Rol eliminado correctamente", rol });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
