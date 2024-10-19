using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Shared.Interfaces;

namespace MainApp.ViewModels;

public partial class StartupViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IProductService _productService;

    public StartupViewModel(IServiceProvider serviceProvider, IProductService productService)
    {
        _serviceProvider = serviceProvider;
        _productService = productService;
    }

    [RelayCommand]
    public void Start()
    {
        var viewModel = _serviceProvider.GetRequiredService<MainWindowViewModel>();
        viewModel.CurrentViewModel = _serviceProvider.GetRequiredService<HomeViewModel>();
    }
}
