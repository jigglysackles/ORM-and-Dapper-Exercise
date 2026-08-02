using System.Data;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace ORM_Dapper;

public class Program
{
    private static void Main(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var connString = config.GetConnectionString("DefaultConnection");

        IDbConnection conn = new MySqlConnection(connString);

        var departmentRepo = new DapperDepartmentRepository(conn);
        var productRepo = new DapperProductRepository(conn);

        departmentRepo.InsertDepartment("Basement Department");

        var departments = departmentRepo.GetAllDepartments();
        Console.Clear();
        foreach (var department in departments)
        {
            Console.WriteLine(department.DepartmentID);
            Console.WriteLine(department.Name);
            Console.WriteLine();
        }

        Console.WriteLine("---------------------");
        Console.WriteLine("Creating a new product \"Basement Product\"");
        productRepo.CreateProduct("Basement Product", 0m, 1, 0, 0);

        var products = productRepo.GetAllProducts();

        Console.WriteLine();

        foreach (var product in products)
        {
            Console.WriteLine(product.ProductID);
            Console.WriteLine(product.Name);
            Console.WriteLine();
        }

        Console.WriteLine("---------------------");

        var isolatedProductID = 0;

        foreach (var product in products)
            if (product.Name == "Basement Product")
            {
                isolatedProductID = product.ProductID;
                Console.WriteLine(
                    $"Product with ID {isolatedProductID} has been isolated and has the following properties:");
                Console.WriteLine($"Product ID: {product.ProductID}");
                Console.WriteLine($"Product Name: {product.Name}");
                product.CategoryID = 1;
                Console.WriteLine($"Category ID: {product.CategoryID}");
                product.StockLevel = 1;
                Console.WriteLine($"Stock Level: {product.StockLevel}");
                product.OnSale = 1;
                Console.WriteLine($"On Sale: {product.OnSale}");
                product.Price = 1;
                Console.WriteLine($"Price: {product.Price}");
            }
            else
            {
                Console.WriteLine(isolatedProductID);
            }

        Console.WriteLine("---------------------");
        var yayUpdate = false;

        while (!yayUpdate)
        {
            Console.WriteLine("Shall I update the product with predetermined information? y/n");
            var readResultProductUpdate = Console.ReadKey();
            if (readResultProductUpdate.Key == ConsoleKey.Y)
            {
                yayUpdate = true;
                productRepo.UpdateProduct(isolatedProductID, "Basement Product", 2.00m, 1, 0, 55);
            }
            else
            {
                Console.WriteLine("Well that leaves the product at it's default. Probably ought to update it");
            }
        }

        products = productRepo.GetAllProducts();
        Console.WriteLine("The product is now as follows:");
        foreach (var product in products)
            if (product.Name == "Basement Product")
            {
                Console.WriteLine($"Product ID: {product.ProductID}");
                Console.WriteLine($"Name: {product.Name}");
                Console.WriteLine($"Category ID: {product.CategoryID}");
                Console.WriteLine($"Stock Level: {product.StockLevel}");
                Console.WriteLine($"On Sale: {product.OnSale}");
                Console.WriteLine($"Price: {product.Price}");
            }

        Console.WriteLine("---------------------");
        var yayDelete = false;

        while (!yayDelete)
        {
            Console.WriteLine("Shall I nuke the product from orbit? y/n");
            var readResultProductDelete = Console.ReadKey();
            if (readResultProductDelete.Key == ConsoleKey.Y)
            {
                yayDelete = true;
                productRepo.DeleteProduct(isolatedProductID);
            }
            else
            {
                Console.WriteLine("That's probably for the best but we want this thing gone anyways so too bad. ");
                yayDelete = true;
                productRepo.DeleteProduct(isolatedProductID);
            }

            products = productRepo.GetAllProducts();
            Console.WriteLine("The product is now as follows:");
            foreach (var product in products)
                if (product.Name == "Basement Product")
                {
                    Console.WriteLine($"Product ID: {product.ProductID}");
                    Console.WriteLine($"Name: {product.Name}");
                    Console.WriteLine($"Category ID: {product.CategoryID}");
                    Console.WriteLine($"Stock Level: {product.StockLevel}");
                    Console.WriteLine($"On Sale: {product.OnSale}");
                    Console.WriteLine($"Price: {product.Price}");
                }
        }
    }
}