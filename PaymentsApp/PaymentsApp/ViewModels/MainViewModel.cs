using System.Collections.ObjectModel;
using System;
using ReactiveUI;
using PaymentsApp.Data;

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

	public MainViewModel(PaymentDbContext dbContext)
	{
		Pages = new ObservableCollection<TabPageModel>
			{
				//new MatrixViewModel(),
				new TabPageModel("Выплаты", new PaymentViewModel()),
				new TabPageModel("Отчет за период",  new ReportViewModel()),
				new TabPageModel("Сотрудники", new EmployeeViewModel()),
				new TabPageModel("Подразделения", new DepartmentViewModel()),
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
