using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls.ApplicationLifetimes;
using PaymentsApp.Data;
using System.Windows.Input;
using PaymentsApp.Interfaces;
using PaymentsApp.Models;
using PaymentsApp.Views;
using ReactiveUI;
using Avalonia;
using DynamicData;

namespace PaymentsApp.ViewModels
{
	public class DepartmentViewModel : ViewModelBase, ISelectedItem<Department>
    {
        private ObservableCollection<Department> _items;
        private readonly PaymentDbContext _dbContext;
        private int _selectedIndex;

        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set { this.RaiseAndSetIfChanged(ref _selectedIndex, value); }
        }

        public ObservableCollection<Department> Items
        {
            get { return _items; }
            set { this.RaiseAndSetIfChanged(ref _items, value); }
        }

        public DataNavigationBarViewModel<Department> NavigationBar { get; private set; }

        public Action NewCommand { get; private set; }
        public Action EditCommand { get; private set; }
        public Action DeleteCommand { get; private set; }

        public DepartmentViewModel(PaymentDbContext dbContext)
        {
            _dbContext = dbContext;
            var departments = _dbContext.Department.ToList();

            Items = [.. departments];
            Items = new ObservableCollection<Department>(Items.OrderBy(i => i.Name));
            if (Items.Count() == 0)
            {
                SelectedIndex = -1;
            }

            NewCommand = () =>
            {
                if (Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
                {
                    var dialog = new EditNameWindow();
                    dialog.DataContext = new EditDepartmentViewModel("Новое подразделение", new Department(), item =>
                    {
                        _dbContext.Department.Add(item);
                        _dbContext.SaveChanges();
                        Items.Add(item);
                        Items = new ObservableCollection<Department>(Items.OrderBy(i => i.Name));
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
                    dialog.DataContext = new EditDepartmentViewModel("Редактирование подразделения", (Department)Items[SelectedIndex].Clone(), item =>
                    {
                        var editItem = _dbContext.Department.Find(item.Id);
                        editItem.Name = item.Name;
                        _dbContext.SaveChanges();
                        var departments = _dbContext.Department.ToList();
                        Items = [.. departments];
                        Items = new ObservableCollection<Department>(Items.OrderBy(i => i.Name));
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
                    dialog.DataContext = new DeleteDialogViewModel($"Вы действительно хотите удалить подразделение {item.Name}?");
                    var isDelete = await dialog.ShowDialog<bool>(desktop.MainWindow);
                    if (isDelete)
                    {
                        _dbContext.Department.Remove(item);
                        _dbContext.SaveChanges();
                        int currentIndex = Items.IndexOf(item);
                        if (currentIndex == Items.Count() - 1)
                        {
                            currentIndex--;
                        }
                        var departments = _dbContext.Department.ToList();
                        Items = [.. departments];
                        Items = new ObservableCollection<Department>(Items.OrderBy(i => i.Name));
                        SelectedIndex = currentIndex;
                    }

                }
            };

            NavigationBar = new DataNavigationBarViewModel<Department>(this, NewCommand, EditCommand, DeleteCommand);
        }
    }
}
