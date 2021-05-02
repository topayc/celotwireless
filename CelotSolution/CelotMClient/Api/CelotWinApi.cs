using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CelotMClient.Api
{
    public class CelotWinApi
    {
        public static IntPtr wow64Value;

        #region DLLImport
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool Wow64DisableWow64FsRedirection(ref IntPtr ptr);
        
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool Wow64RevertWow64FsRedirection(IntPtr ptr);
        #endregion DLLImport

        public static void EnableWow64DisableWow64FsRedirection()
        {
            wow64Value = IntPtr.Zero;
            Wow64DisableWow64FsRedirection(ref wow64Value);
        }


        public static void DisableWow64DisableWow64FsRedirection()
        {
            Wow64RevertWow64FsRedirection(wow64Value);
        }

    }
}
