using DataAccess.DAO;
using Entities_DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CRUD
{
    public class RecuperacionContrasenaCrudFactory : CrudFactory
    {



        public RecuperacionContrasenaCrudFactory() {
            SqlDAO = SqlDAO.GetInstance();
        }



        public override void Create(BaseDTO baseDTO)
        {
            var recuperaraContrasena = baseDTO as RecuperacionContrasenaDTO;
            var sqlOperation = new SqlOperation();
            sqlOperation.ProcedureName = "SP_CREATE_TOKEN";

            sqlOperation.AddStringParam("Correo", recuperaraContrasena.Correo);
            sqlOperation.AddStringParam("Token", recuperaraContrasena.Token);
            sqlOperation.AddDateTimeParam("FechaExpira", recuperaraContrasena.FechaExpira);
            sqlOperation.AddBoolParam("Usado", recuperaraContrasena.Usado);


            SqlDAO.ExecuteProcedure(sqlOperation);
        }

        public override void Delete(BaseDTO baseDTO)
        {
            throw new NotImplementedException();
        }

        public override List<T> RetrieveAll<T>()
        {
            throw new NotImplementedException();
        }

        public RecuperacionContrasenaDTO ObtenerPorToken(string token)
        {
            var operation = new SqlOperation();
            operation.ProcedureName = "SP_RECOVER_PW_BY_TOKEN_ID";
            operation.AddStringParam("Token", token);

            var lstResults = SqlDAO.ExecuteQueryProcedure(operation);

            if (lstResults == null || lstResults.Count == 0)
                return null;

            return BuildToken(lstResults[0]);
        }

        private RecuperacionContrasenaDTO BuildToken(Dictionary<string, object> row)
        {
            return new RecuperacionContrasenaDTO()
            {
                Id = (int)row["Id"],
                Correo = (string)row["Correo"],
                Token = (string)row["Token"],
                FechaExpira = (DateTime)row["FechaExpira"],
                Usado = Convert.ToBoolean(row["Usado"])
            };
        }

        public void MarcarUsado(int id) {
            var operation = new SqlOperation();
            operation.ProcedureName = "SP_MARCAR_TOKEN_USADO";
            operation.AddIntParam("Id", id);

             SqlDAO.ExecuteProcedure(operation);
        }


        public override void Update(BaseDTO baseDTO)
        {
            throw new NotImplementedException();
        }

        public override T RetrieveById<T>(int id)
        {
           
            return default(T);
        }

    }
}
