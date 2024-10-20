using Shared.Enums;
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

    // Metod för att spara värden för en produkt, och skicka till CreateProduct-metoden
    // Fick hjälp av ChatGPT för att skriva felsökningen av Category
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

        Console.WriteLine("\n\t Categories: ");
        foreach (var category in Enum.GetValues(typeof(Category)))
        {
            Console.WriteLine($"\t {(int)category} {category}");
        }
        Console.Write("\n\t Choose category (0-7): ");
        string input = Console.ReadLine()!;

        // Kontrollerar att det användaren inmatar är en siffra
        if (int.TryParse(input, out int inputNumber))
        {
            // Kontrollerar att den inmatade siffran är ett tillåtet siffervärde för en av enumsen (0-7)
            if (inputNumber < 0 || inputNumber > 7)
            {
                Console.WriteLine("\n\t Product was not placed in a category. You need to choose a categorynumber between 0-7.");
                Console.Write("\n\t Press any key to continue. ");
            }
            else // Om då siffran är ok (0-7), spara värdet som produktens kategori och skicka iväg till CreateProduct-metoden
            {
                Category selectedCategory = (Category)inputNumber;

                product.Category = selectedCategory;

                var result = _productService.CreateProduct(product);

                // Några av dessa cases är nog överflödiga (exempelvis NoCategorySet pga den tidigare kontrollen)
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
        }

        else // Om inte användaren skrev in en siffra
        {
            Console.WriteLine("\n\t Invalid option! Please pick a valid number between 0-7.");
            Console.Write("\n\t Press any key to continue. ");
        }
    }

    // Metod för att använda GetAllProductsFromList-metoden och skriva ut alla produkter
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
                $"\n\t Price: {product.Price} kr" +
                $"\n\t Category: {product.Category}");
            }
        }
        Console.Write("\n\t Press any key to continue. ");
    }

    // Metod för att skriva ut alla produkter från GetAllProductsFromList-metoden, för att sedan välja en produkt att radera byggt på Id
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
                $"\n\t Price: {product.Price} kr" +
                $"\n\t Category: {product.Category}");
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

    // Metod för att skriva ut alla produkter från GetAllProductsFromList-metoden, för att sedan välja en produkt att uppdatera byggt på Id
    // Använde mig av samma hjälp här från ChatGPT för felsökning av Category
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
                $"\n\t Price: {printProduct.Price} kr" +
                $"\n\t Category: {printProduct.Category}");
            }

            Console.WriteLine("\n\t Please type or copy the product ID you want to update.");
            Console.Write("\n\t Product ID: ");
            string productId = Console.ReadLine() ?? "";

            var product = new Product();
            product.Id = productId;

            Console.Clear();
            Console.WriteLine("\n\t Please type in the new name and price for the product.");

            Console.Write("\n\t Product name: ");
            product.Name = Console.ReadLine() ?? "";

            Console.Write("\t Product price: ");
            decimal.TryParse(Console.ReadLine(), out decimal price);
            price = Math.Round(price, 2);
            product.Price = price;

            Console.WriteLine("\n\t Categories: ");
            foreach (var category in Enum.GetValues(typeof(Category)))
            {
                Console.WriteLine($"\t {(int)category} {category}");
            }
            Console.Write("\n\t Choose category (0-7): ");
            string input = Console.ReadLine()!;

            if (int.TryParse(input, out int inputNumber))
            {
                if (inputNumber < 0 || inputNumber > 7)
                {
                    Console.WriteLine("\n\t Product was not placed in a category. You need to choose a categorynumber between 0-7.");
                    Console.Write("\n\t Press any key to continue. ");
                }
                else
                {
                    Category selectedCategory = (Category)inputNumber;

                    product.Category = selectedCategory;

                    var result = _productService.Update(product);

                    // Även här kan nog några cases vara lite överflödiga
                    switch (result)
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

            else
            {
                Console.WriteLine("\n\t Invalid option! Please pick a valid number between 0-7.");
                Console.Write("\n\t Press any key to continue. ");
            }
        }
    }
}
