using DataAccess.DAO;

public class PropiedadCrudFactory
{
    private SqlDAO _sqlDAO;

    public PropiedadCrudFactory()
    {
        _sqlDAO = SqlDAO.GetInstance();
    }

    
}