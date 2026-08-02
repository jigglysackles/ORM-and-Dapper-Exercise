using System.Data;
using Dapper;

namespace ORM_Dapper;

public class DapperProductRepository : IProductRepository
{
    private readonly IDbConnection _conn;

    public DapperProductRepository(IDbConnection conn)
    {
        _conn = conn;
    }

    public IEnumerable<Product> GetAllProducts()
    {
        return _conn.Query<Product>(@"SELECT * FROM Products");
    }

    public void CreateProduct(string name, decimal price, int categoryId, int onSale, int stockLevel)
    {
        _conn.Execute(
            @"INSERT INTO products (Name, Price, CategoryID, OnSale, StockLevel) VALUES (@name, @price, @categoryId, @onSale, @stockLevel);",
            new { name, price, categoryId, onSale, stockLevel });
    }


    public void UpdateProduct(int productId, string name, decimal price, int categoryId, int onSale, int stockLevel)
    {
        _conn.Execute(
            @"UPDATE products SET 
                    Name = @name, 
                    Price = @price, 
                    CategoryID = @categoryId, 
                    OnSale = @onSale, 
                    StockLevel = @stockLevel 
                WHERE 
                    ProductID = @productID",
            new { productId, name, price, categoryId, onSale, stockLevel });
    }


    public void DeleteProduct(int productId)
    {
        _conn.Execute(@"DELETE FROM reviews WHERE ProductID = @productId", new { productId });
        _conn.Execute(@"DELETE FROM sales WHERE ProductID = @productId", new { productId });
        _conn.Execute(@"DELETE FROM products WHERE ProductID = @productId", new { productId });
    }
}