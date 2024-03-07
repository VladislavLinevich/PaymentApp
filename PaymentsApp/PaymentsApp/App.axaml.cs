using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using PaymentsApp.Data;
using PaymentsApp.ViewModels;
using PaymentsApp.Views;
using System;

namespace PaymentsApp;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            //var serviceProvider = ConfigureServices();

            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainViewModel(new PaymentDbContext())
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

  /*  private IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        // Регистрация контекста базы данных
        services.AddDbContext<PaymentDbContext>();

        // Создание провайдера служб
        return services.BuildServiceProvider();
    }*/
}
