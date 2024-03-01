using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace InjectorDll.ViewModels
{
    public class DllListViewModel : ViewModelBase
    {
        // private list dll..
        private ObservableCollection<DllNameViewModel> _dllNameViewModels;

        //public dll list
        public ObservableCollection<DllNameViewModel> DllNameViewModels
        {
            get
            {
                return _dllNameViewModels;
            }
            set
            {
                _dllNameViewModels = value;
                OnPropertyChanged(() => DllNameViewModels);
            }

        }
        public DllNameViewModel SelectedDll
        {
            get
            {
                return _selectedDll;
            }
            set
            {
                _selectedDll = value;
                OnPropertyChanged(() => SelectedDll);
            }
        }

        //  private .... buttons 
        private ICommand _cmdAddDll;
        private ICommand _cmdRemoveDll;
        private ICommand _cmdClearList;


        private DllNameViewModel _selectedDll;

        public DllListViewModel()
        {
            _dllNameViewModels = new ObservableCollection<DllNameViewModel>();
        }

        public ICommand CmdAddDll
        {
            get
            {
                if (_cmdAddDll == null)
                {
                    _cmdAddDll = new RelayCommand(x => AddDllDialog());
                }
                return _cmdAddDll;
            }
        }

        public ICommand CmdRemoveDll
        {
            get
            {
                if (_cmdRemoveDll == null)
                {
                    _cmdRemoveDll = new RelayCommand(x => RemoveDll());
                }
                return _cmdRemoveDll;
            }
        }

        public ICommand CmdClearList
        {
            get
            {
                if (_cmdClearList == null)
                {
                    _cmdClearList = new RelayCommand(x => _dllNameViewModels.Clear());
                }
                return _cmdClearList;
            }
        }



        //private method ...

        private void AddDllDialog()
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "dll files (*.dll)|*.dll|All files (*.*)|*.*";
            ofd.FilterIndex = 2;
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == true)
            {
                _dllNameViewModels.Add(new DllNameViewModel
                {
                    Id = _dllNameViewModels.Count() + 1,
                    Name = ofd.SafeFileName,
                    DllPath = ofd.FileName,
                }); ;

            }

        }
        private void RemoveDll()
        {
            if (_selectedDll != null)
            {
                _dllNameViewModels.Remove(_selectedDll);
                OnPropertyChanged(() => DllNameViewModels);
            }

        }

    }
}
