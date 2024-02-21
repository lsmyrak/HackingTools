using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Printing;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace InjectorDll.Modules;

internal static class Injector
{
    internal static void Inject(int IdProcess, string pathDll)
    {
        Process targetProcess = Process.GetProcessById(IdProcess);
        IntPtr procHandle = ExternFunction.OpenProcess(
            ExternFunction.PROCESS_CREATE_THREAD |
            ExternFunction.PROCESS_QUERY_INFORMATION |
            ExternFunction.PROCESS_VM_OPERATION |
            ExternFunction.PROCESS_VM_WRITE |
            ExternFunction.PROCESS_VM_READ,
            false, targetProcess.Id);
        IntPtr loadLibraryAddr = ExternFunction.GetProcAddress(ExternFunction.GetModuleHandle("kernel32.dll"), "LoadLibraryA");
        IntPtr allocMemAddress = ExternFunction.VirtualAllocEx(procHandle, IntPtr.Zero, (uint)((pathDll.Length + 1) * Marshal.SizeOf(typeof(char))), ExternFunction.MEM_COMMIT | ExternFunction.MEM_RESERVE, ExternFunction.PAGE_READWRITE);
        UIntPtr bytesWritten;
        ExternFunction.WriteProcessMemory(procHandle, allocMemAddress, Encoding.Default.GetBytes(pathDll),
            (uint)((pathDll.Length + 1) * Marshal.SizeOf(typeof(char))), out bytesWritten);
        ExternFunction.CreateRemoteThread(procHandle, IntPtr.Zero, 0, loadLibraryAddr, allocMemAddress, 0, IntPtr.Zero);
    }
}
