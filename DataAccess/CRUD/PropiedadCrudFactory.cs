using DataAccess.DAO;
using Entities_DTOs;

public class PropiedadCrudFactory
{
    private SqlDAO _sqlDAO;

    public PropiedadCrudFactory()
    {
        _sqlDAO = SqlDAO.GetInstance();
    }

    
}