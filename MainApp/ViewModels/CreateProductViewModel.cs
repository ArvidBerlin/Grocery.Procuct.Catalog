using CommunityToolkit.Mvvm.ComponentModel;
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

    // Property för att ge felmeddelande ifall inget namn skrivs
    [ObservableProperty]
    private string noName;

    // Property för att ge felmeddelande ifall inget pris skrivs
    [ObservableProperty]
    private string noPrice;

    // property för att ge felmeddelande ifall produkt med samma namn redan existerar i listan
    [ObservableProperty]
    private string sameName;

    // Property som kombinerar felmeddelanden av NoName och SameName, för att skriva ut beroende på vilket fel som sker
    // Samt metoder som uppdaterar propertyn när någon av NoName eller SameName ändras
    // Hjälp från ChatGPT
    public string CombinedNameErrors => $"{NoName}\n{SameName}".Trim();

    partial void OnNoNameChanged(string value)
    {
        OnPropertyChanged(nameof(CombinedNameErrors));
    }

    partial void OnSameNameChanged(string value)
    {
        OnPropertyChanged(nameof(CombinedNameErrors));
    }

    public CreateProductViewModel(IServiceProvider serviceProvider, IProductService productService)
    {
        _serviceProvider = serviceProvider;
        _productService = productService;
        noName = "";
        noPrice = "";
        sameName = "";

        foreach (var category in Enum.GetValues(typeof(Category)))
        {
            Categories.Add((Category)category);
        }
    }

    [ObservableProperty]
    private Product product = new();

    // Kommando med metod för att spara värden av en produkt 
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

            if (_productService.GetAllProductsFromList().Any(x => x.Name == Product.Name))
            {
                SameName = "Product with same name already exists in the inventory.";
            }
            else
            {
                SameName = "";
            }

            // Skickar värdena till metod för att skapa och spara en produkt
            var result = _productService.CreateProduct(Product);

            if (result == Shared.Enums.StatusCodes.Success)
            {
                var viewModel = _serviceProvider.GetRequiredService<MainWindowViewModel>();
                viewModel.CurrentViewModel = _serviceProvider.GetRequiredService<HomeViewModel>();
            }
        }

        catch { }
    }

    // Kommando med metod för att gå tillbaka till "Home"-sidan
    [RelayCommand]
    public void Cancel()
    {
        var viewModel = _serviceProvider.GetRequiredService<MainWindowViewModel>();
        viewModel.CurrentViewModel = _serviceProvider.GetRequiredService<HomeViewModel>();
    }
}
