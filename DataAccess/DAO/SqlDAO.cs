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
            connectionString = @"Data Source=JEAN\SQLEXPRESS;Initial Catalog=Proyecto2;Integrated Security=True;Trust Server Certificate=True";
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
