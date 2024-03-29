using Avalonia.Controls;
using Avalonia.ReactiveUI;
using PaymentsApp.ViewModels;
using ReactiveUI;
using System.Reactive;
using System.Threading.Tasks;

namespace PaymentsApp.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    /*private async Task DoShowEditDialogAsync(InteractionContext<EditNameViewModel,
                                               Unit> interaction)
    {
        var dialog = new EditNameWindow();
        dialog.DataContext = interaction.Input;

        var result = await dialog.ShowDialog<Unit>(this);
        interaction.SetOutput(result);
    }*/
}
