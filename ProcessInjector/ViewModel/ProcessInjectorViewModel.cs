using ProcessInjector.Common;
using ProcessInjector.Services;
using ProcessInjector.View.Components;
using ProcessInjector.ViewModel.Components;
using ProcessInjector.ViewModel.Single;
using System.Collections.ObjectModel;
using System.IO;
using System.Printing.IndexedProperties;
using System.Text.Json;
using System.Windows;

namespace ProcessInjector.ViewModel
{
    public class ProcessInjectorViewModel : ViewModelBase
    {
        private RelayCommandCan _buttonInject;
        private RelayCommandCan _buttonInjectSimple;
        private Mediator _mediator;
     

        public ProcesesViewModel ProcesesVM { get; }
        public DllsViewModel DllsVM { get; }

        public ProcessInjectorViewModel(Mediator mediator)
        {
            _mediator = mediator;
            _buttonInject = new RelayCommandCan(InjectAction, CanInjectButton);
            _buttonInjectSimple = new RelayCommandCan(InjectSimple, CanInjectButton);
            _mediator.UpdateButtonClicked += (sender, args) => _buttonInject.RaiseCanExecuteChanged();
            _mediator.UpdateButtonClicked += (sender, args) => _buttonInjectSimple.RaiseCanExecuteChanged();
            ProcesesVM = new ProcesesViewModel(_mediator);
            DllsVM = new DllsViewModel(_mediator);
        }

        public RelayCommandCan ButtonInject
        {
            get
            {
                return _buttonInject;
            }
        }
        public RelayCommandCan ButtonInjectSimple
        {
            get 
            { 
                return _buttonInjectSimple;
            }
        }

        private bool CanInjectButton()
        {
            return ProcesesVM.SelectedProcess != null && DllsVM.SelectedDll != null;
        }
        private void InjectAction()
        {
            ProcessInjectorApp.InjectCreateRemoteThread(ProcesesVM.SelectedProcess.Pid, DllsVM.SelectedDll.FullPath);
        }
        private void InjectSimple()
        {             
           var messegResponse =  ProcessInjectorApp.InjectSimple(ProcesesVM.SelectedProcess.Pid,null);
           ProcessResponseView responseView = new ProcessResponseView();
           ProcessResponseViewModel viewModel = new ProcessResponseViewModel(messegResponse);
           responseView.DataContext = viewModel;
            Window window = new Window()
            {
                Title = "Response",
                Content = responseView,               
            };
            window.ShowDialog();

        }

        internal async void SaveDllData()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            await using FileStream createstream = File.Create($@"{path}\dllData.json");
            await JsonSerializer.SerializeAsync(createstream, DllsVM.Dlls);
        }
        internal async void ReadDllData()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            StreamReader streamReader = new StreamReader($@"{path}\dllData.json");
            var dlls = JsonSerializer.Deserialize<IList<DllViewModel>>(await streamReader.ReadToEndAsync());
            ObservableCollection<DllViewModel> dllViewModels = new ObservableCollection<DllViewModel>(dlls);
            DllsVM.Dlls = dllViewModels;
        }
    }
}
