using ProcessInjector.Common;
using ProcessInjector.ViewModel;
using System.Windows;

namespace ProcessInjector
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        Mediator mediator = new Mediator();
        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow mainWindow = new MainWindow()
            {
                DataContext = new ProcessInjectorViewModel(mediator)
            };

            mainWindow.Show();
            base.OnStartup(e);
        }
    }

}
