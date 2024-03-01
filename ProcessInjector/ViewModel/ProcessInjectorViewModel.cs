using ProcessInjector.Common;
using ProcessInjector.Services;
using ProcessInjector.ViewModel.Components;
using ProcessInjector.ViewModel.Single;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;

namespace ProcessInjector.ViewModel
{
    public class ProcessInjectorViewModel : ViewModelBase
    {
        private RelayCommandCan _buttonInject;
        private Mediator _mediator;
        public ProcesesViewModel ProcesesVM { get; }
        public DllsViewModel DllsVM { get; }

        public ProcessInjectorViewModel(Mediator mediator)
        {
            _mediator = mediator;
            _buttonInject = new RelayCommandCan(InjectAction, CanInjectButton);
            _mediator.UpdateButtonClicked += (sender, args) => _buttonInject.RaiseCanExecuteChanged();
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

        private bool CanInjectButton()
        {
            return ProcesesVM.SelectedProcess != null && DllsVM.SelectedDll != null;
        }
        private void InjectAction()
        {
            ProcessInjectorApp.InjectCreateRemoteThread(ProcesesVM.SelectedProcess.Pid, DllsVM.SelectedDll.FullPath);
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
