using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using DynamicData;
using PaymentsApp.Data;
using PaymentsApp.Interfaces;
using PaymentsApp.Models;
using PaymentsApp.Views;
using ReactiveUI;

namespace PaymentsApp.ViewModels
{
	public class TypePaymentViewModel: ViewModelBase, ISelectedItem<PaymentType>
    {
        private ObservableCollection<PaymentType> _items;
        private readonly PaymentDbContext _dbContext;
        private int _selectedIndex;

        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set { this.RaiseAndSetIfChanged(ref _selectedIndex, value); }
        }

        public ObservableCollection<PaymentType> Items
        {
            get { return _items; }
            set { this.RaiseAndSetIfChanged(ref _items, value); }
        }

        public DataNavigationBarViewModel<PaymentType> NavigationBar { get; private set; }

        public Action NewCommand { get; private set; }
        public Action EditCommand { get; private set; }
        public Action DeleteCommand { get; private set; }

        public TypePaymentViewModel(PaymentDbContext dbContext)
        {
            _dbContext = dbContext;
            var paymentTypes = _dbContext.PaymentType.ToList();

            Items = [.. paymentTypes];
            Items = new ObservableCollection<PaymentType>(Items.OrderBy(i => i.Name));
            if (Items.Count() == 0)
            {
                SelectedIndex = -1;
            }

            NewCommand = () =>
            {
                if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
                {
                    var dialog = new EditNameWindow();
                    dialog.DataContext = new EditPaymentTypeViewModel("Новый вид платежа", new PaymentType (), item =>
                    {
                        _dbContext.PaymentType.Add(item);
                        _dbContext.SaveChanges();
                        Items.Add(item);
                        Items = new ObservableCollection<PaymentType>(Items.OrderBy(i => i.Name));
                        SelectedIndex = Items.IndexOf(item);
                    }, true);
                    dialog.ShowDialog(desktop.MainWindow);
                }
            };

            EditCommand = () =>
            {
                if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
                {
                    var dialog = new EditNameWindow();
                    dialog.DataContext = new EditPaymentTypeViewModel("Редактирование вида платежа", (PaymentType)Items[SelectedIndex].Clone(), item =>
                    {
                        var editItem = _dbContext.PaymentType.Find(item.Id);
                        editItem.Name = item.Name;
                        _dbContext.SaveChanges();
                        var paymentTypes = _dbContext.PaymentType.ToList();
                        Items = [.. paymentTypes];
                        Items = new ObservableCollection<PaymentType>(Items.OrderBy(i => i.Name));
                        SelectedIndex = Items.IndexOf(editItem);
                    }, false);
                    dialog.ShowDialog(desktop.MainWindow);
                }
            };

            DeleteCommand = async () =>
            {
                if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
                {
                    var dialog = new DeleteWindow();
                    var item = Items[SelectedIndex];
                    dialog.DataContext = new DeleteDialogViewModel($"Вы действительно хотите удалить вид платежа {item.Name}?");
                    var isDelete = await dialog.ShowDialog<bool>(desktop.MainWindow);
                    if (isDelete)
                    {
                        _dbContext.PaymentType.Remove(item);
                        _dbContext.SaveChanges();
                        int currentIndex = Items.IndexOf(item);
                        if (currentIndex == Items.Count() - 1)
                        {
                            currentIndex--;
                        }
                        var paymentTypes = _dbContext.PaymentType.ToList();
                        Items = [.. paymentTypes];
                        Items = new ObservableCollection<PaymentType>(Items.OrderBy(i => i.Name));
                        SelectedIndex = currentIndex;
                    }

                }
            };

            NavigationBar = new DataNavigationBarViewModel<PaymentType>(this, NewCommand, EditCommand, DeleteCommand);
        }
    }
}
