using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using CelotMClient.NMSStructure;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CelotMClient.Manager
{
    public class RouterCommandManager
    {
        public string Message { get; set; }
        public void HttpOpen(string url)
        {
            Process.Start(url);
        }

        public bool Ping(string ip, out string message)
        {
            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();
            options.DontFragment = true;

            PingReply reply;
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer;
            int timeout = 1000;
            string flag = "";
            try
            {
                options.DontFragment = true;
                buffer = Encoding.ASCII.GetBytes(data);
                reply = pingSender.Send(ip, timeout, buffer, options);
                if (reply.Status == IPStatus.Success)
                {
                    message = String.Format("{0}의 응답 : 바이트 ={1} 시간={2}ms TTL={3}\n", ip, reply.Buffer.Length, reply.RoundtripTime, reply.Options.Ttl);
                    return true;
                }
                else
                {
                    message = String.Format("Failed : {0}", reply.Status.ToString());
                    return false;
                }
            }
            catch (Exception e)
            {
                message = e.Message;
                return false;
            }
        }

        public bool RouterReset(string secuCode,string ip, int port = 0)
        {
            StringBuilder strBuilder = new StringBuilder();
            try
            {
                port = port == 0 ? ApplicationConfig.Instance().RemotePort : port;
                TcpClient client = new TcpClient();
                client.Connect(ip, port);
                NetworkStream stream = client.GetStream();

                //header for router reset command
                remote_cmd_header_t header = new remote_cmd_header_t();
                header.nms_cmd = (int)nms_cmd_t.NMS_CMD_SRESET;
                header.secu_code = secuCode;

                //cmd for router reset command
                cmd cmd = new cmd();

                remote_cmd_t remoteCmd = new remote_cmd_t();
                remoteCmd.header = header;
                remoteCmd.cmd = cmd;
                byte[] remotePacketBuffer = new byte[1024];
                unsafe
                {
                    fixed (byte* fixedPacketBuffer = remotePacketBuffer)
                    {
                        Marshal.StructureToPtr(remoteCmd, (IntPtr)fixedPacketBuffer, false);
                    }
                }
                stream.Write(remotePacketBuffer, 0, Marshal.SizeOf(remoteCmd));
                strBuilder.AppendLine(String.Format("[OK] Router reset : [{0}] :[{2}] :[{2}]", ip, port, secuCode));
                Message = strBuilder.ToString();
                return true;
            }
            catch (Exception ee)
            {
                strBuilder.AppendLine(String.Format("[FAIL] Router reset : [{0}] :[{2}] :[{2}]", ip, port, secuCode)); 
                strBuilder.AppendLine(ee.Message); 
                Message = strBuilder.ToString();
                return false;
            }
        }

    }
}
