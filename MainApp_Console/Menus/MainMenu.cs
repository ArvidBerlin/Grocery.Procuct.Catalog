using Shared.Interfaces;

namespace MainApp_Console.Menus;

public class MainMenu
{
    private readonly IProductService _productService;
    private readonly ProductMenu _productMenu;

    public MainMenu(IProductService productService, ProductMenu productMenu)
    {
        _productService = productService;
        _productMenu = productMenu;
    }

    public void PrintMainMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("\n\t What do you want to do today?");
            Console.WriteLine("\n\t 1 - Add product to inventory");
            Console.WriteLine("\t 2 - List all products");
            Console.WriteLine("\t 3 - Remove a product from inventory");
            Console.WriteLine("\t 4 - Update existing product name and price");
            Console.WriteLine("\t 0 - Exit");

            Console.Write("\n\t Enter option: ");

            var result = MenuOptions(Console.ReadLine() ?? "", _productMenu);
            if (!result)
            {
                Console.Clear();
                Console.WriteLine("\n\t Invalid option! Please pick a menu option between 1-4, " +
                    "\n\t or exit the application with 0.");
                Console.Write("\n\t Press any key to continue. ");
                Console.ReadKey();
            }
        }  
    }

    public void ExitApplicationMenu()
    {
        Console.Clear();
        Console.WriteLine("\n\t Are you sure you want to exit the inventory? (y/n)");
        Console.Write("\n\t ");
        var response = Console.ReadLine() ?? "n";
        if (response.ToLower() == "y")
        {
            Environment.Exit(0);
        }
    }

    public bool MenuOptions(string selectedOption, ProductMenu productMenu)
    {
        if (int.TryParse(selectedOption, out int option))
        {
            switch (option)
            {
                case 1:
                    productMenu.CreateProductMenu();
                    Console.ReadKey();
                    break;

                case 2:
                    productMenu.GetAllProductsFromListMenu();
                    Console.ReadKey();
                    break;

                case 3:
                    productMenu.DeleteProductMenu();
                    Console.ReadKey();
                    break;

                case 4:
                    productMenu.UpdateProductMenu();
                    Console.ReadKey();
                    break;

                case 0:
                    ExitApplicationMenu();
                    break;

                default:
                    return false;
            }

            return true;
        }

        return false;
    }
}
