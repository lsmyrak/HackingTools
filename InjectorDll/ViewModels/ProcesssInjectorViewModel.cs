using InjectorDll.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InjectorDll.ViewModels
{
    public class ProcesssInjectorViewModel:ViewModelBase
    {
        public ProcessListViewModel ProcessListViewModel { get; }
        public DllListViewModel DllListViewModel { get; }
       
        public ProcesssInjectorViewModel()
        {
            ProcessListViewModel = new ProcessListViewModel();
            DllListViewModel = new DllListViewModel();
        }
    }
}
