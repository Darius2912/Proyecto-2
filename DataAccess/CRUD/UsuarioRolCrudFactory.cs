using DataAccess.DAO;
using Entities_DTOs;
using System;
using System.Collections.Generic;

namespace DataAccess.CRUD
{
    public class UsuarioRolCrudFactory : CrudFactory
    {
        public UsuarioRolCrudFactory()
        {
            SqlDAO = SqlDAO.GetInstance();
        }

        public override void Create(BaseDTO baseDTO)
        {
            var usuarioRol = baseDTO as UsuarioRol;
            var sqlOperation = new SqlOperation();
            sqlOperation.ProcedureName = "CRE_USUARIO_ROL_PR";

            sqlOperation.AddIntParam("IdUsuario", usuarioRol.IdUsuario);
            sqlOperation.AddIntParam("IdRol", usuarioRol.IdRol);

            SqlDAO.ExecuteProcedure(sqlOperation);
        }

        public override void Delete(BaseDTO baseDTO)
        {
            var usuarioRol = baseDTO as UsuarioRol;
            var sqlOperation = new SqlOperation();
            sqlOperation.ProcedureName = "DEL_USUARIO_ROL_PR";

            sqlOperation.AddIntParam("IdUsuarioRol", usuarioRol.IdUsuarioRol);

            SqlDAO.ExecuteProcedure(sqlOperation);
        }

        public override List<T> RetrieveAll<T>()
        {
            var lstResults = new List<T>();
            var operation = new SqlOperation();
            operation.ProcedureName = "RET_ALL_USUARIO_ROL_PR";

            var lstResult = SqlDAO.ExecuteQueryProcedure(operation);

            foreach (var item in lstResult)
            {
                var usuarioRol = BuildUsuarioRol(item);
                lstResults.Add((T)Convert.ChangeType(usuarioRol, typeof(T)));
            }
            return lstResults;
        }

        public override T RetrieveById<T>(int id)
        {
            var operation = new SqlOperation();
            operation.ProcedureName = "RET_USUARIO_ROL_BY_ID_PR";
            operation.AddIntParam("IdUsuarioRol", id);

            var lstResults = SqlDAO.ExecuteQueryProcedure(operation);

            if (lstResults.Count > 0)
            {
                var usuarioRol = BuildUsuarioRol(lstResults[0]);
                return (T)Convert.ChangeType(usuarioRol, typeof(T));
            }
            return default(T);
        }

        public override void Update(BaseDTO baseDTO)
        {
            var usuarioRol = baseDTO as UsuarioRol;
            var sqlOperation = new SqlOperation();
            sqlOperation.ProcedureName = "UPD_USUARIO_ROL_PR";

            sqlOperation.AddIntParam("IdUsuarioRol", usuarioRol.IdUsuarioRol);
            sqlOperation.AddIntParam("IdUsuario", usuarioRol.IdUsuario);
            sqlOperation.AddIntParam("IdRol", usuarioRol.IdRol);

            SqlDAO.ExecuteProcedure(sqlOperation);
        }

        // 🔹 Método auxiliar para asignar rol directamente
        public void AssignRole(int idUsuario, int idRol)
        {
            var sqlOperation = new SqlOperation();
            sqlOperation.ProcedureName = "CRE_USUARIO_ROL_PR";

            sqlOperation.AddIntParam("IdUsuario", idUsuario);
            sqlOperation.AddIntParam("IdRol", idRol);

            SqlDAO.ExecuteProcedure(sqlOperation);
        }

        // Método auxiliar: obtener roles por usuario
        public List<Rol> ObtenerRolesPorUsuario(int idUsuario)
        {
            var operation = new SqlOperation();
            operation.ProcedureName = "RET_ROLES_BY_USUARIO_PR";
            operation.AddIntParam("IdUsuario", idUsuario);

            var lstResult = SqlDAO.ExecuteQueryProcedure(operation);
            var roles = new List<Rol>();

            foreach (var item in lstResult)
            {
                roles.Add(new Rol()
                {
                    IdRol = (int)item["IdRol"],
                    NombreRol = (string)item["NombreRol"]
                });
            }
            return roles;
        }

        private UsuarioRol BuildUsuarioRol(Dictionary<string, object> row)
        {
            return new UsuarioRol()
            {
                IdUsuarioRol = (int)row["IdUsuarioRol"],
                IdUsuario = (int)row["IdUsuario"],
                IdRol = (int)row["IdRol"]
            };
        }
    }
}
