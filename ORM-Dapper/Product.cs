namespace ORM_Dapper;

public class Product
{
    public int ProductID { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string CategoryID { get; set; }
    public string OnSale { get; set; }
    public string StockLevel { get; set; }
    
}