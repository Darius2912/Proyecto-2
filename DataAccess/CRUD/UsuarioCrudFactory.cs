using DataAccess.DAO;
using Entities_DTOs;
using System;
using System.Collections.Generic;

namespace DataAccess.CRUD
{
    public class UsuarioCrudFactory : CrudFactory
    {
        public UsuarioCrudFactory()
        {
            SqlDAO = SqlDAO.GetInstance();
        }

        public override void Create(BaseDTO baseDTO)
        {
            var usuario = baseDTO as Usuario;
            var sqlOperation = new SqlOperation();
            sqlOperation.ProcedureName = "CRE_USUARIO_PR";

            sqlOperation.AddStringParam("Cedula", usuario.Cedula);
            sqlOperation.AddStringParam("Nombre", usuario.Nombre);
            sqlOperation.AddStringParam("Apellido", usuario.Apellido);
            sqlOperation.AddStringParam("Correo", usuario.Correo);
            sqlOperation.AddStringParam("Contrasena", usuario.Contrasena);
            sqlOperation.AddStringParam("Telefono", usuario.Telefono);
            sqlOperation.AddStringParam("Estado", usuario.Estado);
            sqlOperation.AddDateTimeParam("Fecha_Registro", usuario.FechaRegistro);

            SqlDAO.ExecuteProcedure(sqlOperation);
        }

        public override void Delete(BaseDTO baseDTO)
        {
            var usuario = baseDTO as Usuario;
            var sqlOperation = new SqlOperation();
            sqlOperation.ProcedureName = "DEL_USUARIO_PR";
            sqlOperation.AddIntParam("IdUsuario", usuario.IdUsuario);

            SqlDAO.ExecuteProcedure(sqlOperation);
        }

        public override List<T> RetrieveAll<T>()
        {
            var lstResults = new List<T>();
            var operation = new SqlOperation();
            operation.ProcedureName = "RET_ALL_USUARIO_PR";

            var lstResult = SqlDAO.ExecuteQueryProcedure(operation);

            foreach (var item in lstResult)
            {
                var usuario = BuildUsuario(item);
                lstResults.Add((T)Convert.ChangeType(usuario, typeof(T)));
            }
            return lstResults;
        }

        public override T RetrieveById<T>(int id)
        {
            var operation = new SqlOperation();
            operation.ProcedureName = "RET_USUARIO_BY_ID_PR";
            operation.AddIntParam("P_IDUSUARIO", id);

            var lstResults = SqlDAO.ExecuteQueryProcedure(operation);

            if (lstResults.Count > 0)
            {
                var usuario = BuildUsuario(lstResults[0]);
                return (T)Convert.ChangeType(usuario, typeof(T));
            }
            return default(T);
        }

        public override void Update(BaseDTO baseDTO)
        {
            var usuario = baseDTO as Usuario;
            var sqlOperation = new SqlOperation();
            sqlOperation.ProcedureName = "UPD_USUARIO_PR";

            sqlOperation.AddIntParam("P_IDUSUARIO", usuario.IdUsuario);
            sqlOperation.AddStringParam("P_NOMBRE", usuario.Nombre);
            sqlOperation.AddStringParam("P_APELLIDO", usuario.Apellido);
            sqlOperation.AddStringParam("P_CORREO", usuario.Correo);
            sqlOperation.AddStringParam("P_CONTRASENA", usuario.Contrasena);
            sqlOperation.AddStringParam("P_TELEFONO", usuario.Telefono);
            sqlOperation.AddStringParam("P_ESTADO", usuario.Estado);

            SqlDAO.ExecuteProcedure(sqlOperation);
        }

        private Usuario BuildUsuario(Dictionary<string, object> row)
        {
            return new Usuario()
            {
                IdUsuario = (int)row["IdUsuario"],
                Cedula = (string)row["Cedula"],
                Nombre = (string)row["Nombre"],
                Apellido = row.ContainsKey("Apellido") ? (string)row["Apellido"] : null,
                Correo = (string)row["Correo"],
                Contrasena = (string)row["Contrasena"],
                Telefono = row.ContainsKey("Telefono") ? (string)row["Telefono"] : null,
                Estado = row.ContainsKey("Estado") ? (string)row["Estado"] : null,
                FechaRegistro = (DateTime)row["FechaRegistro"]
            };
        }

        public Usuario RetrieveByCorreo(string correo)
        {
            var operation = new SqlOperation();
            operation.ProcedureName = "RET_USUARIO_BY_CORREO_PR";
            operation.AddStringParam("P_CORREO", correo);

            var lstResults = SqlDAO.ExecuteQueryProcedure(operation);

            if (lstResults.Count > 0)
            {
                var usuario = BuildUsuario(lstResults[0]);
                return usuario;
            }
            return null;
        }
        public int CreateAndReturnId(Usuario usuario)
        {
            var sqlOperation = new SqlOperation();
            sqlOperation.ProcedureName = "CRE_USUARIO_PR";

            sqlOperation.AddStringParam("Cedula", usuario.Cedula);
            sqlOperation.AddStringParam("Nombre", usuario.Nombre);
            sqlOperation.AddStringParam("Apellido", usuario.Apellido);
            sqlOperation.AddStringParam("Correo", usuario.Correo);
            sqlOperation.AddStringParam("Contrasena", usuario.Contrasena);
            sqlOperation.AddStringParam("Telefono", usuario.Telefono);

            var result = SqlDAO.ExecuteScalar(sqlOperation);
            return Convert.ToInt32(result);
        }


    }
}
