using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace InjectorDll.ViewModels
{
    public class ProcessListViewModel : ViewModelBase
    {
        private ObservableCollection<ProcessIdNameViewModel> _processIdNameViewModels;

        public ProcessListViewModel()
        {
            ProcessIdNameViewModels = GetProcessList(string.Empty);
        }

        private ICommand _cmdRefreshProcessList;
        private ICommand _cmdClearFinder;
        private ProcessIdNameViewModel _selectedProcess;
        private string _finder;


        public ProcessIdNameViewModel SelectedProcess
        {
            get
            {
                return _selectedProcess;
            }
            set
            {
                _selectedProcess = value;
                OnPropertyChanged(() => SelectedProcess);
            }
        }
        public ObservableCollection<ProcessIdNameViewModel> ProcessIdNameViewModels
        {
            get
            {
                return _processIdNameViewModels;
            }
            set
            {
                _processIdNameViewModels = value;
                OnPropertyChanged(() => ProcessIdNameViewModels);
            }
        }

        public ICommand CmdRefreshProcessList
        {
            get
            {
                if (_cmdRefreshProcessList == null)
                {
                    _cmdRefreshProcessList = new RelayCommand(x => RefreshProcessList());
                }
                return _cmdRefreshProcessList;
            }
        }

        public ICommand CmdClearFinder
        {
            get
            {
                if (_cmdClearFinder == null)
                {
                    _cmdClearFinder = new RelayCommand(x => ClearFinder());
                }
                return _cmdClearFinder;
            }
        }

        public string Finder
        {
            get
            {
                return _finder;
            }
            set
            {
                _finder = value;
                OnPropertyChanged(() => Finder);
                ProcessIdNameViewModels = GetProcessList(Finder);
            }
        }

        private void RefreshProcessList()
        {
            ProcessIdNameViewModels = GetProcessList(Finder);
        }

        private void ClearFinder()
        {
            Finder = string.Empty;
        }
        private ObservableCollection<ProcessIdNameViewModel> GetProcessList(string filtr)
        {
            try
            {
                var procesNames = Process.GetProcesses();
                var processList = procesNames.Select(x => (
                        new ProcessIdNameViewModel
                        {
                            Pid = x.Id,
                            Name = x.ProcessName,
                            WindowTitle = x.MainWindowTitle,
                        })).ToList();
                return new ObservableCollection<ProcessIdNameViewModel>(processList.Where(x =>
                   x.Name.ToLower().Contains(filtr.ToLower())
                || x.WindowTitle.ToLower().Contains(filtr.ToLower()))
                    .ToList());
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}

