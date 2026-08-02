namespace ORM_Dapper;

public interface ISalesRepository
{
    IEnumerable<Sales> GetAllSales();
}