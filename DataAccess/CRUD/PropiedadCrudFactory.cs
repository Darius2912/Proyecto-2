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
        sqlOperation.AddStringParam("TipoSuperficie", propiedad.TipoSuperficie);
        sqlOperation.AddStringParam("TieneRio", propiedad.TieneRio);
        sqlOperation.AddStringParam("Nacientes", propiedad.Nacientes);
        sqlOperation.AddIntParam("CantidadNacientes", propiedad.CantidadNacientes ?? 0);
        sqlOperation.AddStringParam("TipoVegetacion", propiedad.TipoVegetacion);
        sqlOperation.AddStringParam("UsoSuelo", propiedad.UsoSuelo);
        sqlOperation.AddStringParam("Estado", propiedad.Estado);
        sqlOperation.AddDateTimeParam("FechaRegistro", propiedad.FechaRegistro);
       

        var result = SqlDAO.ExecuteScalar(sqlOperation);
        return Convert.ToInt32(result);
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

        if (results.Count > 0)
        {
            return BuildObject(results[0]);
        }

        return null;
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
            var propiedad = BuildObject(row);
            lista.Add(propiedad);
        }

        return lista;
    }

    private PropiedadDTO BuildObject(Dictionary<string, object> row)
    {
        return new PropiedadDTO
        {
            Id = Convert.ToInt32(row["Id"]),
            NombreFinca = row["NombreFinca"].ToString(),
            Ubicacion = row["Ubicacion"].ToString(),
            TamanoHectareas = Convert.ToDecimal(row["TamanoHectareas"]),
            Estado = row["Estado"].ToString(),
            Observaciones = row.ContainsKey("Observaciones") && row["Observaciones"] != DBNull.Value
            ? row["Observaciones"].ToString()
            : null
        };
    }
}