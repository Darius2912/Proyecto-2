using AppCore;
using Entities_DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagoController : ControllerBase
    {
       
        private readonly PagosManager _pagosManager;

        public PagoController( PagosManager pagosManager)
        {
            
            _pagosManager = pagosManager;
        }



        [HttpGet("RetrieveAllPlanesPendientes")]
        public ActionResult RetrieveAllPlanesPendientes()
        {
            try
            {
                var pm = new PagosManager();

                var listResult = pm.RetrieveAllPlanesPendientes();

                return Ok(listResult);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);

            }

        }

        [HttpPost("AprobarPlan")]
        public ActionResult AprobarPlan(PlanPago p)
        {
            try
            {
                _pagosManager.AprobarPlan(p);
                return Ok(p);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("RetrievePlanPagoById")]
        public ActionResult RetrievePlanPagoById(int Id)
        {
            try
            {
             var planPago =    _pagosManager.RetrievePlanPagoById(Id);
                return Ok(planPago);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
