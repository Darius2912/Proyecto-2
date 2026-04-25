using DataAccess.CRUD;
using DataAccess.DAO;
using Entities_DTOs;

public class PropiedadCrudFactory : CrudFactory
{
   

    public PropiedadCrudFactory()
    {
        SqlDAO = SqlDAO.GetInstance();
    }

    public override void Create(BaseDTO baseDTO)
    {
        throw new NotImplementedException();
    }

    public int CreateAndReturnId(PropiedadDTO propiedad)
    {
        var sqlOperation = new SqlOperation();
        sqlOperation.ProcedureName = "sp_InsertarPropiedad";

        sqlOperation.AddIntParam("IdUsuario", propiedad.IdUsuario);

        sqlOperation.AddStringParam("NombreFinca", propiedad.NombreFinca);
        sqlOperation.AddStringParam("Ubicacion", propiedad.Ubicacion);
        sqlOperation.AddDoubleParam("Latitud", propiedad.Latitud);
        sqlOperation.AddDoubleParam("Longitud", propiedad.Longitud);
        sqlOperation.AddDecimalParam("TamanoHectareas", propiedad.TamanoHectareas);
        sqlOperation.AddIntParam("TipoSuperficie", propiedad.TipoSuperficie);
        sqlOperation.AddBoolParam("TieneRio", propiedad.TieneRio);
        sqlOperation.AddStringParam("Provincia", propiedad.Provincia);
        sqlOperation.AddStringParam("Canton", propiedad.Canton);
        sqlOperation.AddStringParam("Distrito", propiedad.Distrito);
        sqlOperation.AddBoolParam("Nacientes", propiedad.Nacientes);
        sqlOperation.AddIntParam("CantidadNacientes", propiedad.CantidadNacientes ?? 0);
        sqlOperation.AddIntParam("TipoVegetacion", propiedad.TipoVegetacion);
        sqlOperation.AddStringParam("UsoSuelo", propiedad.UsoSuelo);
        sqlOperation.AddStringParam("Estado", propiedad.Estado);
        sqlOperation.AddDateTimeParam("FechaRegistro", propiedad.FechaRegistro);
       

        var result = SqlDAO.ExecuteScalar(sqlOperation);
        return Convert.ToInt32(result);
    }

    public void Update(PropiedadDTO propiedad)
    {
        var op = new SqlOperation();
        op.ProcedureName = "sp_ActualizarPropiedadDesdeEvaluacion";

        op.AddIntParam("Id", propiedad.Id);
        op.AddDecimalParam("TamanoHectareas", propiedad.TamanoHectareas);

        // 🔥 FIX
        int tipoSuperficieId = propiedad.TipoSuperficie;

        op.AddIntParam("TipoSuperficie", tipoSuperficieId);
        int tipoVegetacionId = propiedad.TipoVegetacion;

        op.AddIntParam("TipoVegetacion", tipoVegetacionId);
        op.AddBoolParam("TieneRio", propiedad.TieneRio);
        op.AddBoolParam("Nacientes", propiedad.Nacientes);
        op.AddIntParam("CantidadNacientes", propiedad.CantidadNacientes ?? 0);
        op.AddStringParam("UsoSuelo", propiedad.UsoSuelo);

        SqlDAO.GetInstance().ExecuteProcedure(op);
    }

    public void CreateFoto(int propiedadId, string rutaFoto)
    {
        var sqlOperation = new SqlOperation();
        sqlOperation.ProcedureName = "sp_InsertarPropiedadFoto";

        sqlOperation.AddIntParam("PropiedadId", propiedadId);
        sqlOperation.AddStringParam("RutaFoto", rutaFoto);

        SqlDAO.ExecuteProcedure(sqlOperation);
    }

    public List<PropiedadDTO> RetrieveByUsuario(int idUsuario)
    {
        var lista = new List<PropiedadDTO>();

        var operation = new SqlOperation();
        operation.ProcedureName = "ObtenerPropiedadesPorUsuario";
        operation.AddIntParam("IdUsuario", idUsuario);

        var results = SqlDAO.ExecuteQueryProcedure(operation);

        foreach (var row in results)
        {
            var propiedad = BuildObject(row);
            lista.Add(propiedad);
        }

        return lista;
    }

    public List<PropiedadDTO> RetrieveApprovedPropertiesByUsuarioId(int idUsuario)
    {
        var lista = new List<PropiedadDTO>();

        var operation = new SqlOperation();
        operation.ProcedureName = "SP_PROPIEDADES_BYID_APROBADAS";
        operation.AddIntParam("IdUsuario", idUsuario);

        var results = SqlDAO.ExecuteQueryProcedure(operation);

        foreach (var row in results)
        {
            var propiedad = buildPropertyApproved(row);
            lista.Add(propiedad);
        }

        return lista;
    }

    public List<string> ObtenerProvincias()
    {
        var lista = new List<string>();

        var operation = new SqlOperation();
        operation.ProcedureName = "sp_ObtenerProvincias";

        var results = SqlDAO.ExecuteQueryProcedure(operation);

        foreach (var row in results)
        {
            lista.Add(row["Provincia"].ToString());
        }

        return lista;
    }

