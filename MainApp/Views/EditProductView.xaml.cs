using Shared.Enums;
using System.Windows.Controls;

namespace MainApp.Views;

public partial class EditProductView : UserControl
{
    public EditProductView()
    {
        InitializeComponent();

        CategoryComboBox.ItemsSource = Enum.GetValues(typeof(Category));
        CategoryComboBox.SelectedIndex = 0;
    }
}
