using System.Runtime.InteropServices;

namespace ProcessInjector.Services.ExternFunction
{
    public static class Extern
    {
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool InheritHandle, int processId);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string ModuleName);

        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        internal static extern IntPtr GetProcAddress(IntPtr module, string procName);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        internal static extern IntPtr VirtualAllocEx(IntPtr process, IntPtr address, uint size, uint AllocationType, uint protect);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out UIntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        internal static extern IntPtr CreateRemoteThread(IntPtr hProcess,
            IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

        internal const int PROCESS_CREATE_THREAD = 0x0002;
        internal const int PROCESS_QUERY_INFORMATION = 0x0400;
        internal const int PROCESS_VM_OPERATION = 0x0008;
        internal const int PROCESS_VM_WRITE = 0x0020;
        internal const int PROCESS_VM_READ = 0x0010;

        internal const uint MEM_COMMIT = 0x00001000;
        internal const uint MEM_RESERVE = 0x00002000;
        internal const uint PAGE_READWRITE = 4;
        internal const uint PAGE_EXECUTE_READWRITE = 0x40;
    }
}
