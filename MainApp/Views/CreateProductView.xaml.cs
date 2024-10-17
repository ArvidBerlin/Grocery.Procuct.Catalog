using Shared.Enums;
using System.Windows.Controls;

namespace MainApp.Views;

public partial class CreateProductView : UserControl
{
    public CreateProductView()
    {
        InitializeComponent();

        CategoryComboBox.ItemsSource = Enum.GetValues(typeof(Category));
        CategoryComboBox.SelectedIndex = 0;
    }
}
