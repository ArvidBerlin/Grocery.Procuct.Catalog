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

    [RelayCommand]
    public void Create()
    {
        var viewModel = _serviceProvider.GetRequiredService<MainWindowViewModel>();
        viewModel.CurrentViewModel = _serviceProvider.GetRequiredService<CreateProductViewModel>();
    }

    [RelayCommand]
    public void Edit(Product product)
    {
        var editProductViewModel = _serviceProvider.GetRequiredService<EditProductViewModel>();
        editProductViewModel.Product = product;

        var viewModel = _serviceProvider.GetRequiredService<MainWindowViewModel>();
        viewModel.CurrentViewModel = editProductViewModel;
    }

    [RelayCommand]
    public void Delete(string id)
    {
        _productService.Delete(id);
        UpdateProducts();
    }

    public void UpdateProducts()
    {
        Products.Clear();
        foreach (var product in _productService.GetAllProductsFromList())
        {
            Products.Add(product);
        }
    }
}
