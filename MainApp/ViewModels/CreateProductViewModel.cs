﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Shared.Interfaces;
using Shared.Models;

namespace MainApp.ViewModels;

public partial class CreateProductViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IProductService _productService;


    public CreateProductViewModel(IServiceProvider serviceProvider, IProductService productService)
    {
        _serviceProvider = serviceProvider;
        _productService = productService;
    }

    [ObservableProperty]
    private Product product = new();

    [RelayCommand]
    public void Save()
    {
        var result = _productService.CreateProduct(Product);
        if (result == Shared.Enums.StatusCodes.Success || result == Shared.Enums.StatusCodes.Exists)
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
