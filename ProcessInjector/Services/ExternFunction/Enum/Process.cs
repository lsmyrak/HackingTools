using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessInjector.Services.ExternFunction.Enum;
public enum Process
{
    PROCESS_CREATE_THREAD = 0x0002,
    PROCESS_QUERY_INFORMATION = 0x0400,
    PROCESS_VM_OPERATION = 0x0008,
    PROCESS_VM_WRITE = 0x0020,
    PROCESS_VM_READ = 0x0010,
}
