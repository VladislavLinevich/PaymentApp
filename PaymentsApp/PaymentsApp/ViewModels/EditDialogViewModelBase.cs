using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using PaymentsApp.Models;
using PaymentsApp.Views;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PaymentsApp.ViewModels
{
    public class EditDialogViewModelBase<T>: DialogViewModelBase where T : new()
    {
        public ICommand ApplyCommand { get; set; } = null;

        public ICommand CancelCommand { get; set; } = null;

        private T _record;

        public T Record
        {
            get { return _record; }
            set { this.RaiseAndSetIfChanged(ref _record, value); }
        }

        private bool _isCheckBoxVisible;

        public bool IsCheckBoxVisible
        {
            get { return _isCheckBoxVisible; }
            set { this.RaiseAndSetIfChanged(ref _isCheckBoxVisible, value); }
        }

        /*private bool _isModelValid;
        public bool IsModelValid
        {
            get { return _isModelValid; }
            set { this.RaiseAndSetIfChanged(ref _isModelValid, value); }
        }*/

        public delegate void DataFunc(T item);
        public DataFunc ApplyDataFunc { get; private set; }

        public bool AddNextRecord { get; set; }

        protected EditDialogViewModelBase(string title, T record, DataFunc applyDataFunc, bool isAdd)
        {
            Title = title;
            Record = record;
            /*ApplyDataFunc = applyDataFunc;
            _isModelValid = applyDataFunc != null;*/
            ApplyDataFunc = applyDataFunc;
            //this.ApplyCommand = ReactiveCommand.Create<Window>(OnApplyClicked);
            this.CancelCommand = ReactiveCommand.Create<Window>(OnCancelClicked);
            IsCheckBoxVisible = isAdd;
        }
        protected virtual void OnCancelClicked(Window window)
        {
            window.Close();
        }
        /*protected virtual void OnApplyClicked(Window window)
        {
            ApplyDataFunc(Record);

            if (AddNextRecord)
            {
               Record = new T();
            }
            else 
            {
                window.Close();
            }
        }*/
        
        /*private IObservable<bool> CanApply(string data)
        {
            return data
                .WhenAnyValue(data => data.Length > 0);
        }*/
    }

        /* protected virtual void OnCancelClicked(Window parameter)
        {
            CloseDialogWithResult(parameter, DialogResult.Cancel);
        }

        public string this[string columnName] => AttributeValidator.Validate(this, columnName);

        public string Error
        {
            get
            {
                throw new NotImplementedException();
            }
        }*/
}
