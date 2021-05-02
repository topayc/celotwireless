using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RouterSimulation
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct cg_header_t
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
    public struct config_t
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
    public struct nms_reprot_t
    {
        public cg_header_t header;
        public config_t data;
    }

    public class RouterClient
    {
        private int no;
        private int sleepTime;
        private int port;
        public bool running;
        public Form1 form;
        public Random rand;
        public int sessionId;
        private string inIp;
        private string exIp;
        private uint rpt_port;
        private uint rmt_port;



        public RouterClient(Form1 form, int no, int port, int sleepTime)
        {
            this.rand = new Random();
            this.rpt_port = (uint)rand.Next(0, 63232);
            this.rmt_port = (uint)rand.Next(0, 63232);
            this.no = no;
            this.sleepTime = sleepTime;
            this.port = port;
            this.running = true;
            this.form = form;
            this.sessionId = rand.Next(10000000, 99999999);
            this.inIp = String.Format("{0}.{1}.{2}.{3}", rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));
            this.exIp = String.Format("{0}.{1}.{2}.{3}", rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255), rand.Next(0, 255));
        }


        public RouterClient(Form1 form, int port)
        {
            this.form = form;
            this.port = port;
        }

        public void Write(nms_reprot_t report)
        {
            byte[] buffer = new byte[1024];
            TcpClient tcpClient = new TcpClient();

            tcpClient.Connect("localhost", this.port);
            this.form.textBox1.AppendText((String.Format("\n[CONNECT]          : {0} 번 Router have been connected  to destination port {1}\n", report.header.session_id, this.port)));

            NetworkStream stream = tcpClient.GetStream();
            unsafe
            {
                fixed (byte* fixed_buffer = buffer)
                {
                    Marshal.StructureToPtr(report, (IntPtr)fixed_buffer, false);
                }
            }
            stream.Write(buffer, 0, Marshal.SizeOf(report));

            this.form.textBox2.AppendText("\r\n"+
                (String.Format("[WRITE PACKET] : {0} 번 Router Send Report packet  to destination port {1} [ByteLength : {2}]\n ", report.header.session_id, this.port, Marshal.SizeOf(report))));
            //this.form.textBox2.AppendText("\r\n" +report.data.current_time);

            tcpClient.Close();
            this.form.textBox1.AppendText((String.Format("\n[DISCONNECTED] : {0} 번 Router disconnected\n", report.header.session_id)));
        }

        public string Dump(nms_reprot_t report)
        {
            Type type = report.GetType();
            FieldInfo[] fields = type.GetFields();
            PropertyInfo[] properties = type.GetProperties();

            Dictionary<string, object> values = new Dictionary<string, object>();
            Array.ForEach(fields, (field) => values.Add(field.Name, field.GetValue(report)));
            return String.Join(", ", values);
        }

        public nms_reprot_t GeneraterNmsPacket()
        {
            config_t config = new config_t();

            cg_header_t header = new cg_header_t();
            header.session_id = (uint)this.sessionId;
            header.message_type = (uint)rand.Next(0, 2);
            header.data_len = (uint)Marshal.SizeOf(config);
            header.pro_ver = 33333;
            header.message_type = (uint)rand.Next(0, 2);

            config.ethernet1_state = (uint)rand.Next(0, 2);
            config.ethernet2_state = (uint)rand.Next(0, 1);
            config.network_state = (uint)rand.Next(0, 2);
            config.external_power = (uint)rand.Next(0, 2);
            config.use_rx_amount = (uint)rand.Next(0, 2000);
            config.use_tx_amount = (uint)rand.Next(0, 2000);
            config.current_ip_address = String.Format("{0}({1})", this.inIp, this.exIp);
            config.current_time = DateTime.Now.ToString("yyMMddHHmmss");
            //config.moduleband = (uint)rand.Next(0, 5);
            config.moduleband = (uint)rand.Next(0, 4);
            config.moduleservice = (uint)rand.Next(0, 6);
            config.modulesignal = rand.Next(-120, 0);
            config.devicestatus = (uint)rand.Next(0, 2);
            config.wifistatus = (uint)rand.Next(0, 2);
            config.vpnstatus = (uint)rand.Next(0, 2);
            config.newsms = rand.Next(5);
            config.sw_version = "ver 1.1.1";
            config.rpt_time = (uint)rand.Next(0, 40);
            config.rsrqsignal = rand.Next(0, 97);
            config.rsrpsignal = rand.Next(-20, -3);
            config.hw_version = "celot ver 2.1.2";
            config.rpt_port = this.rpt_port;
            config.rmt_port = this.rmt_port;
            int[] extDevice1 = new int[64];
            extDevice1[0] = rand.Next(0, 400);
            config.ext_device1 = extDevice1;
            config.ext_device2 = "aaaaaaa";

            nms_reprot_t report = new nms_reprot_t();
            report.header = header;
            report.data = config;
            return report;
        }
    }
}
