using System.Windows.Input;

namespace InjectorDll.ViewModels
{
    public class ProcesssInjectorViewModel : ViewModelBase
    {
        public ICommand CmdInjectProcess { get; }

        public ProcessListViewModel ProcessListViewModel { get; }
        public DllListViewModel DllListViewModel { get; }

        public ProcesssInjectorViewModel()
        {
            ProcessListViewModel = new ProcessListViewModel();
            DllListViewModel = new DllListViewModel();
            CmdInjectProcess = new RelayCommandCan(InjectProcess, CanInjectButton);
        }


        private bool CanInjectButton()
        {
            return ProcessListViewModel.SelectedProcess.GetType() == typeof(ProcessListViewModel);
        }
        private void InjectProcess()
        {
            //           Injector.Inject(processModel.SelectedProcess.Pid, dllModel.SelectedDll.DllPath);
        }

    }
}
