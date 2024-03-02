using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessInjector.Services.ExternFunction.Enum;

public enum Protection
{
    PAGE_READWRITE = 4,
    PAGE_EXECUTE_READWRITE = 0x40,
}
