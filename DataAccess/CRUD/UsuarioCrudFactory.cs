using DataAccess.DAO;
using Entities_DTOs;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace DataAccess.CRUD
{
    public class UsuarioCrudFactory : CrudFactory
    {
        public UsuarioCrudFactory()
        {
            SqlDAO = SqlDAO.GetInstance();
        }

        public  (bool registrado, string mensaje) Registrar(BaseDTO baseDTO)
        {
            var usuario = baseDTO as Usuario;
            var sqlOperation = new SqlOperation();
            sqlOperation.ProcedureName = "sp_RegistrarUsuario";

            sqlOperation.AddStringParam("Cedula", usuario.Cedula);
            sqlOperation.AddStringParam("Nombre", usuario.Nombre);
            sqlOperation.AddStringParam("Apellido", usuario.Apellido);
            sqlOperation.AddStringParam("Correo", usuario.Correo);
            sqlOperation.AddStringParam("Contrasena", usuario.Contrasena);
            sqlOperation.AddStringParam("Telefono", usuario.Telefono);
            
            sqlOperation.AddDateTimeParam("FechaRegistro", DateTime.Now);
            sqlOperation.AddBitOutputParam("Registrado");
            sqlOperation.AddVarCharOutputParam("Mensaje", 100);



         return   SqlDAO.ExecuteNonQueryWithOutput(sqlOperation);
        }

        public Usuario ValidarUsuario(Usuario usuario)
        {
           
            var operation = new SqlOperation();
            operation.ProcedureName = "sp_ValidarUsuario";
            operation.AddStringParam("Correo", usuario.Correo);
            operation.AddStringParam("Contrasena", usuario.Contrasena);

            var lstResults = SqlDAO.ExecuteQueryProcedure(operation);

            if (lstResults.Count > 0)
            {
                var oUsuario = BuildUsuarioValidacion(lstResults[0]);

                // Si IdUsuario es 0, las credenciales no coincidieron
                if (oUsuario.IdUsuario == 0) {
                    return null; }

                return oUsuario;

            }
            return null;
        }

        private Usuario BuildUsuarioValidacion(Dictionary<string, object> row)
        {
            return new Usuario
            {
                IdUsuario = Convert.ToInt32(row["IdUsuario"]),
                Rol = Convert.ToInt32(row["Rol"])
               
            };
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
                //FechaRegistro = (DateTime)row["FechaRegistro"]
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

        //encriptar a sha256 un string
        public static string ConvertirSha256(string texto)
        {
            StringBuilder Sb = new StringBuilder();
            using (SHA256 hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));
                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }
            return Sb.ToString();
        }

        public override void Create(BaseDTO baseDTO)
        {
            throw new NotImplementedException();
        }
    }
}
