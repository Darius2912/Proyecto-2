using DataAccess.DAO;
using Entities_DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CRUD
{
    public class EvaluacionCrudFactory : CrudFactory
    {
        public EvaluacionCrudFactory()
        {
            SqlDAO = SqlDAO.GetInstance();
        }
        public override void Create(BaseDTO baseDTO)
        {
            throw new NotImplementedException();
        }
        public void Create(EvaluacionDTO e)
        {
            var operation = new SqlOperation();
            operation.ProcedureName = "CrearEvaluacion";

            operation.AddIntParam("PropiedadId", e.PropiedadId);
            operation.AddStringParam("Estado", e.Estado);
            operation.AddStringParam("Observaciones", e.Observaciones);
            operation.AddIntParam("IdUsuario", e.IdUsuario);

            SqlDAO.ExecuteProcedure(operation);
        }
        public override void Delete(BaseDTO baseDTO)
        {
            throw new NotImplementedException();
        }

        public override List<T> RetrieveAll<T>()
        {
            throw new NotImplementedException();
        }

        public override T RetrieveById<T>(int id)
        {
            throw new NotImplementedException();
        }

        public override void Update(BaseDTO baseDTO)
        {
            throw new NotImplementedException();
        }
    }
}
