using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RouterSimulation
{
    public partial class Form1 : Form
    {
        private List<RouterClient> routeClientList;
        Random rand = new Random();
        public Form1()
        {
            InitializeComponent();
            this.routeClientList = new List<RouterClient>();
            this.Text = "Router Simulation";

        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            //this.textBox1.Text = "";
            //this.textBox2.Text = "";
            //cg_header_t k = new cg_header_t();
            //config_t c = new config_t();
            //nms_reprot_t r = new nms_reprot_t();
            // MessageBox.Show(Marshal.SizeOf(r).ToString());
            timer1.Enabled = true;
            //int backlog = Convert.ToInt32(this.backLogBox.Text);
            //Random rand = new Random();
            //for (int i = 0; i < backlog; i++)
            //{
            //    RouterClient routerClient = new RouterClient(this, i+1, Convert.ToInt32(this.portBox.Text), rand.Next(3000, 6000));
            //    this.routeClientList.Add(routerClient);
            //    Thread t = new Thread(new ThreadStart(routerClient.RunUdpThread));
            //    t.Start();
            //}
        }

        int timeCount = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
          
            int port = Convert.ToInt32(this.portBox.Text);
            RouterClient client = new RouterClient(this, port);
            int count = rand.Next(0, 5);
            nms_reprot_t report = new nms_reprot_t();
            config_t config = new config_t();
            cg_header_t header = new cg_header_t();
           
            switch (count)
            {
                case 0:
                    header.session_id = 11111111;
                    config.current_ip_address = String.Format("{0}({1})", "101.101.101.101", "101.101.101.101");
                    break;
                case 1:
                    header.session_id = 22222222;
                    config.current_ip_address = String.Format("{0}({1})", "102.102.102.102", "102.102.102.102");
                    break;
                case 2:
                    header.session_id = 33333333;
                    config.current_ip_address = String.Format("{0}({1})", "103.103.103.103", "103.103.103.103");
                    break;
                case 3:
                    header.session_id = 44444444;
                    config.current_ip_address = String.Format("{0}({1})", "104.104.104.104", "104.104.104.104");
                    break;
                case 4:
                    header.session_id = 55555555;
                    config.current_ip_address = String.Format("{0}({1})", "105.105.105.105", "105.105.105.105");
                    break;
            }
            //정상 패킷을 테스트로 보낸다 
            //header.message_type = (uint)0;
            //header.data_len = (uint)Marshal.SizeOf(config);
            //header.pro_ver = 33333;

            //config.ethernet1_state = (uint)0;
            //config.ethernet2_state = (uint)0;
            //config.network_state = (uint)1;
            //config.external_power = (uint)0;
            //config.use_rx_amount = (uint)rand.Next(0, 2000);
            //config.use_tx_amount = (uint)rand.Next(0, 2000);
            //config.current_time = DateTime.Now.ToString("yyMMddHHmmss");
            ////config.moduleband = (uint)rand.Next(0, 5);
            //config.moduleband = (uint)rand.Next(0, 4);
            //config.moduleservice = (uint)rand.Next(0, 6);
            //config.modulesignal = -1;
            //config.devicestatus = 0;
            //config.wifistatus = 1;
            //config.vpnstatus = (uint)1;
            //config.newsms = 320;
            //config.sw_version = "ver 1.1.1";
            //config.rpt_time = (uint)rand.Next(0, 40);
            //config.rsrqsignal = rand.Next(0, 97);
            //config.rsrpsignal = rand.Next(-20, -3);
            //config.hw_version = "celot ver 2.1.2";
            //config.rpt_port = 3333;
            //config.rmt_port = 2222;
            //int[] extDevice1 = new int[64];
            //extDevice1[0] = 300;
            //config.ext_device1 = extDevice1;
            //config.ext_device2 = "aaaaaaa";
            //report.header = header;
            //report.data = config;

            header.message_type = (uint)rand.Next(0, 2);
            header.data_len = (uint)Marshal.SizeOf(config);
            header.pro_ver = 33333;

            config.ethernet1_state = (uint)rand.Next(0, 2);
            config.ethernet2_state = (uint)rand.Next(0, 1);
            config.network_state = (uint)rand.Next(0, 2);
            config.external_power = (uint)rand.Next(0, 2);
            config.use_rx_amount = (uint)rand.Next(0, 2000);
            config.use_tx_amount = (uint)rand.Next(0, 2000);
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
            config.rpt_port = 3333;
            config.rmt_port = 2222;
            int[] extDevice1 = new int[64];
            extDevice1[0] = rand.Next(0, 400);
            config.ext_device1 = extDevice1;
            config.ext_device2 = "aaaaaaa";

            report.header = header;
            report.data = config;

            client.Write(report);
            timeCount++;
        }

        private void stopBtn_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {

            remote_cmd_t remoteCmd = new remote_cmd_t();
            int size = Marshal.SizeOf(remoteCmd);
            int size2 = Marshal.SizeOf(new cmd());
            int size3 = Marshal.SizeOf(new nms_cmd_sms_send_t());
            MessageBox.Show(size3.ToString());
        }
    }
}
