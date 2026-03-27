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

        private SqlDAO()
        {
            connectionString = @"Data Source=DESKTOP-U50R978;Initial Catalog=SistemaPSA;Integrated Security=True;Trust Server Certificate=True";
        }

        public static SqlDAO GetInstance()
        {
            if (instance == null)
            {
                instance = new SqlDAO();
            }
            return instance;
        }

        // 🔹 Ejecuta SP sin retorno de datos
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
