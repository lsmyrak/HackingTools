using ProcessInjector.Common;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProcessInjector.ViewModel.Components
{
    public class ShellViewModel:ViewModelBase
    {
        private readonly Mediator _mediator;

        private ICommand _buttonOpenFile;
        private ICommand ButtonOk;
        private ICommand ButtonCancel;
        private string _textBoxData;
        private byte[] _data;

        public ShellViewModel(Mediator mediator) 
        { 
            _mediator = mediator;
        }

        public byte[] Data
        {
            get 
            { 
                return _data; 
            }
            set 
            { 
                _data = value;
                OnPropertyChanged(()=>Data);
            }
        }

        public string TextBoxData
        { 
            get 
            {
                return _textBoxData;
            }
            set
            { 
                _textBoxData = value;
                OnPropertyChanged(()=>TextBoxData);
            }
        }



    }
}
