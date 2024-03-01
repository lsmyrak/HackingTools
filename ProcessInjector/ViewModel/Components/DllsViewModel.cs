using Microsoft.Win32;
using ProcessInjector.Common;
using ProcessInjector.ViewModel.Single;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ProcessInjector.ViewModel.Components
{
    public class DllsViewModel : ViewModelBase
    {
        private Mediator _mediator;

        private ICommand _buttonAddDllToList;
        private ICommand _buttonRemoveDllFromList; // enable only selected
        private ICommand _buttonClearList;

        private ObservableCollection<DllViewModel> _Dllvm;
        private DllViewModel _selectedDll;

        public DllsViewModel(Mediator mediator)
        {
            _mediator = mediator;
            _Dllvm = new ObservableCollection<DllViewModel>();
        }

        public ObservableCollection<DllViewModel> Dlls
        {
            get
            {
                return _Dllvm;
            }
            set
            {
                _Dllvm = value;
                OnPropertyChanged(() => Dlls);
            }
        }
        public DllViewModel SelectedDll
        {
            get
            {
                return _selectedDll;
            }
            set
            {
                _selectedDll = value;
                OnPropertyChanged(() => SelectedDll);
                _mediator.RaiseButtonClicked();
            }
        }

        public ICommand ButtonAddDllToList
        {
            get
            {
                if (_buttonAddDllToList == null)
                {
                    _buttonAddDllToList = new RelayCommand(x => AddDllToList());
                }
                return _buttonAddDllToList;
            }
        }

        public ICommand ButtonRemoveDllFromList
        {
            get
            {
                if (_buttonRemoveDllFromList == null)
                {
                    _buttonRemoveDllFromList = new RelayCommand(x => RemoveDllFromList());
                }
                return _buttonRemoveDllFromList;
            }
        }

        public ICommand ButtonClearList
        {
            get
            {
                if (_buttonClearList == null)
                {
                    _buttonClearList = new RelayCommand(x => ClearDlls());
                }
                return _buttonClearList;
            }
        }




        //private method
        private void AddDllToList()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "dll files (*.dll)|*.dll";
            if (openFileDialog.ShowDialog() == true)
            {
                Dlls.Add(
                    new DllViewModel
                    {
                        FullPath = openFileDialog.FileName,
                        Id = _Dllvm.Count() + 1,
                        IsChceked = false,
                        Name = openFileDialog.SafeFileName,
                    }
                    );
            }
        }

        private void RemoveDllFromList()
        {
            _Dllvm.Remove(_selectedDll);
            OnPropertyChanged(() => SelectedDll);
        }

        private void ClearDlls()
        {
            _Dllvm.Clear();
            OnPropertyChanged(() => SelectedDll);
        }
    }


}
