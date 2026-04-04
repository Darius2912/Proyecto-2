using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DataAccess.DAO
{
    // Patrón Singleton para acceso a la BD
    public class SqlDAO
    {
        private static SqlDAO instance;
        private string connectionString;


        private SqlDAO(){
            connectionString = @"Server=tcp:dbtiendajean.database.windows.net,1433;Initial Catalog=jean-db-tienda;Persist Security Info=False;User ID=jeanrva;Password=D29mayo@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        }

        public static SqlDAO GetInstance()
        {
            if (instance == null)
            {
                instance = new SqlDAO();
            }
            return instance;
        }

<<<<<<< HEAD
        internal static SqlDAO? GetInstance()
        {
            throw new NotImplementedException();
        }

=======
        // 🔹 Ejecuta SP sin retorno de datos
>>>>>>> adb841ee777f14b12b6621b45f091600613b66d1
        public void ExecuteProcedure(SqlOperation operation)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand(operation.ProcedureName, conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    foreach (var param in operation.Parameters)
                    {
                        cmd.Parameters.Add(param);
                    }

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        //con outputs 
        public (bool registrado, string mensaje) ExecuteNonQueryWithOutput(SqlOperation operation)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand(operation.ProcedureName, conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    foreach (var param in operation.Parameters)
                    {
                        cmd.Parameters.Add(param);
                    }

                    conn.Open();
                    cmd.ExecuteNonQuery();

                    bool registrado = Convert.ToBoolean(cmd.Parameters["Registrado"].Value);
                    string mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                    return (registrado, mensaje);
                }
            }
        }




        // 🔹 Ejecuta SP que retorna un único valor (ej. SCOPE_IDENTITY)
        public object ExecuteScalar(SqlOperation operation)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand(operation.ProcedureName, conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    foreach (var param in operation.Parameters)
                    {
                        cmd.Parameters.Add(param);
                    }

                    conn.Open();
                    return cmd.ExecuteScalar(); // Devuelve el primer valor de la primera fila
                }
            }
        }

        // 🔹 Ejecuta SP que retorna múltiples filas
        public List<Dictionary<string, object>> ExecuteQueryProcedure(SqlOperation operation)
        {
            var lstResults = new List<Dictionary<string, object>>();

            using (var conn = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand(operation.ProcedureName, conn)
                {
                    CommandType = CommandType.StoredProcedure
                })
                {
                    foreach (var param in operation.Parameters)
                    {
                        cmd.Parameters.Add(param);
                    }

                    conn.Open();
                    var reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var row = new Dictionary<string, object>();
                            for (var index = 0; index < reader.FieldCount; index++)
                            {
                                var key = reader.GetName(index);
                                var value = reader.GetValue(index);
                                row[key] = value;
                            }
                            lstResults.Add(row);
                        }
                    }
                }
            }

            return lstResults;
        }
    }
}