    public List<string> ObtenerCantones(string provincia)
    {
        var lista = new List<string>();

        var operation = new SqlOperation();
        operation.ProcedureName = "sp_ObtenerCantones";
        operation.AddStringParam("Provincia", provincia);

        var results = SqlDAO.ExecuteQueryProcedure(operation);

        foreach (var row in results)
        {
            lista.Add(row["Canton"].ToString());
        }

        return lista;
    }

    public List<string> ObtenerDistritos(string canton)
    {
        var lista = new List<string>();

        var operation = new SqlOperation();
        operation.ProcedureName = "sp_ObtenerDistritos";
        operation.AddStringParam("Canton", canton);

        var results = SqlDAO.ExecuteQueryProcedure(operation);

        foreach (var row in results)
        {
            lista.Add(row["Distrito"].ToString());
        }

        return lista;
    }

    public override void Delete(BaseDTO baseDTO)
    {
        throw new NotImplementedException();
    }

    public override List<T> RetrieveAll<T>()
    {
        throw new NotImplementedException();
    }

    public override T RetrieveById<T>(int id)
    {
        throw new NotImplementedException();
    }

    public PropiedadDTO RetrieveById(int id)
    {
        var operation = new SqlOperation();
        operation.ProcedureName = "ObtenerPropiedadPorId";
        operation.AddIntParam("Id", id);

        var results = SqlDAO.ExecuteQueryProcedure(operation);

        if (results.Count == 0)
            return null;

        var propiedad = BuildObject(results[0]);

        // 🔥 AQUÍ AGREGAS LAS FOTOS
        var opFotos = new SqlOperation();
        opFotos.ProcedureName = "sp_ObtenerFotosPorPropiedad";
        opFotos.AddIntParam("PropiedadId", id);

        var fotosResult = SqlDAO.ExecuteQueryProcedure(opFotos);

        foreach (var row in fotosResult)
        {
            if (row.ContainsKey("RutaFoto") && row["RutaFoto"] != DBNull.Value)
            {
                propiedad.Fotos.Add(row["RutaFoto"].ToString());
            }
        }

        return propiedad;
    }


    public override void Update(BaseDTO baseDTO)
    {
        throw new NotImplementedException();
    }

    public List<PropiedadDTO> RetrieveByEstado(string Estado)
    {
        var lista = new List<PropiedadDTO>();

        var operation = new SqlOperation();
        operation.ProcedureName = "ObtenerPropiedadesPorEstado";
        operation.AddStringParam("Estado", Estado);

        var results = SqlDAO.ExecuteQueryProcedure(operation);

        foreach (var row in results)
        {
            var propiedad = buildPropiedadEstado(row);
            lista.Add(propiedad);
        }

        return lista;
    }

    private PropiedadDTO BuildObject(Dictionary<string, object> row)
    {
        return new PropiedadDTO
        {
            Id = Convert.ToInt32(row["Id"]),
            NombreFinca = (string)row["NombreFinca"],
            Ubicacion = (string)row["Ubicacion"],
            TamanoHectareas = Convert.ToDecimal(row["TamanoHectareas"]),
            Estado = (string)row["Estado"],
            Observaciones = row.ContainsKey("Observaciones") && row["Observaciones"] != DBNull.Value
                ? row["Observaciones"].ToString() : null,
            TipoSuperficie = (int)row["TipoSuperficie"],
            TieneRio = row.ContainsKey("TieneRio") && row["TieneRio"] != DBNull.Value
    ? Convert.ToBoolean(row["TieneRio"])
    : false,
            Nacientes = row.ContainsKey("Nacientes") && row["Nacientes"] != DBNull.Value
             ? row["Nacientes"].ToString() == "Si" : false,
            CantidadNacientes = row.ContainsKey("CantidadNacientes") && row["CantidadNacientes"] != DBNull.Value
                ? Convert.ToInt32(row["CantidadNacientes"]) : 0,
            TipoVegetacion = (int)row["TipoVegetacion"],
            UsoSuelo = (string)row["UsoSuelo"],
            CorreoUsuario = (string)row["CorreoUsuario"]
        };
    }

    private PropiedadDTO buildPropiedadEstado(Dictionary<string, object> row)
    {
        return new PropiedadDTO
        {
            Id = (int)row["Id"],
            NombreFinca = (string)row["NombreFinca"],
            Ubicacion = (string)row["Ubicacion"],
            TamanoHectareas = (decimal)row["TamanoHectareas"],
            Estado = (string)row["Estado"],
        };
    }

    private PropiedadDTO buildPropertyApproved(Dictionary<string, object> row)
    {
        return new PropiedadDTO
        {
           NombreFinca = (string)row["NombreFinca"],
           Provincia = (string)row["Provincia"],
            Canton = (string)row["Canton"],
            Distrito = (string)row["Distrito"],
            TamanoHectareas = (decimal)row["TamanoHectareas"],
            TipoSuperficie = (int)row["TipoSuperficie"],
            TieneRio = (bool)row["TieneRio"],
            Nacientes = (bool)row["Nacientes"],
            CantidadNacientes = (int)row["CantidadNacientes"],
            TipoVegetacion = (int)row["TipoVegetacion"],
            UsoSuelo = (string)row["UsoSuelo"],
            Estado = (string)row["Estado"]
        };
    }

}