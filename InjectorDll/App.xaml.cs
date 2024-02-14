using InjectorDll.ViewModels;
using System.Configuration;
using System.Data;
using System.Windows;

namespace InjectorDll
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = new MainWindow()
            {
                DataContext = new ProcesssInjectorViewModel()
            };

            MainWindow.Show();
            base.OnStartup(e);
        }
    }

}
