using DataAccess.CRUD;
using Entities_DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore
{
    public class PropiedadManager : BaseManager
    {

        public int Create(PropiedadDTO p)
        {
            try
            {

              
                var pCrud = new PropiedadCrudFactory();
               
                return pCrud.CreateAndReturnId(p); 

            }
            catch (Exception ex)
            {
                ManegerException(ex);
                return 0;
            }
        }
        public PropiedadDTO ObtenerPorId(int id)
        {
            try
            {
                var pCrud = new PropiedadCrudFactory();
                return pCrud.RetrieveById(id);
            }
            catch (Exception ex)
            {
                ManegerException(ex);
                return null;
            }
        }

        public List<string> ObtenerProvincias()
        {
            return new PropiedadCrudFactory().ObtenerProvincias();
        }

        public List<string> ObtenerCantones(string provincia)
        {
            return new PropiedadCrudFactory().ObtenerCantones(provincia);
        }

        public List<string> ObtenerDistritos(string canton)
        {
            return new PropiedadCrudFactory().ObtenerDistritos(canton);
        }

        public void UpdateDesdeEvaluacion(EvaluacionDTO dto)
        {
            var propiedad = new PropiedadDTO
            {
                Id = dto.PropiedadId,
                TamanoHectareas = dto.TamanoHectareas,
                TipoSuperficie = dto.TipoSuperficie,
                TieneRio = dto.TieneRio,
                Nacientes = dto.Nacientes,
                CantidadNacientes = dto.CantidadNacientes,
                TipoVegetacion = dto.TipoVegetacion,
                UsoSuelo = dto.UsoSuelo
            };


            var crud = new PropiedadCrudFactory();
            crud.Update(propiedad);


            if (dto.Estado == "Rechazada") {
                return;
            }


            var pCrud = new PagoCrudFactory();

          
            var parametros = pCrud.RetrieveParametro();
            var parametrosPendiente = pCrud.RetrieveParametroPendiente(propiedad.TipoSuperficie);
            var parametrosBosque = pCrud.RetrieveParametroBosque(propiedad.TipoVegetacion);

          
            decimal montoBase = propiedad.TamanoHectareas * parametros.PrecioPorHectarea;

          
            decimal porcentajeBosque = parametrosBosque.PorcentajePago / 100m;
            decimal porcentajePendiente = parametrosPendiente.PorcentajePago / 100m;
            decimal porcentajeNaciente = parametros.PorcentajeNaciente / 100m;
            decimal porcentajeRio = parametros.PorcentajeRios / 100m;

          
            decimal porcentajeHidrico = 0;

            if (propiedad.Nacientes > 0)
            {
              
                int maxNacientes = 3;
                int nacientesUsadas = Math.Min(propiedad.Nacientes, maxNacientes);

                porcentajeHidrico += nacientesUsadas * porcentajeNaciente;
            }

            if (propiedad.TieneRio > 0)
            {
                porcentajeHidrico += porcentajeRio;
            }

           
            decimal maxHidrico = 0.20m;
            if (porcentajeHidrico > maxHidrico)
            {
                porcentajeHidrico = maxHidrico;
            }

            
            decimal porcentajeTotal = porcentajeBosque + porcentajePendiente + porcentajeHidrico;

            
            decimal topeMaximo = 0.40m;

           
            if (porcentajeTotal > topeMaximo)
            {
                decimal factor = topeMaximo / porcentajeTotal;

                porcentajeBosque *= factor;
                porcentajePendiente *= factor;
                porcentajeHidrico *= factor;

                porcentajeTotal = topeMaximo;
            }

            
            decimal montoBosque = montoBase * porcentajeBosque;
            decimal montoPendiente = montoBase * porcentajePendiente;
            decimal montoHidrico = montoBase * porcentajeHidrico;

            decimal montoAdicional = montoBosque + montoPendiente + montoHidrico;
            decimal totalPago = montoBase + montoAdicional;

          
            var planPago = new PlanPago()
            {
                IdPropiedad = propiedad.Id,
                IdTipoBosque = propiedad.TipoVegetacion,
                IdTipoPendiente = propiedad.TipoSuperficie,

                PrecioBaseHectarea = montoBase,

                PorcentajeBosque = montoBosque,
                PorcentajeTerreno = montoPendiente,
                PorcentajeHidrico = montoHidrico,

                FechaCalculo = DateTime.Today,
                Estado = "Pendiente",
                IdConfiguracionParametros = 3,

                TotalPago = totalPago
            };
           
            
            pCrud.Create(planPago);
            

        }

        public List<PropiedadDTO> ObtenerPorEstado(string Estado)
        {
            try
            {
                var pCrud = new PropiedadCrudFactory();
                return pCrud.RetrieveByEstado(Estado);
            }
            catch (Exception ex)
            {
                ManegerException(ex);
                return new List<PropiedadDTO>();
            }
        }

        public List<PropiedadDTO> ObtenerPorUsuario(int idUsuario)
        {
            var crud = new PropiedadCrudFactory();
            return crud.RetrieveByUsuario(idUsuario);
        }

        public List<PropiedadDTO> RetrieveApprovedPropertiesByUsuarioId(int idUsuario)
        {
            var crud = new PropiedadCrudFactory();
            return crud.RetrieveApprovedPropertiesByUsuarioId(idUsuario);
        }

        public void CreateFoto(int propiedadId, string rutaFoto)
        {
            try
            {


                var pCrud = new PropiedadCrudFactory();
                pCrud.CreateFoto(propiedadId, rutaFoto);
                return ;

            }
            catch (Exception ex)
            {
                ManegerException(ex);
              
            }
        }
    }
}
