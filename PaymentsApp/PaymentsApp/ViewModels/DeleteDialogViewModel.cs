using Avalonia.Controls;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PaymentsApp.ViewModels
{
    public class DeleteDialogViewModel: DialogViewModelBase
    {
        public ICommand YesCommand { get; set; } = null;

        public ICommand NoCommand { get; set; } = null;

        public string Message { get; set; }

        public DeleteDialogViewModel(string message)
        {
            Title = "Подтверждение";
            Message = message;

            YesCommand = ReactiveCommand.Create<Window>(OnYesClicked);
            NoCommand = ReactiveCommand.Create<Window>(OnNoClicked);
        }

        private void OnNoClicked(Window window)
        {
            window.Close(false);
        }

        private void OnYesClicked(Window window)
        {
            window.Close(true);
        }
    }
}
