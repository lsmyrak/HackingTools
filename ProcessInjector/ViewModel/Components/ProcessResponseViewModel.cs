using ProcessInjector.Services.ExternFunction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessInjector.ViewModel.Components
{
     public  class ProcessResponseViewModel:ViewModelBase
    {
        private ProcessResponse _processResponse;
        public ProcessResponseViewModel(ProcessResponse processResponse)
        {
            _processResponse = processResponse;
        }

        public string Id
        {
            get
            { 
                return _processResponse.Id.ToString();
            }
        }
        public string Message
        { 
            get 
            {
                return _processResponse.Message;
            }
        }
    }
}
