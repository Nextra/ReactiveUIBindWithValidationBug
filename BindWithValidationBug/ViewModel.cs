using ReactiveUI.SourceGenerators;
using ReactiveUI.Validation.Extensions;
using ReactiveUI.Validation.Helpers;

using System.Collections.ObjectModel;

namespace BindWithValidationBug;

public record Entry(string Name, string Value);

public partial class ViewModel : ReactiveValidationObject
{
    [Reactive]
    string? _selectedValue;

    public ObservableCollection<Entry> Items { get; } = [
        new("Foo", "Bar"), 
        new("Baz", "Qux"),
    ];

    public ViewModel()
    {
        this.ValidationRule((vm) => vm.SelectedValue, (value) => !string.IsNullOrEmpty(value), (_) => "Please select an entry");
    }
}
