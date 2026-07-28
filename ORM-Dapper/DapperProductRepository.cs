namespace ORM_Dapper;

public class DapperProductRepository : IProductRepository
{
    public IEnumerable<Product> GetAllProducts()
    {
        throw new NotImplementedException();
    }

    public void CreateProduct(string name, double price, int categoryID)
    {
        throw new NotImplementedException();
    }

    public void UpdateProduct(string name, double price, int categoryID)
    {
        throw new NotImplementedException();
    }

    public void DeleteProduct(int productID)
    {
        throw new NotImplementedException();
    }
    
}