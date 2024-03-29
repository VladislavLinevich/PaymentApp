using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentsApp.ViewModels
{
    public abstract class DialogViewModelBase: ViewModelBase
    {
        public string Title { get; set; }
    }
}
