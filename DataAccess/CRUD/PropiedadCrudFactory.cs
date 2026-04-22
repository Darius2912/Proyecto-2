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

    public override void Update(BaseDTO baseDTO)
    {
        throw new NotImplementedException();
    }
}