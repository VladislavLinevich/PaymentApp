using Avalonia.Controls;
using PaymentsApp.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentsApp.ViewModels
{
    public class EditDepartmentViewModel : EditDialogViewModelBase<Department>, INotifyPropertyChanged
    {
        public string RecordTitle => "Подразделение";

        //[Required(ErrorMessage = "Поле является обязательным и не может быть пустым")]
        public string RecordData
        {
            get { return Record.Name; }
            set {
                Record.Name = value;
                if (Record.Name?.Length > 0)
                {
                    IsButtonEnabled = true;
                }
                else
                {
                    IsButtonEnabled = false;
                }
                OnPropertyChanged(nameof(RecordData));
            }
        }

        private bool _IsButtonEnabled { get; set; }
        public bool IsButtonEnabled
        {
            get { return _IsButtonEnabled; }
            set
            {
                _IsButtonEnabled = value;
                OnPropertyChanged("IsButtonEnabled");
            }
        }

        public EditDepartmentViewModel(string title, Department record, DataFunc applyDataFunc, bool isAdd) : base(title, record, applyDataFunc, isAdd)
        {
            this.ApplyCommand = ReactiveCommand.Create<Window>(OnApplyClicked);
        }

        protected void OnApplyClicked(Window window)
        {
            ApplyDataFunc(Record);

            if (AddNextRecord)
            {
                Record = new Department();
                RecordData = Record.Name;
            }
            else
            {
                window.Close();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
