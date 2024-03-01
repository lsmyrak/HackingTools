using ProcessInjector.Services.ExternFunction;
using System.Runtime.InteropServices;
using System.Text;

namespace ProcessInjector.Services
{
    internal static class ProcessInjectorApp
    {
        public static void InjectCreateRemoteThread(int idProcess, string pathDll)
        {
            IntPtr procHandle = Extern.OpenProcess(
                Extern.PROCESS_CREATE_THREAD |
                Extern.PROCESS_QUERY_INFORMATION |
                Extern.PROCESS_VM_OPERATION |
                Extern.PROCESS_VM_WRITE |
                Extern.PROCESS_VM_READ,
                false, idProcess);
            IntPtr loadLibraryAddr = Extern.GetProcAddress(Extern.GetModuleHandle("kernel32.dll"), "LoadLibraryA");
            IntPtr allocMemAddress = Extern.VirtualAllocEx(procHandle, IntPtr.Zero, (uint)((pathDll.Length + 1) *
                Marshal.SizeOf(typeof(char))), Extern.MEM_COMMIT | Extern.MEM_RESERVE, Extern.PAGE_READWRITE);
            UIntPtr bytesWritten;
            Extern.WriteProcessMemory(procHandle, allocMemAddress, Encoding.Default.GetBytes(pathDll),
                (uint)((pathDll.Length + 1) * Marshal.SizeOf(typeof(char))), out bytesWritten);
            Extern.CreateRemoteThread(procHandle, IntPtr.Zero, 0, loadLibraryAddr, allocMemAddress, 0, IntPtr.Zero);
        }
    }
}

