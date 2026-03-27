using DataAccess.DAO;
using Entities_DTOs;
using System;
using System.Collections.Generic;

namespace DataAccess.CRUD
{
    public class RolCrudFactory : CrudFactory
    {
        public RolCrudFactory()
        {
            SqlDAO = SqlDAO.GetInstance();
        }

        public override void Create(BaseDTO baseDTO)
        {
            var rol = baseDTO as Rol;
            var sqlOperation = new SqlOperation();
            sqlOperation.ProcedureName = "CRE_ROL_PR";

            sqlOperation.AddStringParam("NombreRol", rol.NombreRol);

            SqlDAO.ExecuteProcedure(sqlOperation);
        }

        public override void Delete(BaseDTO baseDTO)
        {
            var rol = baseDTO as Rol;
            var sqlOperation = new SqlOperation();
            sqlOperation.ProcedureName = "DEL_ROL_PR";

            sqlOperation.AddIntParam("IdRol", rol.IdRol);

            SqlDAO.ExecuteProcedure(sqlOperation);
        }

        public override List<T> RetrieveAll<T>()
        {
            var lstResults = new List<T>();
            var operation = new SqlOperation();
            operation.ProcedureName = "RET_ALL_ROL_PR";

            var lstResult = SqlDAO.ExecuteQueryProcedure(operation);

            foreach (var item in lstResult)
            {
                var rol = BuildRol(item);
                lstResults.Add((T)Convert.ChangeType(rol, typeof(T)));
            }
            return lstResults;
        }

        public override T RetrieveById<T>(int id)
        {
            var operation = new SqlOperation();
            operation.ProcedureName = "RET_ROL_BY_ID_PR";
            operation.AddIntParam("IdRol", id);

            var lstResults = SqlDAO.ExecuteQueryProcedure(operation);

            if (lstResults.Count > 0)
            {
                var rol = BuildRol(lstResults[0]);
                return (T)Convert.ChangeType(rol, typeof(T));
            }
            return default(T);
        }

        public override void Update(BaseDTO baseDTO)
        {
            var rol = baseDTO as Rol;
            var sqlOperation = new SqlOperation();
            sqlOperation.ProcedureName = "UPD_ROL_PR";

            sqlOperation.AddIntParam("IdRol", rol.IdRol);
            sqlOperation.AddStringParam("NombreRol", rol.NombreRol);

            SqlDAO.ExecuteProcedure(sqlOperation);
        }

        private Rol BuildRol(Dictionary<string, object> row)
        {
            return new Rol()
            {
                IdRol = (int)row["IdRol"],
                NombreRol = (string)row["NombreRol"]
            };
        }
    }
}
