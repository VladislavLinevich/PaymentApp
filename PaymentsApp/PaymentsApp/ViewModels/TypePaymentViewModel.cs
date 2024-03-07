using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using DynamicData;
using PaymentsApp.Data;
using PaymentsApp.Interfaces;
using PaymentsApp.Models;
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

        public TypePaymentViewModel(PaymentDbContext dbContext)
        {
            _dbContext = dbContext;
            var paymentTypes = _dbContext.PaymentType.ToList();

            Items = [.. paymentTypes];

            NavigationBar = new DataNavigationBarViewModel<PaymentType>(this);
        }
    }
}
