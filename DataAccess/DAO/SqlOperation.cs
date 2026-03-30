using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
   


        public class SqlOperation
        {
            public string ProcedureName { get; set; }
            public List<SqlParameter> Parameters { get; set; }
            public SqlOperation()
            {
                Parameters = new List<SqlParameter>();
            }


            public void AddStringParam(string paramName, string paramValue)
            {
                Parameters.Add(new SqlParameter(paramName, paramValue));
            }
            public void AddDateOnlyParam(string paramName, DateOnly paramValue)
            {
                Parameters.Add(new SqlParameter(paramName, paramValue));
            }

       





        public void AddIntParam(string paramName, int paramValue)
            {
                Parameters.Add(new SqlParameter(paramName, paramValue));
            }

            public void AddDoubleParam(string paramName, double paramValue)
            {
                Parameters.Add(new SqlParameter(paramName, paramValue));
            }


            public void AddDateTimeParam(string paramName, DateTime paramValue)
            {
                Parameters.Add(new SqlParameter(paramName, paramValue));
            }

            public void AddDecimalParam(string paramName, decimal paramValue)
            {
                Parameters.Add(new SqlParameter(paramName, paramValue));
            }

        //parametros output que seran llenados con lo retornado de la base de datos
        public void AddBitOutputParam(string paramName)
        {
            var param = new SqlParameter(paramName, SqlDbType.Bit);
            param.Direction = ParameterDirection.Output;
            Parameters.Add(param);
        }


        public void AddVarCharOutputParam(string paramName, int size)
        {
            var param = new SqlParameter(paramName, SqlDbType.VarChar, size);
            param.Direction = ParameterDirection.Output;
            Parameters.Add(param);
        }


    }
    }
