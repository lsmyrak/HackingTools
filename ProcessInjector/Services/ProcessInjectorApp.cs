using ProcessInjector.Services.ExternFunction;
using ProcessInjector.Services.ExternFunction.Enum;
using System.Runtime.InteropServices;
using System.Text;

namespace ProcessInjector.Services
{
    internal static class ProcessInjectorApp
    {
        public static void InjectCreateRemoteThread(int idProcess, string pathDll)
        {
            var desiredAccess = Process.PROCESS_CREATE_THREAD | Process.PROCESS_QUERY_INFORMATION | Process.PROCESS_VM_OPERATION | Process.PROCESS_VM_READ | Process.PROCESS_VM_WRITE;
            IntPtr procHandle = Extern.OpenProcess((int) desiredAccess,
                false, idProcess);

            IntPtr loadLibraryAddr = Extern.GetProcAddress(Extern.GetModuleHandle("kernel32.dll"), "LoadLibraryA");
            IntPtr allocMemAddress = Extern.VirtualAllocEx(procHandle, IntPtr.Zero, (uint)((pathDll.Length + 1) *
                Marshal.SizeOf(typeof(char))),(int) (State.MEM_COMMIT | State.MEM_RESERVE),(int) Protection.PAGE_READWRITE);
            UIntPtr bytesWritten;
            Extern.WriteProcessMemory(procHandle, allocMemAddress, Encoding.Default.GetBytes(pathDll),
                (uint)((pathDll.Length + 1) * Marshal.SizeOf(typeof(char))), out bytesWritten);
            Extern.CreateRemoteThread(procHandle, IntPtr.Zero, 0, loadLibraryAddr, allocMemAddress, 0, IntPtr.Zero);
        }
        public static void InjectSimple(int IdProcess, string pathDll)
        {

        }

    }
}

