namespace ORM_Dapper;

public interface IProductRepository
{
    IEnumerable<Product> GetAllProducts();

    void CreateProduct(string name, decimal price, int categoryId, int onSale, int stockLevel);
}