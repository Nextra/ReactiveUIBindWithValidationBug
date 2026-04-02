using ReactiveUI;

namespace BindWithValidationBug;

public class BaseMainWindow : ReactiveWindow<ViewModel>;

public partial class MainWindow : BaseMainWindow
{
    public MainWindow()
    {
        ViewModel = new();

        InitializeComponent();

        this.WhenActivated(WhenActivated);
    }

    void WhenActivated(Action<IDisposable> d)
    {
        d(this.OneWayBind(ViewModel, (vm) => vm.Items, (v) => v.ComboBox.ItemsSource));
        d(this.OneWayBind(ViewModel, (vm) => vm.SelectedValue, (v) => v.TextBlock.Text));

        // Works
        d(this.BindWithValidation(ViewModel, (vm) => vm.SelectedValue, (v) => v.TextBox.Text));

        // Works
        d(this.Bind(ViewModel, (vm) => vm.SelectedValue, (v) => v.ComboBox.SelectedValue));

        // Breaks, "Dependency Property not found on object"
        d(this.BindWithValidation(ViewModel, (vm) => vm.SelectedValue, (v) => v.ComboBox.SelectedValue));
    }
}