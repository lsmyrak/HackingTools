using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InjectorDll.ViewModels
{
   public class DllNameViewModel : ViewModelBase
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string DllPath { get; set; }

        private ICommand _cmdSelectDll;
    }
}
