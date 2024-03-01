using ProcessInjector.ViewModel;
using System.Windows;

namespace ProcessInjector
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Closing += MainWindow_Closing;
            Loaded += MainWindow_Loaded;

        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is ProcessInjectorViewModel vm)
            {
                vm.ReadDllData();
            }
        }

        private void MainWindow_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            if (DataContext is ProcessInjectorViewModel vm)
            {
                vm.SaveDllData();
            }
        }
    }
}