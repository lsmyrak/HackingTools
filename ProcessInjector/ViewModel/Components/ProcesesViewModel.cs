using ProcessInjector.Common;
using ProcessInjector.ViewModel.Single;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace ProcessInjector.ViewModel.Components
{
    public class ProcesesViewModel : ViewModelBase
    {
        private Mediator _mediator;

        private ProcessViewModel _selectedProcess;
        private string _filtr;
        private ICommand _buttonReflashProceses;
        private ICommand _buttonClearFiltr;
        private ObservableCollection<ProcessViewModel> _processes;



        public ObservableCollection<ProcessViewModel> Proceses 
        { get
            {
                return _processes;
            }
            set 
            { 
                _processes = value;
                OnPropertyChanged(() => Proceses);
            }
        }

        public ProcesesViewModel(Mediator mediator)
        {
            _mediator = mediator;
            Proceses = GetProceses(string.Empty);
        }

        public ObservableCollection<ProcessViewModel> GetProceses(string filtr)
        {
            try
            {
                var proceses = Process.GetProcesses();

                var procesesSelected =  new ObservableCollection<ProcessViewModel>(proceses.Select(x => new ProcessViewModel
                {
                    Pid = x.Id,
                    Name = x.ProcessName,
                    WindowName = x.MainWindowTitle,
                    //   Type = x.
                }).ToList());
               return new ObservableCollection<ProcessViewModel>(procesesSelected.Where(x =>
                   x.Name.ToLower().Contains(filtr.ToLower())
                || x.WindowName.ToLower().Contains(filtr.ToLower()))
                    .ToList());

            }
            catch (Exception ex)
            {
                return new ObservableCollection<ProcessViewModel>();
            }
        }

        public ProcessViewModel SelectedProcess
        {
            get
            {
                return _selectedProcess;
            }
            set
            {
                _selectedProcess = value;
                OnPropertyChanged(() => SelectedProcess);
                _mediator.RaiseButtonClicked();
            }
        }

        public ICommand ButtonClearFiltr
        {
            get
            {

                if (_buttonClearFiltr == null)
                {
                    _buttonClearFiltr = new RelayCommand(x => ClearFiltr());
                }
                return _buttonClearFiltr;
            }
        }
        public ICommand ButtonRefreshProceses
        {
            get
            {
                if (_buttonReflashProceses == null)
                {
                    _buttonReflashProceses = new RelayCommand(x => RefleshProcesses());
                }
                return _buttonReflashProceses;
            }
        }

        public string Filtr
        {
            get
            {
                return _filtr;
            }
            set
            {
                _filtr = value;
                OnPropertyChanged(() => Filtr);
                Proceses = GetProceses(Filtr);
            }
        }

        private void RefleshProcesses()
        {
            Proceses= GetProceses(Filtr);
        }
        private void ClearFiltr()
        {
            Filtr = string.Empty;
        }

    }
}
