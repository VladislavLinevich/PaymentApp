using PaymentsApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentsApp.Interfaces
{
    public interface ISelectedItem<T> where T : class
    {
        int SelectedIndex { get; set; }

        ObservableCollection<T> Items { get; set; }
    }
}
