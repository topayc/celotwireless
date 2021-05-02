using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RouterSimulation
{
    public enum nms_cmd_t
    {
        NMS_CMD_CONREQ,
        NMS_CMD_SRESET,
        NMS_CMD_WCLOSE,
        NMS_CMD_WOPEN,
        NMS_CMD_STRING,
        NMS_CMD_SMS_SEND,
        NMS_CMD_CONFIG,
        NMS_CMD_MAX
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public class nms_cmd_sms_send_t
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string receive_number;
        public string message; // 가변길이 문자열
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public class remote_cmd_header_t
    {
        [MarshalAs(UnmanagedType.I4)]
        public int nms_cmd;
      
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 9)]
        public string secu_code;
        
        [MarshalAs(UnmanagedType.U4)]
        public uint cmd_len;
    }

    [StructLayout(LayoutKind.Explicit)]
    public class cmd
    {
        [FieldOffset(0)]
        [MarshalAs(UnmanagedType.AnsiBStr)]
        public string cmd_string;  // 가변길이 문자열

        [FieldOffset(0)]
        public nms_cmd_sms_send_t sms_send;
        
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public class remote_cmd_t
    {
        public remote_cmd_header_t header;
        public cmd cmd;
      
    }

}
