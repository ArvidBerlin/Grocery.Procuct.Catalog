using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Shared.Enums;
using Shared.Interfaces;
using Shared.Models;
using System.Collections.ObjectModel;

namespace MainApp.ViewModels;

public partial class CreateProductViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IProductService _productService;

    public ObservableCollection<Category> Categories { get; } = new ObservableCollection<Category>();

    [ObservableProperty]
    private string _savingMessage;

    public CreateProductViewModel(IServiceProvider serviceProvider, IProductService productService)
    {
        _serviceProvider = serviceProvider;
        _productService = productService;
        _savingMessage = "";

        foreach (var category in Enum.GetValues(typeof(Category)))
        {
            Categories.Add((Category)category);
        }
    }

    [ObservableProperty]
    private Product product = new();

    [RelayCommand]
    public void Save()
    {

        if (Product.Price == null || Product.Price <= 0)
        {
            SavingMessage = "saknar pris";
            return;
        }

        var result = _productService.CreateProduct(Product);
        if ( result == Shared.Enums.StatusCodes.Exists)
        {
           SavingMessage  = "Finns redan";
        }

        if (result == Shared.Enums.StatusCodes.NoPriceSet)
        {
            SavingMessage = "Saknar prisssss";
        }

        if (result == Shared.Enums.StatusCodes.Success)
        {
            var viewModel = _serviceProvider.GetRequiredService<MainWindowViewModel>();
            viewModel.CurrentViewModel = _serviceProvider.GetRequiredService<HomeViewModel>();
        }

    }

    [RelayCommand]
    public void Cancel()
    {
        var viewModel = _serviceProvider.GetRequiredService<MainWindowViewModel>();
        viewModel.CurrentViewModel = _serviceProvider.GetRequiredService<HomeViewModel>();
    }
}
