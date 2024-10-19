using MainApp.ViewModels;
using MainApp.Views;
using Microsoft.Extensions.DependencyInjection;
using Shared.Interfaces;
using Shared.Services;
using System.Globalization;
using System.IO;
using System.Windows;

namespace MainApp;

public partial class App : Application
{
    public static IServiceProvider? ServiceProvider { get; private set; }

    private void ConfigureServices(IServiceCollection services)
    {
        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        var filePath = Path.Combine(baseDirectory, "products.json");

        services.AddSingleton<IProductService, ProductService>();
        services.AddSingleton<IFileService>(new FileService(filePath));

        services.AddSingleton<MainWindowViewModel>();
        services.AddSingleton<MainWindow>();

        services.AddTransient<HomeViewModel>();
        services.AddTransient<HomeView>();

        services.AddTransient<CreateProductViewModel>();
        services.AddTransient<CreateProductView>();

        services.AddTransient<EditProductViewModel>();
        services.AddTransient<EditProductView>();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);

        ServiceProvider = serviceCollection.BuildServiceProvider();

        var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
        mainWindow.DataContext = ServiceProvider.GetRequiredService<MainWindowViewModel>();
        mainWindow.Show();

        base.OnStartup(e);
    }
}
