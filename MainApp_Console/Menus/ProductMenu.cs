using Shared.Interfaces;
using Shared.Models;

namespace MainApp_Console.Menus;

public class ProductMenu
{
    private readonly IProductService _productService;

    public ProductMenu(IProductService productService)
    {
        _productService = productService;
    }

    public void CreateProductMenu()
    {
        var product = new Product();

        Console.Clear();
        Console.WriteLine("\n\t Please type in the product you want to add to the inventory.");

        Console.Write("\n\t Product name: ");
        product.Name = Console.ReadLine() ?? "";

        Console.Write("\t Product price: ");
        decimal.TryParse(Console.ReadLine(), out decimal price);
        price = Math.Round(price, 2);
        product.Price = price;

        var result = _productService.CreateProduct(product);
        switch (result)
        {
            case Shared.Enums.StatusCodes.Success:
                Console.WriteLine("\n\t Product was added to the inventory.");
                break;

            case Shared.Enums.StatusCodes.Exists:
                Console.WriteLine("\n\t Product with same name already exists in the inventory.");
                break;

            case Shared.Enums.StatusCodes.NoNameSet:
                Console.WriteLine("\n\t No name was given to product.");
                break;

            case Shared.Enums.StatusCodes.NoPriceSet:
                Console.WriteLine("\n\t Product price can't be 0, please set a price.");
                break;

            case Shared.Enums.StatusCodes.NoCategorySet:
                Console.WriteLine("\n\t Product was not placed in a category.");
                break;

            case Shared.Enums.StatusCodes.Failed:
                Console.WriteLine("\n\t Something went wrong. Product was not added to the inventory.");
                break;
        }

        Console.Write("\n\t Press any key to continue. ");
    }

    public void GetAllProductsFromListMenu()
    {
        var products = _productService.GetAllProductsFromList();

        Console.Clear();
        Console.WriteLine("\n\t The inventory: ");

        if (!products.Any())
        {
            Console.WriteLine("\n\t No products in inventory.");
        }
        else
        {
            foreach (var product in products)
            {
                Console.WriteLine($"\n\t Product ID: {product.Id}" +
                $"\n\t Product: {product.Name}" +
                $"\n\t Price: {product.Price} kr");
            }
        }
        Console.Write("\n\t Press any key to continue. ");
    }

    public void DeleteProductMenu()
    {
        var products = _productService.GetAllProductsFromList();

        Console.Clear();
        Console.WriteLine("\n\t The inventory: ");

        if (!products.Any())
        {
            Console.WriteLine("\n\t No products in inventory.");
        }
        else
        {
            foreach (var product in products)
            {
                Console.WriteLine($"\n\t Product ID: {product.Id}" +
                $"\n\t Product: {product.Name}" +
                $"\n\t Price: {product.Price} kr");
            }

            Console.WriteLine("\n\t Please type or copy the Id of the product you want to remove from the inventory.");
            Console.Write("\n\t Product Id: ");
            var productId = Console.ReadLine() ?? "";

            var result = _productService.Delete(productId);

            switch (result)
            {
                case Shared.Enums.StatusCodes.Success:
                    Console.WriteLine("\n\t Product was removed from the inventory.");
                    break;

                case Shared.Enums.StatusCodes.NotFound:
                    Console.WriteLine("\n\t Product does not exist in inventory.");
                    break;

                case Shared.Enums.StatusCodes.Failed:
                    Console.WriteLine("\n\t Something went wrong. Product was not removed.");
                    break;
            }

            Console.Write("\n\t Press any key to continue. ");
        }
    }

    public void UpdateProductMenu()
    {
        var products = _productService.GetAllProductsFromList();

        Console.Clear();
        Console.WriteLine("\n\t The inventory: ");

        if (!products.Any())
        {
            Console.WriteLine("\n\t No products in inventory.");
        }
        else
        {
            foreach (var printProduct in products)
            {
                Console.WriteLine($"\n\t Product ID: {printProduct.Id}" +
                $"\n\t Product: {printProduct.Name}" +
                $"\n\t Price: {printProduct.Price} kr");
            }

            Console.WriteLine("\n\t Please type or copy the product ID you want to update.");
            Console.Write("\n\t Product ID: ");
            string productId = Console.ReadLine() ?? "";

            var product = new Product();

            Console.Clear();
            Console.WriteLine("\n\t Please type in the new name and price for the product.");

            Console.Write("\n\t Product name: ");
            product.Name = Console.ReadLine() ?? "";

            Console.Write("\t Product price: ");
            decimal.TryParse(Console.ReadLine(), out decimal price);
            price = Math.Round(price, 2);
            product.Price = price;

            var updatedProduct = product;

            var result = _productService.Update(product);

            switch(result)
            {
                case Shared.Enums.StatusCodes.Success:
                    Console.WriteLine("\n\t Product was updated successfully.");
                    break;
                
                case Shared.Enums.StatusCodes.NoNameSet:
                    Console.WriteLine("\n\t No name was given to product.");
                    break;

                case Shared.Enums.StatusCodes.NoPriceSet:
                    Console.WriteLine("\n\t Product price can't be 0, please set a price.");
                    break;

                case Shared.Enums.StatusCodes.NoCategorySet:
                    Console.WriteLine("\n\t Product was not placed in a category.");
                    break;

                case Shared.Enums.StatusCodes.Failed:
                    Console.WriteLine("\n\t Something went wrong. Product was not added to the inventory.");
                    break;
            }

            Console.Write("\n\t Press any key to continue. ");
        }
    }
}
