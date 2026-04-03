using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    internal class SqlDAO
    {

        //unica instancia de sqldao

        private static SqlDAO instance;

        private string connectionString;


        private SqlDAO(){
            connectionString = @"Server=tcp:dbtiendajean.database.windows.net,1433;Initial Catalog=jean-db-tienda;Persist Security Info=False;User ID=jeanrva;Password=D29mayo@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        }

        public static SqlDAO getInstance() {
            if (instance == null) {
                instance = new SqlDAO();
            }
            return instance;

        
        }



        public void ExecuteProcedure(SqlOperation operation)
        {
           
            using (var conn = new SqlConnection(connectionString))
            {
                
                using (var cmd = new SqlCommand(operation.ProcedureName, conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
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


        public List<Dictionary<string, object>> ExecuteQueryProcedure(SqlOperation operation)
        {
            var listaResultados = new List<Dictionary<string, object>>();
            using (var conn = new SqlConnection(connectionString))
            {
                //PARAMETROS STORED PROCEDURE Y LA CONEXION QUE USAMOS
                using (var cmd = new SqlCommand(operation.ProcedureName, conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {
                    //SET DE PARAMETROS
                    foreach (var param in operation.Parameters)
                    {
                        cmd.Parameters.Add(param);

                    }
                    //ejecutar el SP contra la base de datos
                    conn.Open();
                    //ejecucion del SP que retorna data desde la base de datos
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
                            listaResultados.Add(row);
                        }
                        ;

                    }

                }

            }
            return listaResultados;
        }



    }
}
