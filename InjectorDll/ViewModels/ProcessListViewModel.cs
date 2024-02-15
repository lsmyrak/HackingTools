using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InjectorDll.ViewModels
{
    public class ProcessListViewModel : ViewModelBase
    {
        private ObservableCollection<ProcessIdNameViewModel> _processIdNameViewModels;

        private ICommand _cmdRefreshProcessList;

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

        private void RefreshProcessList()
        { 

        }
    }
}
