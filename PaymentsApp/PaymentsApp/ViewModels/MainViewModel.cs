using System.Collections.ObjectModel;
using System;
using ReactiveUI;
using PaymentsApp.Data;
using System.Reactive;
using System.Windows.Input;
using System.Reactive.Linq;

namespace PaymentsApp.ViewModels;

public class MainViewModel : ViewModelBase
{
	public ObservableCollection<TabPageModel> Pages { get; private set; }

    /*	private int _selectedTab;
        public int SelectedTab
        {
            get { return _selectedTab; }
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedTab, value);
            }
        }*/

/*    public Interaction<EditNameViewModel, Unit> ShowDialog { get; }
    public ICommand NewCommand { get; private set; }*/
    public MainViewModel(PaymentDbContext dbContext)
	{

       /* ShowDialog = new Interaction<EditNameViewModel, Unit>();

        NewCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var store = new EditNameViewModel();

            var result = await ShowDialog.Handle(store);
        });*/

        Pages = new ObservableCollection<TabPageModel>
			{
				//new MatrixViewModel(),
				new TabPageModel("Выплаты", new PaymentViewModel()),
				new TabPageModel("Отчет за период",  new ReportViewModel()),
				new TabPageModel("Сотрудники", new EmployeeViewModel()),
				new TabPageModel("Подразделения", new DepartmentViewModel(dbContext)),
				new TabPageModel("Виды платежей", new TypePaymentViewModel(dbContext))
				//new PeriodViewModel()
			};

    }

}

public sealed class TabPageModel
{
	public string Title { get; private set; }
	public ViewModelBase PageModel { get; private set; }

	public TabPageModel(string title, ViewModelBase pageModel)
	{
		Title = title;
		PageModel = pageModel;
	}
}
