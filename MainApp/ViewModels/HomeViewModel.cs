using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Shared.Interfaces;
using Shared.Models;
using System.Collections.ObjectModel;
using System.Net.Http.Headers;

namespace MainApp.ViewModels;

public partial class HomeViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IProductService _productService;

    public HomeViewModel(IServiceProvider serviceProvider, IProductService productService)
    {
        _serviceProvider = serviceProvider;
        _productService = productService;
        UpdateProducts();
    }

    [ObservableProperty]
    private ObservableCollection<Product> products = [];

    // Kommando med metod för att gå till "Create Produkt"-sidan
    [RelayCommand]
    public void Create()
    {
        var viewModel = _serviceProvider.GetRequiredService<MainWindowViewModel>();
        viewModel.CurrentViewModel = _serviceProvider.GetRequiredService<CreateProductViewModel>();
    }

    // Kommando med metod för att gå till "Startup"-sidan
    [RelayCommand]
    public void Back()
    {
        var viewModel = _serviceProvider.GetRequiredService<MainWindowViewModel>();
        viewModel.CurrentViewModel = _serviceProvider.GetRequiredService<StartupViewModel>();
    }

    // Kommando med metod för att gå till "Edit Product"-sidan
    [RelayCommand]
    public void Edit(Product product)
    {
        var editProductViewModel = _serviceProvider.GetRequiredService<EditProductViewModel>();
        editProductViewModel.Product = product;

        var viewModel = _serviceProvider.GetRequiredService<MainWindowViewModel>();
        viewModel.CurrentViewModel = editProductViewModel;
    }

    // Kommando med metod för att ta bort en produkt och sedan uppdatera listan
    [RelayCommand]
    public void Delete(string id)
    {
        _productService.Delete(id);
        UpdateProducts();
    }

    // Metod som uppdaterar listan
    public void UpdateProducts()
    {
        Products.Clear();
        foreach (var product in _productService.GetAllProductsFromList())
        {
            Products.Add(product);
        }
    }
}
