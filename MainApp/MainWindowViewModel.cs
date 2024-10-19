using CommunityToolkit.Mvvm.ComponentModel;
using MainApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace MainApp;

public partial class MainWindowViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;

    public MainWindowViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        currentViewModel = _serviceProvider.GetRequiredService<StartupViewModel>();
    }

    [ObservableProperty]
    private ObservableObject currentViewModel;
}
