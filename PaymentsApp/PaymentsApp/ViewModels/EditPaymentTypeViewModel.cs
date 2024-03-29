using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Avalonia.Controls;
using PaymentsApp.Interfaces;
using PaymentsApp.Models;
using ReactiveUI;

namespace PaymentsApp.ViewModels
{
	public class EditPaymentTypeViewModel : EditDialogViewModelBase<PaymentType>, INotifyPropertyChanged
    {
		public string RecordTitle => "Вид платежа";

        //[Required(ErrorMessage = "а")]
        //[MaxLength(50)]
        public string RecordData
        {
            get { return Record.Name; }
            set {   Record.Name = value;
                    if (Record.Name?.Length > 0)
                    {
                        IsButtonEnabled = true;
                    } else
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
            set {
                _IsButtonEnabled = value;
                OnPropertyChanged("IsButtonEnabled");
            }
        }

        public EditPaymentTypeViewModel(string title, PaymentType record, DataFunc applyDataFunc, bool isAdd) : base(title, record, applyDataFunc, isAdd)
        {

            this.ApplyCommand = ReactiveCommand.Create<Window>(OnApplyClicked);
        }

        protected void OnApplyClicked(Window window)
        {
            ApplyDataFunc(Record);

            if (AddNextRecord)
            {
                Record = new PaymentType();
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

        /*private IObservable<bool> CanApply()
        {
            return this
                .WhenAnyValue(data => data.RecordData, rec => rec == "g");
        }*/
    }
}