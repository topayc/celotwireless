using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CelotMClient.NMSStructure
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public class cg_header_t
    {
        [MarshalAs(UnmanagedType.U4)]
        public uint session_id;

        [MarshalAs(UnmanagedType.U4)]
        public uint message_type;

        [MarshalAs(UnmanagedType.U4)]
        public uint data_len;

        [MarshalAs(UnmanagedType.U4)]
        public uint pro_ver;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public class config_t
    {

        [MarshalAs(UnmanagedType.U4)]
        public uint ethernet1_state;		// LAN 포트1 상태 ,  장애 : 1, 정상 : 0

        [MarshalAs(UnmanagedType.U4)]
        public uint ethernet2_state;		// LAN 포트 2 상태 ,  장애 : 1, 정상 : 0

        [MarshalAs(UnmanagedType.U4)]
        public uint network_state;			// Wan 연결 상태,  무선 : 1, 유선 : 0

        [MarshalAs(UnmanagedType.U4)]
        public uint external_power;		// 외부 전원 차단 : 1, 정상 :0

        [MarshalAs(UnmanagedType.U4)]
        public uint use_rx_amount;			// 데이터 사용량(rx)

        [MarshalAs(UnmanagedType.U4)]
        public uint use_tx_amount;			// 데이터 사용량(tx)

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string current_ip_address;	// 현재 ip 주소		

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 23)]
        public string current_time; 		// timestemp 	

        [MarshalAs(UnmanagedType.U4)]
        public uint moduleband;      		// band : LTE or Not

        [MarshalAs(UnmanagedType.U4)]
        public uint moduleservice; 		// Service : Connected or Not

        [MarshalAs(UnmanagedType.I4)]
        public int modulesignal;   		// RSSI Level 0 ~ -120)

        [MarshalAs(UnmanagedType.U4)]
        public uint devicestatus; 		// 외부 device 연결 상태, 장애:1, 정상: 0, unknown:2

        [MarshalAs(UnmanagedType.U4)]
        public uint wifistatus; 		// WiFi 상태,  On:1, Off : 0, else: not support

        [MarshalAs(UnmanagedType.U4)]
        public uint vpnstatus;   		// VPN 상태 사용:1, 사용안함: 0, else : not support

        [MarshalAs(UnmanagedType.I4)]
        public int newsms;			// 새로운 SMS 개수, -1 : SMS not support

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string sw_version;    		// firmware version

        [MarshalAs(UnmanagedType.U4)]
        public uint rpt_time;			// report time(주기설정,분)

        [MarshalAs(UnmanagedType.I4)]
        public int rsrqsignal;   			// RSRQ (0 ~ 97)

        [MarshalAs(UnmanagedType.I4)]
        public int rsrpsignal;   			// RSRP (-3 ~ -20	

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string hw_version;		// hw version

        [MarshalAs(UnmanagedType.U4)]
        public uint rpt_port;			//  report port	

        [MarshalAs(UnmanagedType.U4)]
        public uint rmt_port;			//  remort port	

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
        public int[] ext_device1;		// external device  int 값

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string ext_device2;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class nms_reprot_t
    {
        public cg_header_t header;
        public config_t data;

    
    }
}
