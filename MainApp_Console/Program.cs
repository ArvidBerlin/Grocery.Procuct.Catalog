using MainApp_Console.Menus;
using Microsoft.Extensions.DependencyInjection;
using Shared.Interfaces;
using Shared.Services;
using System;

namespace MainApp_Console;
class Program
{
    private static IServiceProvider? _serviceProvider;

    public static void Main(string[] args)
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);

        _serviceProvider = serviceCollection.BuildServiceProvider();

        var menuService = _serviceProvider!.GetRequiredService<MainMenu>();
        menuService.PrintMainMenu(); // Startar applikationen vid menyutskriften
    }

    static void ConfigureServices(IServiceCollection services)
    {
        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var filePath = Path.Combine(baseDirectory, "products.json");

        services.AddSingleton<IProductService, ProductService>();
        services.AddSingleton<IFileService>(new FileService(filePath));
        services.AddSingleton<MainMenu>();
        services.AddSingleton<ProductMenu>();
    }
}