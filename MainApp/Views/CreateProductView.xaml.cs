using Shared.Enums;
using System.Windows.Controls;

namespace MainApp.Views;

public partial class CreateProductView : UserControl
{
    public CreateProductView()
    {
        InitializeComponent();
        
        // Fyller en Combobox med värdena från "Category"-enums
        CategoryComboBox.ItemsSource = Enum.GetValues(typeof(Category));
        CategoryComboBox.SelectedIndex = 0;
    }
}
