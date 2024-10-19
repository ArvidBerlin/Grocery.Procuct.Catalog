﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Shared.Enums;
using Shared.Interfaces;
using Shared.Models;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace MainApp.ViewModels;

public partial class CreateProductViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IProductService _productService;

    public ObservableCollection<Category> Categories { get; } = new ObservableCollection<Category>();

    [ObservableProperty]
    private string noName;

    [ObservableProperty]
    private string noPrice;

    public CreateProductViewModel(IServiceProvider serviceProvider, IProductService productService)
    {
        _serviceProvider = serviceProvider;
        _productService = productService;
        noName = NoName;
        noPrice = NoPrice;

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
        try
        {
            if (string.IsNullOrWhiteSpace(Product.Name))
            {
                NoName = "No name was given to product.";
            }
            else
            {
                NoName = "";
            } 

            if (Product.Price <= 0 || Product.Price == null!)
            {
                NoPrice = "Product price can't be 0, please set a price.";
            }
            else
            {
                NoPrice = "";
            }

            var result = _productService.CreateProduct(Product);

            if (result == Shared.Enums.StatusCodes.Success)
            {
                var viewModel = _serviceProvider.GetRequiredService<MainWindowViewModel>();
                viewModel.CurrentViewModel = _serviceProvider.GetRequiredService<HomeViewModel>();
            }
        }

        catch { }
    }

    [RelayCommand]
    public void Cancel()
    {
        var viewModel = _serviceProvider.GetRequiredService<MainWindowViewModel>();
        viewModel.CurrentViewModel = _serviceProvider.GetRequiredService<HomeViewModel>();
    }
}
