using PaymentsApp.Interfaces;
using PaymentsApp.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PaymentsApp.ViewModels
{
    public class DataNavigationBarViewModel<T> : ViewModelBase where T : class
    {

        /* public Func<bool> RefreshCanExecute { get; set; } = () => true;*/

        public DataNavigationBarViewModel(ISelectedItem<T> selected)
        {
            FirstCommand = ReactiveCommand.Create(() => { selected.SelectedIndex = 0; }, CanExecuteFirst(selected));
            PreviousCommand = ReactiveCommand.Create(() => { selected.SelectedIndex--; }, CanExecutePrevious(selected));
            NextCommand = ReactiveCommand.Create(() => { selected.SelectedIndex++; }, CanExecuteNext(selected));
            LastCommand = ReactiveCommand.Create(() => { selected.SelectedIndex = selected.Items.Count - 1; }, CanExecuteLast(selected));

            /*FirstCommand = new RelayCommand(() =>
            {
                _view.MoveCurrentToFirst();
            }, _ => _view != null && !_view.IsEmpty && _view.CurrentPosition > 0);
            PreviousCommand = new RelayCommand(() => _view.MoveCurrentToPrevious(), _ => _view != null && !_view.IsEmpty && _view.CurrentPosition > 0);
            NextCommand = new RelayCommand(() => _view.MoveCurrentToNext(), _ => _view != null && !_view.IsEmpty && _view.CurrentPosition + 1 < _view.Count);
            LastCommand = new RelayCommand(() => _view.MoveCurrentToLast(), _ => _view != null && !_view.IsEmpty && _view.CurrentPosition + 1 < _view.Count);

            NewCommand = newCommand == null ? new RelayCommand<T>(o => { }, _ => false) : new RelayCommand<T>(newCommand, _ => _view != null);
            DeleteCommand = deleteCommand == null ? new RelayCommand<T>(o => { }, _ => false) : new RelayCommand<T>(deleteCommand, _ => _view != null && !_view.IsEmpty && !_view.IsCurrentAfterLast && !_view.IsCurrentBeforeFirst);
            EditCommand = editCommand == null ? new RelayCommand<T>(o => { }, _ => false) : new RelayCommand<T>(editCommand, _ => _view != null && !_view.IsEmpty && !_view.IsCurrentAfterLast && !_view.IsCurrentBeforeFirst);
            RefreshCommand = refreshCommand == null ? new RelayCommand(() => { }, _ => false) : new RelayCommand(refreshCommand, _ => _view != null && RefreshCanExecute());*/
        }

        public ReactiveCommand<Unit, Unit> FirstCommand { get; private set; }
        public ReactiveCommand<Unit, Unit> PreviousCommand { get; private set; }
        public ReactiveCommand<Unit, Unit> NextCommand { get; private set; }
        public ReactiveCommand<Unit, Unit> LastCommand { get; private set; }
        private IObservable<bool> CanExecuteFirst(ISelectedItem<T> selected)
        {
            return selected
                .WhenAnyValue(x => x.SelectedIndex, index => index > 0);
        }

        private IObservable<bool> CanExecutePrevious(ISelectedItem<T> selected)
        {
            return selected
                .WhenAnyValue(x => x.SelectedIndex, index => index > 0);
        }

        private IObservable<bool> CanExecuteNext(ISelectedItem<T> selected)
        {
            return selected
                .WhenAnyValue(x => x.SelectedIndex, index => index < selected.Items.Count - 1);
        }

        private IObservable<bool> CanExecuteLast(ISelectedItem<T> selected)
        {
            return selected
                .WhenAnyValue(x => x.SelectedIndex, index => index < selected.Items.Count - 1);
        }

        /*    public ICommand NewCommand { get; private set; }
            public ICommand DeleteCommand { get; private set; }
            public ICommand EditCommand { get; private set; }
            public ICommand RefreshCommand { get; private set; }*/

    }
}
